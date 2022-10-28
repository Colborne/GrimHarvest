using UnityEngine;

public class MovementManager : MonoBehaviour
{
    CharacterController characterController;
    InputManager inputManager;
    StatsManager statsManager;
    CameraManager camera;
    Rigidbody playerRigidbody;
    Vector3 normalVector;
    Vector3 moveDirection;
    public Transform cameraObject;
    public float movementSpeed;
    public float rotationSpeed;
    public float rollSpeed;
    public float anim = 1.45f;
    float tempV = 1f;
    [HideInInspector] public bool canRotate = true;
    [HideInInspector]public bool canRoll = false;

    void Awake()
    {
        inputManager = GetComponent<InputManager>();
        characterController = GetComponent<CharacterController>();
        playerRigidbody = GetComponent<Rigidbody>();
        statsManager = GetComponent<StatsManager>();
        camera = FindObjectOfType<CameraManager>();
    }

    void Update()
    {
        if(inputManager.animatorManager.animator.GetInteger("combo") == -1) 
            canRotate = true;

        HandleMovement();
        if(canRotate)
            HandleRotation();
        HandleRoll();
        camera.HandleAllCameraMovement();
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
                    tempV += .01f;
                    speed = movementSpeed * Mathf.Min(tempV, 2f);
                }
                else
                {   
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
    }
    public void CannotRotate()
    {
        canRotate = false;
    }
}