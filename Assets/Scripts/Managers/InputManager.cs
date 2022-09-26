using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InputManager : MonoBehaviour
{    
    PlayerControls playerControls;
    public AnimatorManager animatorManager;
    ToolManager toolManager;
    public Vector2 movementInput;
    public Vector2 mouseInput;
    public float scrollInput;
    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;
    public bool interactInput;
    public bool dodgeInput;
    public bool sprintInput;
    public bool isInteracting = false;
    public bool rolling;
    public float _x = 0f;
    public float _y = 0f;
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
        HandleAllInputs();
        if(isInteracting)
            AttackAlgorithm();
        else
        {
            _x = 1f * horizontalInput;
            _y = 1f * verticalInput;
        }
    }

    public void HandleAllInputs()
    {
        isInteracting = animatorManager.animator.GetBool("isInteracting");
        rolling = animatorManager.animator.GetBool("isRolling");

        HandleMovementInput();
        HandlePlanting();
        HandleRoll();
    }

    private void HandleRoll()
    {
        if(dodgeInput && !isInteracting && !rolling)
        {
            dodgeInput = false;
            animatorManager.animator.CrossFade("Roll", .2f);
            animatorManager.animator.SetInteger("combo", 0);
        }
    }

    private void HandleMovementInput()
    {
        horizontalInput = isInteracting ? movementInput.x : movementInput.x;
        verticalInput = isInteracting ? movementInput.y: movementInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(horizontalInput, moveAmount);
    } 

    private void HandlePlanting()
    {
        if(interactInput)
        {
            interactInput = false;
            toolManager.UseTool();
        }
    }
    private void AttackAlgorithm()
    {
        _x = Mathf.Lerp(_x, 0f, 2f * Time.deltaTime);
        _y = Mathf.Lerp(_y, 0f, 2f * Time.deltaTime);
    }
}