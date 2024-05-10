//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Evan/_Controller.inputactions
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

public partial class @_Controller: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @_Controller()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""_Controller"",
    ""maps"": [
        {
            ""name"": ""Controller"",
            ""id"": ""592c012c-e48e-426c-ab75-fc619ad1a2bc"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""eeec7ca9-c7ad-41c1-bdb4-42d055918b45"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""33f0d700-1759-414a-b7cf-f361b2ea3011"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""96c73967-2f2e-4839-9557-b9185b681efb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""bd0c22f0-8f8a-44f1-bf9e-9a46db1dd405"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dodge"",
                    ""type"": ""Button"",
                    ""id"": ""42280f65-af00-49bd-af1e-d85644837d19"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Turn"",
                    ""type"": ""Value"",
                    ""id"": ""f15b0f92-13b5-4907-a8d6-786b128a352c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ShiftLock"",
                    ""type"": ""Button"",
                    ""id"": ""e79631ee-4174-4e59-934d-b5f795197a0a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CheckRecipes"",
                    ""type"": ""Button"",
                    ""id"": ""c3f95a68-9577-46d9-ac0a-6f6bd848a09e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DropItem"",
                    ""type"": ""Value"",
                    ""id"": ""c2d11cde-a5aa-4864-b0ab-ad85d0679a51"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""91711a01-ed99-4c87-a12f-2841c2c03e3e"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8b79097-7928-48e7-9c25-3be794406476"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fd6f8076-23db-4682-aa9e-e6f7c075e6c3"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3eded7fd-1534-41a2-beef-e10d0086bf48"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""61fced52-0577-48d5-b60b-77394a7e6818"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a9ffbc1c-e696-43a7-a039-ef1509953e46"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f64f3ea-38d5-4466-b21a-82933352a51a"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShiftLock"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b8d63d9-a535-464f-b658-974ce25ae13b"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CheckRecipes"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe52e90d-283f-482e-b943-f72cb1473870"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DropItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Craft"",
            ""id"": ""3fce51a4-b0aa-47b6-897e-ad52a489cc12"",
            ""actions"": [
                {
                    ""name"": ""CraftBandages"",
                    ""type"": ""Button"",
                    ""id"": ""07254118-297e-468d-93ce-f5ea2eead6eb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CraftClub"",
                    ""type"": ""Button"",
                    ""id"": ""678f752a-7443-40c0-bc5d-24b5fa973530"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7bfac60b-426d-4b1c-9415-ecb992077c2e"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CraftBandages"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a037e9dc-de05-497e-878c-2c98ee54492f"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CraftClub"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Controller
        m_Controller = asset.FindActionMap("Controller", throwIfNotFound: true);
        m_Controller_Jump = m_Controller.FindAction("Jump", throwIfNotFound: true);
        m_Controller_Move = m_Controller.FindAction("Move", throwIfNotFound: true);
        m_Controller_Sprint = m_Controller.FindAction("Sprint", throwIfNotFound: true);
        m_Controller_Attack = m_Controller.FindAction("Attack", throwIfNotFound: true);
        m_Controller_Dodge = m_Controller.FindAction("Dodge", throwIfNotFound: true);
        m_Controller_Turn = m_Controller.FindAction("Turn", throwIfNotFound: true);
        m_Controller_ShiftLock = m_Controller.FindAction("ShiftLock", throwIfNotFound: true);
        m_Controller_CheckRecipes = m_Controller.FindAction("CheckRecipes", throwIfNotFound: true);
        m_Controller_DropItem = m_Controller.FindAction("DropItem", throwIfNotFound: true);
        // Craft
        m_Craft = asset.FindActionMap("Craft", throwIfNotFound: true);
        m_Craft_CraftBandages = m_Craft.FindAction("CraftBandages", throwIfNotFound: true);
        m_Craft_CraftClub = m_Craft.FindAction("CraftClub", throwIfNotFound: true);
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

    // Controller
    private readonly InputActionMap m_Controller;
    private List<IControllerActions> m_ControllerActionsCallbackInterfaces = new List<IControllerActions>();
    private readonly InputAction m_Controller_Jump;
    private readonly InputAction m_Controller_Move;
    private readonly InputAction m_Controller_Sprint;
    private readonly InputAction m_Controller_Attack;
    private readonly InputAction m_Controller_Dodge;
    private readonly InputAction m_Controller_Turn;
    private readonly InputAction m_Controller_ShiftLock;
    private readonly InputAction m_Controller_CheckRecipes;
    private readonly InputAction m_Controller_DropItem;
    public struct ControllerActions
    {
        private @_Controller m_Wrapper;
        public ControllerActions(@_Controller wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Controller_Jump;
        public InputAction @Move => m_Wrapper.m_Controller_Move;
        public InputAction @Sprint => m_Wrapper.m_Controller_Sprint;
        public InputAction @Attack => m_Wrapper.m_Controller_Attack;
        public InputAction @Dodge => m_Wrapper.m_Controller_Dodge;
        public InputAction @Turn => m_Wrapper.m_Controller_Turn;
        public InputAction @ShiftLock => m_Wrapper.m_Controller_ShiftLock;
        public InputAction @CheckRecipes => m_Wrapper.m_Controller_CheckRecipes;
        public InputAction @DropItem => m_Wrapper.m_Controller_DropItem;
        public InputActionMap Get() { return m_Wrapper.m_Controller; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ControllerActions set) { return set.Get(); }
        public void AddCallbacks(IControllerActions instance)
        {
            if (instance == null || m_Wrapper.m_ControllerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_ControllerActionsCallbackInterfaces.Add(instance);
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Sprint.started += instance.OnSprint;
            @Sprint.performed += instance.OnSprint;
            @Sprint.canceled += instance.OnSprint;
            @Attack.started += instance.OnAttack;
            @Attack.performed += instance.OnAttack;
            @Attack.canceled += instance.OnAttack;
            @Dodge.started += instance.OnDodge;
            @Dodge.performed += instance.OnDodge;
            @Dodge.canceled += instance.OnDodge;
            @Turn.started += instance.OnTurn;
            @Turn.performed += instance.OnTurn;
            @Turn.canceled += instance.OnTurn;
            @ShiftLock.started += instance.OnShiftLock;
            @ShiftLock.performed += instance.OnShiftLock;
            @ShiftLock.canceled += instance.OnShiftLock;
            @CheckRecipes.started += instance.OnCheckRecipes;
            @CheckRecipes.performed += instance.OnCheckRecipes;
            @CheckRecipes.canceled += instance.OnCheckRecipes;
            @DropItem.started += instance.OnDropItem;
            @DropItem.performed += instance.OnDropItem;
            @DropItem.canceled += instance.OnDropItem;
        }

        private void UnregisterCallbacks(IControllerActions instance)
        {
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Sprint.started -= instance.OnSprint;
            @Sprint.performed -= instance.OnSprint;
            @Sprint.canceled -= instance.OnSprint;
            @Attack.started -= instance.OnAttack;
            @Attack.performed -= instance.OnAttack;
            @Attack.canceled -= instance.OnAttack;
            @Dodge.started -= instance.OnDodge;
            @Dodge.performed -= instance.OnDodge;
            @Dodge.canceled -= instance.OnDodge;
            @Turn.started -= instance.OnTurn;
            @Turn.performed -= instance.OnTurn;
            @Turn.canceled -= instance.OnTurn;
            @ShiftLock.started -= instance.OnShiftLock;
            @ShiftLock.performed -= instance.OnShiftLock;
            @ShiftLock.canceled -= instance.OnShiftLock;
            @CheckRecipes.started -= instance.OnCheckRecipes;
            @CheckRecipes.performed -= instance.OnCheckRecipes;
            @CheckRecipes.canceled -= instance.OnCheckRecipes;
            @DropItem.started -= instance.OnDropItem;
            @DropItem.performed -= instance.OnDropItem;
            @DropItem.canceled -= instance.OnDropItem;
        }

        public void RemoveCallbacks(IControllerActions instance)
        {
            if (m_Wrapper.m_ControllerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IControllerActions instance)
        {
            foreach (var item in m_Wrapper.m_ControllerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_ControllerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public ControllerActions @Controller => new ControllerActions(this);

    // Craft
    private readonly InputActionMap m_Craft;
    private List<ICraftActions> m_CraftActionsCallbackInterfaces = new List<ICraftActions>();
    private readonly InputAction m_Craft_CraftBandages;
    private readonly InputAction m_Craft_CraftClub;
    public struct CraftActions
    {
        private @_Controller m_Wrapper;
        public CraftActions(@_Controller wrapper) { m_Wrapper = wrapper; }
        public InputAction @CraftBandages => m_Wrapper.m_Craft_CraftBandages;
        public InputAction @CraftClub => m_Wrapper.m_Craft_CraftClub;
        public InputActionMap Get() { return m_Wrapper.m_Craft; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CraftActions set) { return set.Get(); }
        public void AddCallbacks(ICraftActions instance)
        {
            if (instance == null || m_Wrapper.m_CraftActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CraftActionsCallbackInterfaces.Add(instance);
            @CraftBandages.started += instance.OnCraftBandages;
            @CraftBandages.performed += instance.OnCraftBandages;
            @CraftBandages.canceled += instance.OnCraftBandages;
            @CraftClub.started += instance.OnCraftClub;
            @CraftClub.performed += instance.OnCraftClub;
            @CraftClub.canceled += instance.OnCraftClub;
        }

        private void UnregisterCallbacks(ICraftActions instance)
        {
            @CraftBandages.started -= instance.OnCraftBandages;
            @CraftBandages.performed -= instance.OnCraftBandages;
            @CraftBandages.canceled -= instance.OnCraftBandages;
            @CraftClub.started -= instance.OnCraftClub;
            @CraftClub.performed -= instance.OnCraftClub;
            @CraftClub.canceled -= instance.OnCraftClub;
        }

        public void RemoveCallbacks(ICraftActions instance)
        {
            if (m_Wrapper.m_CraftActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICraftActions instance)
        {
            foreach (var item in m_Wrapper.m_CraftActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CraftActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CraftActions @Craft => new CraftActions(this);
    public interface IControllerActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnDodge(InputAction.CallbackContext context);
        void OnTurn(InputAction.CallbackContext context);
        void OnShiftLock(InputAction.CallbackContext context);
        void OnCheckRecipes(InputAction.CallbackContext context);
        void OnDropItem(InputAction.CallbackContext context);
    }
    public interface ICraftActions
    {
        void OnCraftBandages(InputAction.CallbackContext context);
        void OnCraftClub(InputAction.CallbackContext context);
    }
}
