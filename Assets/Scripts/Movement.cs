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

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        characterController = GetComponent<CharacterController>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection += cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();

        Vector3 movementVelocity = Vector3.ProjectOnPlane(moveDirection * movementSpeed, normalVector);
        playerRigidbody.velocity = movementVelocity;
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
}
