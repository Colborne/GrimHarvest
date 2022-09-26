using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    CharacterController characterController;
    AnimatorManager animatorManager;
    InputManager inputManager;
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

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        characterController = GetComponent<CharacterController>();
        playerRigidbody = GetComponent<Rigidbody>();
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
       // moveDirection.Normalize();
        
        float speed = 0f;

        if(canRotate)
        {
            if(inputManager.animatorManager.animator.GetInteger("combo") > -1) 
                speed = movementSpeed / (inputManager.animatorManager.animator.GetInteger("combo") + 1);
            else
                speed = movementSpeed;
            if (inputManager.sprintInput)
            {
                tempV += .01f;
                speed = movementSpeed * Mathf.Min(tempV, 2f);
            }
            else
                tempV = 1f;
        }
        else
            speed = 0;

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
        canRoll = inputManager.rolling;
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