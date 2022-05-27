using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MovementManager : MonoBehaviour 
{
    public float moveSpeed = 12;
  
    Rigidbody rigidBody;
    public Vector3 moveVector;
    public Transform cameraObject;
    Vector3 moveDirection;
    InputManager input;
    Vector3 normalVector;

    private void Awake() 
    {
        input = GetComponent<InputManager>();
        rigidBody = GetComponent<Rigidbody>();
        normalVector = new Vector3(0f,0f,0f);
    }
  
    private void LateUpdate() 
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * input.verticalInput;
        moveDirection += cameraObject.right * input.horizontalInput;
        moveDirection.Normalize();
        
        moveDirection = moveDirection * moveSpeed;

        Vector3 movementVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
        rigidBody.velocity = movementVelocity;
    }
}