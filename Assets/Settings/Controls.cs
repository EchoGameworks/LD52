//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Settings/Controls.inputactions
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

public partial class @Controls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Level"",
            ""id"": ""466b5a97-40e0-47da-ace8-a65770ae2690"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""008409d2-bfd4-4f1b-812e-5e885e8b0ebd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""5bd401f8-7690-40c5-b837-985013c89ab7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""IconToggle"",
                    ""type"": ""Button"",
                    ""id"": ""284876ab-525d-4433-b3bb-49b4bd4966eb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skip"",
                    ""type"": ""Button"",
                    ""id"": ""2f543c98-0075-4a2d-ba79-91caff6807e6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Restart"",
                    ""type"": ""Button"",
                    ""id"": ""d3ad96d4-c8d3-43d2-9fca-4598f434fb31"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""01bdcc9e-1e10-4ee6-9737-20c7ef7dc043"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba90f4e9-406d-4b24-9415-7f5a052f3a58"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5bff2045-0783-4efd-aca6-212fc8b1966a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""IconToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""405d2da0-a164-4599-9663-f44ea6885435"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c315328-a74d-4eb8-bc42-e80429c3eb78"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Restart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Level
        m_Level = asset.FindActionMap("Level", throwIfNotFound: true);
        m_Level_Click = m_Level.FindAction("Click", throwIfNotFound: true);
        m_Level_MousePosition = m_Level.FindAction("MousePosition", throwIfNotFound: true);
        m_Level_IconToggle = m_Level.FindAction("IconToggle", throwIfNotFound: true);
        m_Level_Skip = m_Level.FindAction("Skip", throwIfNotFound: true);
        m_Level_Restart = m_Level.FindAction("Restart", throwIfNotFound: true);
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

    // Level
    private readonly InputActionMap m_Level;
    private ILevelActions m_LevelActionsCallbackInterface;
    private readonly InputAction m_Level_Click;
    private readonly InputAction m_Level_MousePosition;
    private readonly InputAction m_Level_IconToggle;
    private readonly InputAction m_Level_Skip;
    private readonly InputAction m_Level_Restart;
    public struct LevelActions
    {
        private @Controls m_Wrapper;
        public LevelActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Click => m_Wrapper.m_Level_Click;
        public InputAction @MousePosition => m_Wrapper.m_Level_MousePosition;
        public InputAction @IconToggle => m_Wrapper.m_Level_IconToggle;
        public InputAction @Skip => m_Wrapper.m_Level_Skip;
        public InputAction @Restart => m_Wrapper.m_Level_Restart;
        public InputActionMap Get() { return m_Wrapper.m_Level; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LevelActions set) { return set.Get(); }
        public void SetCallbacks(ILevelActions instance)
        {
            if (m_Wrapper.m_LevelActionsCallbackInterface != null)
            {
                @Click.started -= m_Wrapper.m_LevelActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_LevelActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_LevelActionsCallbackInterface.OnClick;
                @MousePosition.started -= m_Wrapper.m_LevelActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_LevelActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_LevelActionsCallbackInterface.OnMousePosition;
                @IconToggle.started -= m_Wrapper.m_LevelActionsCallbackInterface.OnIconToggle;
                @IconToggle.performed -= m_Wrapper.m_LevelActionsCallbackInterface.OnIconToggle;
                @IconToggle.canceled -= m_Wrapper.m_LevelActionsCallbackInterface.OnIconToggle;
                @Skip.started -= m_Wrapper.m_LevelActionsCallbackInterface.OnSkip;
                @Skip.performed -= m_Wrapper.m_LevelActionsCallbackInterface.OnSkip;
                @Skip.canceled -= m_Wrapper.m_LevelActionsCallbackInterface.OnSkip;
                @Restart.started -= m_Wrapper.m_LevelActionsCallbackInterface.OnRestart;
                @Restart.performed -= m_Wrapper.m_LevelActionsCallbackInterface.OnRestart;
                @Restart.canceled -= m_Wrapper.m_LevelActionsCallbackInterface.OnRestart;
            }
            m_Wrapper.m_LevelActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @IconToggle.started += instance.OnIconToggle;
                @IconToggle.performed += instance.OnIconToggle;
                @IconToggle.canceled += instance.OnIconToggle;
                @Skip.started += instance.OnSkip;
                @Skip.performed += instance.OnSkip;
                @Skip.canceled += instance.OnSkip;
                @Restart.started += instance.OnRestart;
                @Restart.performed += instance.OnRestart;
                @Restart.canceled += instance.OnRestart;
            }
        }
    }
    public LevelActions @Level => new LevelActions(this);
    public interface ILevelActions
    {
        void OnClick(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnIconToggle(InputAction.CallbackContext context);
        void OnSkip(InputAction.CallbackContext context);
        void OnRestart(InputAction.CallbackContext context);
    }
}
