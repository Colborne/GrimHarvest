using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
}
    /*PlayerManager playerManager;
    AnimatorManager animatorManager;
    InputManager inputManager;
    StatsManager stats;
    Vector3 moveDirection;
    public Transform cameraObject;
    Rigidbody playerRigidbody;
    
    [Header("Falling")]
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public LayerMask groundLayer;

    [Header("Movement Flags")]

    public bool isSprinting;
    public bool isSneaking;
    public bool isGrounded;
    public bool isJumping;
    public bool isAttacking;
    public bool isFalling;
    public bool isSwimming;

    [Header("Movement Speeds")]
    public float sneakingSpeed = 1.5f;
    public float walkingSpeed = 2.5f;
    public float runningSpeed = 5;
    public float sprintingSpeed = 7;
     public float swimmingSpeed = 1.25f;
    public float rotationSpeed = 15;

    [Header("Jump Speeds")]
    public float jumpHeight = 3;
    public float gravityIntensity = -15;
    Vector3 normalVector;


    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        animatorManager = GetComponent<AnimatorManager>();
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        stats = GetComponent<StatsManager>();
        //cameraObject = Camera.main.transform;
    }
    
    public void HandleAllMovement()
    {
        HandleFallingAndLanding();

        if(inputManager.inventoryFlag){
            if(!isFalling)
                playerRigidbody.velocity = Vector3.zero;
            return;
        }
        
        if(playerManager.isInteracting && inputManager.attackChargeTimer == 0f)
        {
            if(isAttacking)
            {
                //playerRigidbody.velocity = Vector3.zero;
            }
        }
        
        HandleSwimming();
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        if(isJumping)
            return;
        if(isFalling)
            return;
        if(animatorManager.animator.GetBool("isInteracting") && inputManager.attackChargeTimer == 0)
            return;

        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection += cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();

        float weight = Mathf.Clamp(stats.inventoryWeight, 0, 1000);
        
        if(isSwimming)
        {
            if(inputManager.verticalInput != 0 || inputManager.horizontalInput != 0) 
            {
                stats.UseStamina(.25f);
                if(stats.currentStamina >= .25f){
                    moveDirection = (moveDirection * swimmingSpeed  * stats.swimSpeedBonus * (((1000f - weight) / 1000f) + .25f));
                    stats.drownTimer = 0f;
                }
                else
                    moveDirection = (moveDirection * swimmingSpeed * stats.swimSpeedBonus * (((1000f - weight) / 1000f) + .25f) / 2); 
            }
            if(stats.currentStamina <= .25f)
                stats.Drowning();
        }
        else if(isSprinting && stats.currentStamina >= .25f)
        {
            stats.UseStamina(.25f);
            moveDirection = moveDirection * sprintingSpeed * stats.baseSpeedBonus * (((1000f - weight) / 1000f) + .25f);
        }
        else if(isSneaking)
        {
            moveDirection = moveDirection * sneakingSpeed * stats.baseSpeedBonus * (((1000f - weight) / 1000f) + .25f);
        }
        else
        {
            if(inputManager.moveAmount >= 0.25f)
            {
                moveDirection = moveDirection * runningSpeed * stats.baseSpeedBonus * (((1000f - weight) / 1000f) + .25f);
            }        
            else
            {
                moveDirection = moveDirection * walkingSpeed * stats.baseSpeedBonus * (((1000f - weight) / 1000f) + .25f);
            }
        }

        Vector3 movementVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
        playerRigidbody.velocity = movementVelocity;
    }

    private void HandleRotation()
    {
        if(isJumping)
            return;
        if(isFalling)
            return;

        if(inputManager.attackChargeTimer == 0f)
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
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(inputManager.mousePosition);
            var look = ray.direction;
            look.y = 0;
            transform.rotation = Quaternion.LookRotation(look);
        }
    }
    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y = rayCastOrigin.y + .5f;//1.6f;
        Vector3 targetPosition = transform.position;

        if(!isGrounded)// && !isJumping)// && (!isSwimming || stats.currentStamina <= 0))
        {
            if(!playerManager.isInteracting)
                animatorManager.PlayTargetAnimation("Falling", true);
            
            inAirTimer += Time.deltaTime;
            playerRigidbody.AddForce(transform.forward * leapingVelocity);
            playerRigidbody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);          
        }
       
        if(Physics.SphereCast(rayCastOrigin, .2f, -Vector3.up, out hit, 1f, groundLayer))
        {
            if(!isGrounded && !playerManager.isInteracting)// && !isSwimming)
                animatorManager.PlayTargetAnimation("Landing", true);

            Vector3 rayCastHitPoint = hit.point;
            targetPosition.y = rayCastHitPoint.y;
            inAirTimer = 0;
            isGrounded = true;
        } 
        else
        {
            isGrounded = false;
        } 

        if(isGrounded && !isJumping)// && (!isSwimming || stats.currentStamina <= 0))
        {
            if(playerManager.isInteracting || inputManager.moveAmount > 0)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f);
            }
            else
            {
                transform.position = targetPosition;
            }
        }    
        

    }

    public void HandleJumping()
    {
        if(isGrounded)
        {
            if(stats.currentStamina >= 5)
            {
                stats.UseStamina(5);
                animatorManager.animator.SetBool("isJumping", true);
                animatorManager.PlayTargetAnimation("Jumping", true);
                
                float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight * stats.jumpBonus);
                Vector3 playerVelocity = moveDirection;
                playerVelocity.y = jumpingVelocity;
                playerRigidbody.velocity = playerVelocity;
            }
        }
    }

    public void HandleSwimming()
    {
        /*
        if(transform.position.y < -3f)
        {
            isSwimming = true;
            playerRigidbody.useGravity = false;
        }
        else
        {
            isSwimming = false;
            //playerRigidbody.useGravity = true;
        }
        */