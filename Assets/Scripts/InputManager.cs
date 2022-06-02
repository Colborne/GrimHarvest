using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InputManager : MonoBehaviour
{    
    PlayerControls playerControls;
    AnimatorManager animatorManager;
    public Vector2 movementInput;
    public Vector2 mouseInput;
    public float scrollInput;
    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;
    public bool interactInput;
    public bool isInteracting = false;
    private void Awake() 
    {
        //player = GetComponent<Player>();
        animatorManager = GetComponent<AnimatorManager>();
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
        isInteracting = animatorManager.animator.GetBool("isPlanting");

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
            animatorManager.animator.SetBool("isPlanting", true);
            animatorManager.animator.CrossFade("Plant", 0f);
        }
    }
}