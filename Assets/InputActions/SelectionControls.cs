// GENERATED AUTOMATICALLY FROM 'Assets/InputActions/SelectionControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @SelectionControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @SelectionControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""SelectionControls"",
    ""maps"": [
        {
            ""name"": ""Selection"",
            ""id"": ""72c78356-f47e-4657-8535-bf7b0d3f11d5"",
            ""actions"": [
                {
                    ""name"": ""Next"",
                    ""type"": ""Button"",
                    ""id"": ""06e80efd-e27a-486d-8f01-46113c4e7270"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Prev"",
                    ""type"": ""Button"",
                    ""id"": ""2b390191-bd09-41a7-a4f8-1b4d87d05878"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ready"",
                    ""type"": ""Button"",
                    ""id"": ""33f869f8-af51-44d8-9947-634f6538bfa4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""5d53d18a-30c1-42c2-a9c4-019ddbdb50dd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""33cfa729-52a2-43bf-a4cb-17edb9f41d2f"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Next"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f54810c8-c346-488c-b809-3183cdd98d26"",
                    ""path"": ""<Joystick>/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Next"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e90aa9a-4bf8-4895-9b68-0bbffc871372"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Next"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6cc306c8-aa53-49af-8054-80ca9c32c3cd"",
                    ""path"": ""<XInputController>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Next"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0ee0abb5-1faa-4615-847b-d086948aa1df"",
                    ""path"": ""<HID::Logitech Logitech Dual Action>/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Next"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff33126a-4821-4188-9f06-b23966c3fd19"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Prev"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b45d4cf0-688c-4370-b2d3-31203e024769"",
                    ""path"": ""<Joystick>/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Prev"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3044a0e0-8d79-45c2-9ddb-2e9cbeaf5e8d"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Prev"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aeb29cc3-41c0-4a99-8f83-83e572766336"",
                    ""path"": ""<XInputController>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Prev"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a056a6fe-2b4f-4364-9c51-67fd5d684004"",
                    ""path"": ""<HID::Logitech Logitech Dual Action>/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Prev"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""56941ec5-b989-4dc1-891b-3ed867f74409"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ready"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca33ab53-98aa-425c-b176-f06fd8015e2a"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ready"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a7841adb-bb07-4958-8107-9ac83eb5ab87"",
                    ""path"": ""<HID::Logitech Logitech Dual Action>/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ready"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c86df03-4ca2-4624-8651-6ce3a0753354"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43d1b12a-fa6f-40e8-b0be-e7ba687a9807"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b1795b13-3a8b-4f79-a82c-05d609bb1155"",
                    ""path"": ""<HID::Logitech Logitech Dual Action>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Selection
        m_Selection = asset.FindActionMap("Selection", throwIfNotFound: true);
        m_Selection_Next = m_Selection.FindAction("Next", throwIfNotFound: true);
        m_Selection_Prev = m_Selection.FindAction("Prev", throwIfNotFound: true);
        m_Selection_Ready = m_Selection.FindAction("Ready", throwIfNotFound: true);
        m_Selection_Back = m_Selection.FindAction("Back", throwIfNotFound: true);
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

    // Selection
    private readonly InputActionMap m_Selection;
    private ISelectionActions m_SelectionActionsCallbackInterface;
    private readonly InputAction m_Selection_Next;
    private readonly InputAction m_Selection_Prev;
    private readonly InputAction m_Selection_Ready;
    private readonly InputAction m_Selection_Back;
    public struct SelectionActions
    {
        private @SelectionControls m_Wrapper;
        public SelectionActions(@SelectionControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Next => m_Wrapper.m_Selection_Next;
        public InputAction @Prev => m_Wrapper.m_Selection_Prev;
        public InputAction @Ready => m_Wrapper.m_Selection_Ready;
        public InputAction @Back => m_Wrapper.m_Selection_Back;
        public InputActionMap Get() { return m_Wrapper.m_Selection; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SelectionActions set) { return set.Get(); }
        public void SetCallbacks(ISelectionActions instance)
        {
            if (m_Wrapper.m_SelectionActionsCallbackInterface != null)
            {
                @Next.started -= m_Wrapper.m_SelectionActionsCallbackInterface.OnNext;
                @Next.performed -= m_Wrapper.m_SelectionActionsCallbackInterface.OnNext;
                @Next.canceled -= m_Wrapper.m_SelectionActionsCallbackInterface.OnNext;
                @Prev.started -= m_Wrapper.m_SelectionActionsCallbackInterface.OnPrev;
                @Prev.performed -= m_Wrapper.m_SelectionActionsCallbackInterface.OnPrev;
                @Prev.canceled -= m_Wrapper.m_SelectionActionsCallbackInterface.OnPrev;
                @Ready.started -= m_Wrapper.m_SelectionActionsCallbackInterface.OnReady;
                @Ready.performed -= m_Wrapper.m_SelectionActionsCallbackInterface.OnReady;
                @Ready.canceled -= m_Wrapper.m_SelectionActionsCallbackInterface.OnReady;
                @Back.started -= m_Wrapper.m_SelectionActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_SelectionActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_SelectionActionsCallbackInterface.OnBack;
            }
            m_Wrapper.m_SelectionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Next.started += instance.OnNext;
                @Next.performed += instance.OnNext;
                @Next.canceled += instance.OnNext;
                @Prev.started += instance.OnPrev;
                @Prev.performed += instance.OnPrev;
                @Prev.canceled += instance.OnPrev;
                @Ready.started += instance.OnReady;
                @Ready.performed += instance.OnReady;
                @Ready.canceled += instance.OnReady;
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
            }
        }
    }
    public SelectionActions @Selection => new SelectionActions(this);
    public interface ISelectionActions
    {
        void OnNext(InputAction.CallbackContext context);
        void OnPrev(InputAction.CallbackContext context);
        void OnReady(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
    }
}
