//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/InputManager/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""PlayerMovement"",
            ""id"": ""3fba4ad5-8cee-4dff-8bfd-d417c18d9156"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3d6de44d-7612-4d37-a3e7-128ec5127a07"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Mouse"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a81a7dd5-d595-4916-8d4a-d85b2e6005e4"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""9c07d6f9-e350-4bf4-80b3-53aa704ca80b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ebf18968-5c54-4766-9e0c-4b5901e1c4b0"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""af9294f7-92fe-42ec-a965-40be1b1fd8e3"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""00fbef67-49dc-4735-b920-b4bdb1393dae"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""acfb5e9f-64b1-409c-a0c6-ff63585e009d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e82d91f5-42e5-4588-aaf7-7b9178fa2619"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca1eedd8-6ade-4d7a-845f-97be8e94bb20"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerActions"",
            ""id"": ""90bb0b4e-feb2-43ff-9e55-f9c19d7c931a"",
            ""actions"": [
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""8482f6c5-5b67-40ee-98be-164fb364999f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""6471094b-bf24-4d7a-9efd-fbad0130a535"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseWheel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9d978c81-ad3c-4ba7-97da-26e9df5b0272"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Toolbar1"",
                    ""type"": ""Button"",
                    ""id"": ""4d666802-bce6-4b40-85ee-13c0ab77df43"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Toolbar2"",
                    ""type"": ""Button"",
                    ""id"": ""fc77d080-cd75-4edc-9e1b-a5f8ff5bf587"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Toolbar3"",
                    ""type"": ""Button"",
                    ""id"": ""9be16b31-86e5-4d94-9700-561cc7bb1855"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Toolbar4"",
                    ""type"": ""Button"",
                    ""id"": ""299bcde9-5064-4cf5-8fde-21c5a8daad2c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Toolbar5"",
                    ""type"": ""Button"",
                    ""id"": ""a4040673-28b6-4384-9b13-2dedfc676937"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Toolbar6"",
                    ""type"": ""Button"",
                    ""id"": ""d3f0e0c0-5930-450d-be9e-db256fb906f0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Toolbar7"",
                    ""type"": ""Button"",
                    ""id"": ""dd4094ee-dff0-4a9d-ba84-7cb7385f546a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f8dae371-eceb-4ed5-8a2b-f935d0b562be"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""45ab3c6f-5cbf-4e08-83f5-a84a5cc09a3e"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""973ff371-d6bb-402b-ac37-ab96addfc476"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""949b73e1-bf23-4c5e-b414-be739bf6292c"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toolbar1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a1fe31e-5fdf-4c2b-932b-b5b1dd020292"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toolbar2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""480668d5-4a19-4078-a992-17403d6614b0"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toolbar3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c5d2ae1c-857e-4adb-9320-a44958dddb84"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toolbar4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a55cd356-5471-40b3-bd68-a440c98ab747"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toolbar5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""919b820d-ed3d-457b-a756-bc6c0486221f"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toolbar6"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b203c260-abaf-471c-89d7-4d3696d984b6"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toolbar7"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa9b32d9-75d2-4b3d-a9fe-54d01d4cd5f0"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toolbar7"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f169fb5e-05e0-4bfa-ba5c-d2e19b119a44"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerMovement
        m_PlayerMovement = asset.FindActionMap("PlayerMovement", throwIfNotFound: true);
        m_PlayerMovement_Movement = m_PlayerMovement.FindAction("Movement", throwIfNotFound: true);
        m_PlayerMovement_Mouse = m_PlayerMovement.FindAction("Mouse", throwIfNotFound: true);
        // PlayerActions
        m_PlayerActions = asset.FindActionMap("PlayerActions", throwIfNotFound: true);
        m_PlayerActions_Interact = m_PlayerActions.FindAction("Interact", throwIfNotFound: true);
        m_PlayerActions_Jump = m_PlayerActions.FindAction("Jump", throwIfNotFound: true);
        m_PlayerActions_MouseWheel = m_PlayerActions.FindAction("MouseWheel", throwIfNotFound: true);
        m_PlayerActions_Toolbar1 = m_PlayerActions.FindAction("Toolbar1", throwIfNotFound: true);
        m_PlayerActions_Toolbar2 = m_PlayerActions.FindAction("Toolbar2", throwIfNotFound: true);
        m_PlayerActions_Toolbar3 = m_PlayerActions.FindAction("Toolbar3", throwIfNotFound: true);
        m_PlayerActions_Toolbar4 = m_PlayerActions.FindAction("Toolbar4", throwIfNotFound: true);
        m_PlayerActions_Toolbar5 = m_PlayerActions.FindAction("Toolbar5", throwIfNotFound: true);
        m_PlayerActions_Toolbar6 = m_PlayerActions.FindAction("Toolbar6", throwIfNotFound: true);
        m_PlayerActions_Toolbar7 = m_PlayerActions.FindAction("Toolbar7", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerMovement
    private readonly InputActionMap m_PlayerMovement;
    private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
    private readonly InputAction m_PlayerMovement_Movement;
    private readonly InputAction m_PlayerMovement_Mouse;
    public struct PlayerMovementActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerMovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerMovement_Movement;
        public InputAction @Mouse => m_Wrapper.m_PlayerMovement_Mouse;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                @Mouse.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMouse;
                @Mouse.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMouse;
                @Mouse.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMouse;
            }
            m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Mouse.started += instance.OnMouse;
                @Mouse.performed += instance.OnMouse;
                @Mouse.canceled += instance.OnMouse;
            }
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);

    // PlayerActions
    private readonly InputActionMap m_PlayerActions;
    private IPlayerActionsActions m_PlayerActionsActionsCallbackInterface;
    private readonly InputAction m_PlayerActions_Interact;
    private readonly InputAction m_PlayerActions_Jump;
    private readonly InputAction m_PlayerActions_MouseWheel;
    private readonly InputAction m_PlayerActions_Toolbar1;
    private readonly InputAction m_PlayerActions_Toolbar2;
    private readonly InputAction m_PlayerActions_Toolbar3;
    private readonly InputAction m_PlayerActions_Toolbar4;
    private readonly InputAction m_PlayerActions_Toolbar5;
    private readonly InputAction m_PlayerActions_Toolbar6;
    private readonly InputAction m_PlayerActions_Toolbar7;
    public struct PlayerActionsActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActionsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Interact => m_Wrapper.m_PlayerActions_Interact;
        public InputAction @Jump => m_Wrapper.m_PlayerActions_Jump;
        public InputAction @MouseWheel => m_Wrapper.m_PlayerActions_MouseWheel;
        public InputAction @Toolbar1 => m_Wrapper.m_PlayerActions_Toolbar1;
        public InputAction @Toolbar2 => m_Wrapper.m_PlayerActions_Toolbar2;
        public InputAction @Toolbar3 => m_Wrapper.m_PlayerActions_Toolbar3;
        public InputAction @Toolbar4 => m_Wrapper.m_PlayerActions_Toolbar4;
        public InputAction @Toolbar5 => m_Wrapper.m_PlayerActions_Toolbar5;
        public InputAction @Toolbar6 => m_Wrapper.m_PlayerActions_Toolbar6;
        public InputAction @Toolbar7 => m_Wrapper.m_PlayerActions_Toolbar7;
        public InputActionMap Get() { return m_Wrapper.m_PlayerActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActionsActions instance)
        {
            if (m_Wrapper.m_PlayerActionsActionsCallbackInterface != null)
            {
                @Interact.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnInteract;
                @Jump.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnJump;
                @MouseWheel.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMouseWheel;
                @MouseWheel.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMouseWheel;
                @MouseWheel.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnMouseWheel;
                @Toolbar1.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnToolbar1;
                @Toolbar1.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnToolbar1;
                @Toolbar1.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnToolbar1;
                @Toolbar2.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnToolbar2;
                @Toolbar2.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnToolbar2;
                @Toolbar2.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnToolbar2;
                @Toolbar3.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnToolbar3;
                @Toolbar3.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnToolbar3;
                @Toolbar3.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnToolbar3;
                @Toolbar4.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnToolbar4;
                @Toolbar4.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnToolbar4;
                @Toolbar4.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnToolbar4;
                @Toolbar5.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnToolbar5;
                @Toolbar5.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnToolbar5;
                @Toolbar5.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnToolbar5;
                @Toolbar6.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnToolbar6;
                @Toolbar6.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnToolbar6;
                @Toolbar6.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnToolbar6;
                @Toolbar7.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnToolbar7;
                @Toolbar7.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnToolbar7;
                @Toolbar7.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnToolbar7;
            }
            m_Wrapper.m_PlayerActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @MouseWheel.started += instance.OnMouseWheel;
                @MouseWheel.performed += instance.OnMouseWheel;
                @MouseWheel.canceled += instance.OnMouseWheel;
                @Toolbar1.started += instance.OnToolbar1;
                @Toolbar1.performed += instance.OnToolbar1;
                @Toolbar1.canceled += instance.OnToolbar1;
                @Toolbar2.started += instance.OnToolbar2;
                @Toolbar2.performed += instance.OnToolbar2;
                @Toolbar2.canceled += instance.OnToolbar2;
                @Toolbar3.started += instance.OnToolbar3;
                @Toolbar3.performed += instance.OnToolbar3;
                @Toolbar3.canceled += instance.OnToolbar3;
                @Toolbar4.started += instance.OnToolbar4;
                @Toolbar4.performed += instance.OnToolbar4;
                @Toolbar4.canceled += instance.OnToolbar4;
                @Toolbar5.started += instance.OnToolbar5;
                @Toolbar5.performed += instance.OnToolbar5;
                @Toolbar5.canceled += instance.OnToolbar5;
                @Toolbar6.started += instance.OnToolbar6;
                @Toolbar6.performed += instance.OnToolbar6;
                @Toolbar6.canceled += instance.OnToolbar6;
                @Toolbar7.started += instance.OnToolbar7;
                @Toolbar7.performed += instance.OnToolbar7;
                @Toolbar7.canceled += instance.OnToolbar7;
            }
        }
    }
    public PlayerActionsActions @PlayerActions => new PlayerActionsActions(this);
    public interface IPlayerMovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnMouse(InputAction.CallbackContext context);
    }
    public interface IPlayerActionsActions
    {
        void OnInteract(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnMouseWheel(InputAction.CallbackContext context);
        void OnToolbar1(InputAction.CallbackContext context);
        void OnToolbar2(InputAction.CallbackContext context);
        void OnToolbar3(InputAction.CallbackContext context);
        void OnToolbar4(InputAction.CallbackContext context);
        void OnToolbar5(InputAction.CallbackContext context);
        void OnToolbar6(InputAction.CallbackContext context);
        void OnToolbar7(InputAction.CallbackContext context);
    }
}
