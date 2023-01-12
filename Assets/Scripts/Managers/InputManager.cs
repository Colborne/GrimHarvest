using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public bool jumpInput;
    public bool toolbar1Input;
    public bool toolbar2Input;
    public bool toolbar3Input;
    public bool toolbar4Input;
    public bool toolbar5Input;
    public bool toolbar6Input;
    public bool toolbar7Input;
    public bool isInteracting = false;
    public float cameraInputX;
    public float cameraInputY;
    public float orbit = 10;
    float step;
    public float strength = 1f;
    public RectTransform cursor;
    public RectTransform indicator;
    public float TestAngle;

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
            playerControls.PlayerActions.Jump.performed += i => jumpInput = true;
            playerControls.PlayerActions.Jump.canceled += i => jumpInput = false;
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
            playerControls.PlayerActions.Toolbar6.performed += i => toolbar6Input = true;
            playerControls.PlayerActions.Toolbar6.canceled += i => toolbar6Input = false;
            playerControls.PlayerActions.Toolbar7.performed += i => toolbar7Input = true;
            playerControls.PlayerActions.Toolbar7.canceled += i => toolbar7Input = false;
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
        isInteracting = false;//animatorManager.animator.GetBool("isInteracting");

        if(GetComponent<FishManager>().isCatching && GetComponent<FishManager>().isHooked)
            HandleFishingInput();
        else
        {
            HandleMovementInput();
            HandlePlanting();
        }
    }

    private void HandleMovementInput()
    {
        cameraInputX = mouseInput.x;
        cameraInputY = mouseInput.y;

        verticalInput = isInteracting ? 0 : movementInput.y;
        horizontalInput = isInteracting ? 0 : movementInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(horizontalInput, moveAmount);
    } 

    private void HandlePlanting()
    {
        if(interactInput)
        {
            toolManager.UseTool();
        }
    }

    private void HandleFishingInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        if(verticalInput != 0 || horizontalInput != 0)
            indicator.GetComponent<Image>().enabled = true;
        else
            indicator.GetComponent<Image>().enabled = false;

        Vector3 Direction = new Vector3(horizontalInput * orbit, verticalInput * orbit, 0);
        cursor.localPosition = Direction;
        cursor.rotation = Quaternion.AngleAxis(Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg, Vector3.forward);
        step = (strength * Vector2.Distance(cursor.localPosition, Vector2.zero)) / (FindObjectOfType<FishMovement>().currentFish.baseStrength / 20);

        indicator.localPosition = new Vector3(
            FindObjectOfType<FishMovement>().rect.localPosition.x + horizontalInput * orbit, 
            FindObjectOfType<FishMovement>().rect.localPosition.y + verticalInput * orbit, 0);
        indicator.rotation = cursor.rotation;
        
        float angle = Quaternion.Angle(Quaternion.Euler(0,0,0), cursor.rotation);
        TestAngle = Mathf.Abs((angle - FindObjectOfType<FishMovement>().angleFromCenter)) / 180;

        if(FindObjectOfType<FishMovement>().Radius > 0)
            FindObjectOfType<FishMovement>().Radius -= step * TestAngle * Time.deltaTime;
        
    }
}