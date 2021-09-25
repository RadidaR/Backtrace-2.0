// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace MMC
{
    public class @Input : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @Input()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input"",
    ""maps"": [
        {
            ""name"": ""Play"",
            ""id"": ""a2feca35-2238-4f4d-aeb3-bb4bcb2ae36f"",
            ""actions"": [
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""b6c0988b-89de-410a-bf17-93b087c582fd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""3f28bff3-d176-41f2-b429-6345f5a1924c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""efbdec2a-40f8-49c5-b4c7-96e42d423c76"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""428888e9-d0bf-4ed2-bb5e-5a2e2b1b4244"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Play
            m_Play = asset.FindActionMap("Play", throwIfNotFound: true);
            m_Play_Left = m_Play.FindAction("Left", throwIfNotFound: true);
            m_Play_Right = m_Play.FindAction("Right", throwIfNotFound: true);
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

        // Play
        private readonly InputActionMap m_Play;
        private IPlayActions m_PlayActionsCallbackInterface;
        private readonly InputAction m_Play_Left;
        private readonly InputAction m_Play_Right;
        public struct PlayActions
        {
            private @Input m_Wrapper;
            public PlayActions(@Input wrapper) { m_Wrapper = wrapper; }
            public InputAction @Left => m_Wrapper.m_Play_Left;
            public InputAction @Right => m_Wrapper.m_Play_Right;
            public InputActionMap Get() { return m_Wrapper.m_Play; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayActions set) { return set.Get(); }
            public void SetCallbacks(IPlayActions instance)
            {
                if (m_Wrapper.m_PlayActionsCallbackInterface != null)
                {
                    @Left.started -= m_Wrapper.m_PlayActionsCallbackInterface.OnLeft;
                    @Left.performed -= m_Wrapper.m_PlayActionsCallbackInterface.OnLeft;
                    @Left.canceled -= m_Wrapper.m_PlayActionsCallbackInterface.OnLeft;
                    @Right.started -= m_Wrapper.m_PlayActionsCallbackInterface.OnRight;
                    @Right.performed -= m_Wrapper.m_PlayActionsCallbackInterface.OnRight;
                    @Right.canceled -= m_Wrapper.m_PlayActionsCallbackInterface.OnRight;
                }
                m_Wrapper.m_PlayActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Left.started += instance.OnLeft;
                    @Left.performed += instance.OnLeft;
                    @Left.canceled += instance.OnLeft;
                    @Right.started += instance.OnRight;
                    @Right.performed += instance.OnRight;
                    @Right.canceled += instance.OnRight;
                }
            }
        }
        public PlayActions @Play => new PlayActions(this);
        public interface IPlayActions
        {
            void OnLeft(InputAction.CallbackContext context);
            void OnRight(InputAction.CallbackContext context);
        }
    }
}
