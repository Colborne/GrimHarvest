using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{    
    PlayerControls playerControls;
    ActionManager actionManager;
    StatsManager statsManager;
    EquipmentManager equipmentManager;
    public AnimatorManager animatorManager;
    public Vector2 movementInput;
    public Vector2 mouseInput;
    public float scrollInput;
    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;
    public float cameraInputX;
    public float cameraInputY;
    public bool interactInput;
    public bool leftInput;
    public bool heavyInput;
    public bool specialInput;
    public bool dodgeInput;
    public bool sprintInput;
    public bool changeSchemeInput;
    public bool changeStatsInput;
    public bool changeWeaponInput;
    public bool changeLeftWeaponInput;
    public bool isInteracting = false;
    public bool isSprinting = false;
    public bool isRolling = false;
    public bool isComboing = false;
    public bool isController = true;

    private void Awake() 
    {
        animatorManager = GetComponent<AnimatorManager>();
        actionManager = GetComponent<ActionManager>();
        statsManager = GetComponent<StatsManager>();
        equipmentManager = GetComponent<EquipmentManager>();
        Cursor.lockState = CursorLockMode.Locked;
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
            playerControls.PlayerActions.Left.performed += i => leftInput = true;
            playerControls.PlayerActions.Left.canceled += i => leftInput = false;
            playerControls.PlayerActions.Heavy.performed += i => heavyInput = true;
            playerControls.PlayerActions.Heavy.canceled += i => heavyInput = false;
            playerControls.PlayerActions.Special.performed += i => specialInput = true;
            playerControls.PlayerActions.Special.canceled += i => specialInput = false;
            playerControls.PlayerActions.Dodge.performed += i => dodgeInput = true;
            playerControls.PlayerActions.Dodge.canceled += i => dodgeInput = false;
            playerControls.PlayerActions.Sprint.performed += i => sprintInput = true;
            playerControls.PlayerActions.Sprint.canceled += i => sprintInput = false;
            playerControls.PlayerActions.ChangeWeapon.performed += i => changeWeaponInput = true;
            playerControls.PlayerActions.ChangeWeapon.canceled += i => changeWeaponInput = false;
            playerControls.PlayerActions.ChangeLeftWeapon.performed += i => changeLeftWeaponInput = true;
            playerControls.PlayerActions.ChangeLeftWeapon.canceled += i => changeLeftWeaponInput = false;
            playerControls.GameCommands.ChangeScheme.performed += i => changeSchemeInput = true;
            playerControls.GameCommands.ChangeScheme.canceled += i => changeSchemeInput = false;
            playerControls.GameCommands.ChangeStats.performed += i => changeStatsInput = true;
            playerControls.GameCommands.ChangeStats.canceled += i => changeStatsInput = false;
            playerControls.bindingMask = InputBinding.MaskByGroup(playerControls.controlSchemes.First(x => x.name == "Controller").bindingGroup);
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
        HandleMouse();
        HandleControlScheme();
        HandleChangeWeapon();
        HandleChangeLeftWeapon();
    }

    private void HandleRoll()
    {
        
        if(dodgeInput )
        {
            dodgeInput = false;
            animatorManager.animator.SetBool("rollInput", true);
            if(!isInteracting && !isRolling)
                actionManager.Roll();
        }
    }

    private void HandleMovementInput()
    {
        cameraInputX = mouseInput.x;
        cameraInputY = mouseInput.y;

        horizontalInput = isInteracting ? movementInput.x : movementInput.x;
        verticalInput = isInteracting ? movementInput.y: movementInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(horizontalInput, moveAmount);
    } 

    void HandleMouse()
    {
        if(changeStatsInput){
            changeStatsInput = false;
            if(Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void HandleChangeWeapon()
    {
        if(changeWeaponInput)
        {
            changeWeaponInput = false;
            equipmentManager.weaponToLoad++;
            if(equipmentManager.weaponToLoad >= equipmentManager.gameManager.equipment.Length)
                equipmentManager.weaponToLoad = 0;
            equipmentManager.gameManager.DestroyItem(equipmentManager.gameManager.spawnedWeapon);
            equipmentManager.gameManager.LoadItem(equipmentManager.weaponToLoad, "Weapon");
        }
    }

    void HandleChangeLeftWeapon()
    {
        if(changeLeftWeaponInput)
        {
            changeLeftWeaponInput = false;
            equipmentManager.weaponLeftToLoad++;
            if(equipmentManager.weaponLeftToLoad >= equipmentManager.gameManager.equipment.Length)
                equipmentManager.weaponLeftToLoad = 0;
            equipmentManager.gameManager.DestroyItem(equipmentManager.gameManager.spawnedShield);
            equipmentManager.gameManager.LoadItem(equipmentManager.weaponLeftToLoad, "Shield");
        }
    }

    private void HandleAction()
    {
        if(!isComboing)
        {
            if(interactInput)
            {
                animatorManager.animator.SetBool("heavyInput", false);
                interactInput = false;
                actionManager.Use();
            }
            else if(heavyInput)
            {
                animatorManager.animator.SetBool("heavyInput", true);
                heavyInput = false;
                actionManager.UseHeavy();
            }
            else if(leftInput)
            {
                animatorManager.animator.SetBool("heavyInput", false);
                leftInput = false;
                actionManager.UseLeft();
            }
            else if(specialInput)
            {
                animatorManager.animator.SetBool("heavyInput", true);
                specialInput = false;
                actionManager.UseSpecial();
            }
        }
    }

    private void HandleControlScheme()
    {
        if(changeSchemeInput)
        {
            if(isController)
            {
                isController = false;
                playerControls.bindingMask = InputBinding.MaskByGroup(playerControls.controlSchemes.First(x => x.name == "Keys").bindingGroup);
            }
            else
            {
                isController = true;
                playerControls.bindingMask = InputBinding.MaskByGroup(playerControls.controlSchemes.First(x => x.name == "Controller").bindingGroup);
            }
        }
    }
}