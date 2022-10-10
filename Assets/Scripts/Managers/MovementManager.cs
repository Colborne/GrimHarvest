using UnityEngine;

public class MovementManager : MonoBehaviour
{
    CharacterController characterController;
    AnimatorManager animatorManager;
    InputManager inputManager;
    StatsManager statsManager;
    public Transform cameraObject;
    Rigidbody playerRigidbody;
    Vector3 normalVector;
    Vector3 moveDirection;
    public float movementSpeed;
    public float rotationSpeed;
    public float rollSpeed;
    public bool canRotate = true;
    public bool canRoll = false;
    public float tempV = 1f;
    public float anim = 1.45f;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        characterController = GetComponent<CharacterController>();
        playerRigidbody = GetComponent<Rigidbody>();
        statsManager = GetComponent<StatsManager>();
    }

    void Update()
    {
        if(inputManager.animatorManager.animator.GetInteger("combo") == -1) 
            canRotate = true;

        HandleMovement();
        if(canRotate)
            HandleRotation();
        HandleRoll();
    }

    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection += cameraObject.right * inputManager.horizontalInput;
        //moveDirection.Normalize();
        
        float speed = 0f;

        if(canRotate)
        {
            if(inputManager.animatorManager.animator.GetInteger("combo") > -1) 
                speed = movementSpeed / (inputManager.animatorManager.animator.GetInteger("combo") + 1);
            else
            {
                speed = movementSpeed;
                if (inputManager.sprintInput && statsManager.currentStamina > 0)
                {
                    inputManager.animatorManager.animator.SetBool("isSprinting", true);
                    statsManager.UseStamina(.25f);
                    tempV += .01f;
                    speed = movementSpeed * Mathf.Min(tempV, 2f);
                }
                else
                {
                    inputManager.animatorManager.animator.SetBool("isSprinting", false);
                    inputManager.sprintInput = false;
                } 
            }
        }
        else
        {
            speed = 0;
            tempV = 1f;
        }

        Vector3 movementVelocity = Vector3.ProjectOnPlane(moveDirection * speed, normalVector);
        playerRigidbody.velocity = movementVelocity;
        
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;
        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if(targetDirection == Vector3.zero)
            targetDirection = transform.forward;
        
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    public void HandleRoll()
    {        
        canRoll = inputManager.isRolling;
        if(canRoll)
            playerRigidbody.velocity = transform.forward * rollSpeed;
        
    }

    public void CanRotate()
    {
        canRotate = true;
        inputManager.animatorManager.animator.SetFloat("animSpeed", 1f);
    }
    public void CannotRotate()
    {
        canRotate = false;
        inputManager.animatorManager.animator.SetFloat("animSpeed",anim);
    }
}