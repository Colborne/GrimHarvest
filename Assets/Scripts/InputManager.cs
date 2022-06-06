using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InputManager : MonoBehaviour
{    
    PlayerControls playerControls;
    AnimatorManager animatorManager;
    ToolManager toolManager;
    public Vector2 movementInput;
    public Vector2 mouseInput;
    public float scrollInput;
    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;
    public bool interactInput;
    public bool toolbar1Input;
    public bool toolbar2Input;
    public bool toolbar3Input;
    public bool toolbar4Input;
    public bool toolbar5Input;
    public bool isInteracting = false;
    private void Awake() 
    {
        //player = GetComponent<Player>();
        animatorManager = GetComponent<AnimatorManager>();
        toolManager = GetComponent<ToolManager>();
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
            playerControls.PlayerActions.Toolbar1.performed += i => toolbar1Input = true;
            playerControls.PlayerActions.Toolbar1.canceled += i => toolbar1Input = false;
            playerControls.PlayerActions.Toolbar2.performed += i => toolbar2Input = true;
            playerControls.PlayerActions.Toolbar2.canceled += i => toolbar2Input = false;
            playerControls.PlayerActions.Toolbar3.performed += i => toolbar3Input = true;
            playerControls.PlayerActions.Toolbar3.canceled += i => toolbar3Input = false;
            playerControls.PlayerActions.Toolbar4.performed += i => toolbar4Input = true;
            playerControls.PlayerActions.Toolbar4.canceled += i => toolbar4Input = false;
            playerControls.PlayerActions.Toolbar5.performed += i => toolbar5Input = true;
            playerControls.PlayerActions.Toolbar5.canceled += i => toolbar5Input = false;
        }
        playerControls.Enable();
    }

    private void OnDisable() 
    {
        playerControls.Disable();
    }
    private void Update() 
    {
        HandleAllInputs();
    }

    public void HandleAllInputs()
    {
        isInteracting = animatorManager.animator.GetBool("isInteracting");

        HandleMovementInput();
        HandlePlanting();
    }

    private void HandleMovementInput()
    {
        verticalInput = isInteracting ? 0 : movementInput.y;
        horizontalInput = isInteracting ? 0 : movementInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(horizontalInput, moveAmount);
    } 

    private void HandlePlanting()
    {
        if(interactInput)
        {
            if(toolManager.currentTool == 0)
            {
                animatorManager.animator.SetBool("isInteracting", true);
                animatorManager.animator.CrossFade("Hoe", 0f);
            }
            else if(toolManager.currentTool == 1)
            {
                animatorManager.animator.SetBool("isInteracting", true);
                animatorManager.animator.CrossFade("Plant", 0f);
            }
            else if(toolManager.currentTool == 2)
            {
                animatorManager.animator.SetBool("isInteracting", true);
                animatorManager.animator.CrossFade("Axe", 0f);
            }
        }
    }
}