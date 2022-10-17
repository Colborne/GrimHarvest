using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InputManager : MonoBehaviour
{    
    PlayerControls playerControls;
    public AnimatorManager animatorManager;
    ActionManager actionManager;
    StatsManager statsManager;
    public Vector2 movementInput;
    public Vector2 mouseInput;
    public float scrollInput;
    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;
    public bool interactInput;
    public bool heavyInput;
    public bool dodgeInput;
    public bool sprintInput;
    public bool isInteracting = false;
    public bool isSprinting = false;
    public bool isRolling = false;
    public bool isComboing = false;
    private void Awake() 
    {
        animatorManager = GetComponent<AnimatorManager>();
        actionManager = GetComponent<ActionManager>();
        statsManager = GetComponent<StatsManager>();
    }

    private void OnEnable() 
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Mouse.performed += i => mouseInput = i.ReadValue<Vector2>();
            //playerControls.UI.MousePosition.performed += i => mousePosition = i.ReadValue<Vector2>();
            playerControls.PlayerActions.MouseWheel.performed += i => scrollInput = i.ReadValue<float>();
            playerControls.PlayerActions.Interact.performed += i => interactInput = true;
            playerControls.PlayerActions.Interact.canceled += i => interactInput = false;
            playerControls.PlayerActions.Heavy.performed += i => heavyInput = true;
            playerControls.PlayerActions.Heavy.canceled += i => heavyInput = false;
            playerControls.PlayerActions.Dodge.performed += i => dodgeInput = true;
            playerControls.PlayerActions.Dodge.canceled += i => dodgeInput = false;
            playerControls.PlayerActions.Sprint.performed += i => sprintInput = true;
            playerControls.PlayerActions.Sprint.canceled += i => sprintInput = false;
        }
        playerControls.Enable();
    }

    private void OnDisable() 
    {
        playerControls.Disable();
    }
    private void Update() 
    {
        isInteracting = animatorManager.animator.GetBool("isInteracting");
        isRolling = animatorManager.animator.GetBool("isRolling");
        isSprinting = animatorManager.animator.GetBool("isSprinting");
        isComboing = animatorManager.animator.GetBool("isCombo");

        HandleAllInputs();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleAction();
        HandleRoll();
    }

    private void HandleRoll()
    {
        if(dodgeInput && !isInteracting && !isRolling)
        {
            actionManager.Roll();
            dodgeInput = false;
        }
    }

    private void HandleMovementInput()
    {
        horizontalInput = isInteracting ? movementInput.x : movementInput.x;
        verticalInput = isInteracting ? movementInput.y: movementInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(horizontalInput, moveAmount);
    } 

    private void HandleAction()
    {
        if(interactInput && !isComboing)
        {
            interactInput = false;
            actionManager.Use();
        }
        if(heavyInput && !isComboing)
        {
            heavyInput = false;
            actionManager.UseHeavy();
        }
    }
}