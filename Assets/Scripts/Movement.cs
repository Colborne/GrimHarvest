using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    CharacterController characterController;
    AnimatorManager animatorManager;
    InputManager inputManager;
    public Transform cameraObject;
    CameraManager camera;
    Rigidbody playerRigidbody;
    Vector3 normalVector;
    Vector3 moveDirection;
    public float movementSpeed;
    public float rotationSpeed;
    public float buttonTime = 0.5f;
    public float jumpHeight = 5;
    public float cancelRate = 100;
    public float jumpTime;
    public bool jumping;
    public bool jumpCancelled;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        characterController = GetComponent<CharacterController>();
        playerRigidbody = GetComponent<Rigidbody>();
        //camera = FindObjectOfType<CameraManager>();
    }

    void Update()
    {
        if(GetComponent<FishManager>().isCatching || GetComponent<FishManager>().isHooked || GetComponent<FishManager>().isFishing)
            return;

        HandleMovement();
        HandleJump();
        //camera.HandleAllCameraMovement();
    }

    void FixedUpdate()
    {
        HandleJumpCancel();
    }

    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection += cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();

        Vector3 movementVelocity = Vector3.ProjectOnPlane(moveDirection * movementSpeed, normalVector);
        playerRigidbody.velocity = new Vector3(movementVelocity.x, playerRigidbody.velocity.y, movementVelocity.z);
        HandleRotation();
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

    private void HandleJump()
    {
        if (inputManager.jumpInput)
        {
            inputManager.interactInput = false;
            float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics.gravity.y));
            playerRigidbody.AddForce(new Vector3(0, jumpForce,0), ForceMode.Impulse);
            jumping = true;
            jumpCancelled = false;
            jumpTime = 0;
        }
        
        if (jumping)
        {
            jumpTime += Time.deltaTime;
            if (!inputManager.interactInput)
            {
                jumpCancelled = true;
            }
            if (jumpTime > buttonTime)
            {
                jumping = false;
            }
        }
    }

    private void HandleJumpCancel()
    {
        if(jumpCancelled && jumping && playerRigidbody.velocity.y > 0)
        {
            playerRigidbody.AddForce(Vector3.down * cancelRate * Time.deltaTime);
        }
    }
}
