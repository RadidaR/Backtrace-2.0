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
                    ""name"": ""Choice"",
                    ""type"": ""Button"",
                    ""id"": ""b6c0988b-89de-410a-bf17-93b087c582fd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""24717970-5ba7-4640-9a23-7d4ad2169ed7"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Choice"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0ce7efb4-00af-4668-8570-385991d27a8b"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Choice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f3d0283b-1658-4e45-ba8e-13d45c79a027"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Choice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""f7e5e412-ddac-4bcd-a340-4dc12b2f61a4"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Choice"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""8985bf55-498f-4b64-be9b-a3f6882973dc"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Choice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ac12ebf6-883d-4290-919c-352c616487a6"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Choice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Play
            m_Play = asset.FindActionMap("Play", throwIfNotFound: true);
            m_Play_Choice = m_Play.FindAction("Choice", throwIfNotFound: true);
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
        private readonly InputAction m_Play_Choice;
        public struct PlayActions
        {
            private @Input m_Wrapper;
            public PlayActions(@Input wrapper) { m_Wrapper = wrapper; }
            public InputAction @Choice => m_Wrapper.m_Play_Choice;
            public InputActionMap Get() { return m_Wrapper.m_Play; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayActions set) { return set.Get(); }
            public void SetCallbacks(IPlayActions instance)
            {
                if (m_Wrapper.m_PlayActionsCallbackInterface != null)
                {
                    @Choice.started -= m_Wrapper.m_PlayActionsCallbackInterface.OnChoice;
                    @Choice.performed -= m_Wrapper.m_PlayActionsCallbackInterface.OnChoice;
                    @Choice.canceled -= m_Wrapper.m_PlayActionsCallbackInterface.OnChoice;
                }
                m_Wrapper.m_PlayActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Choice.started += instance.OnChoice;
                    @Choice.performed += instance.OnChoice;
                    @Choice.canceled += instance.OnChoice;
                }
            }
        }
        public PlayActions @Play => new PlayActions(this);
        public interface IPlayActions
        {
            void OnChoice(InputAction.CallbackContext context);
        }
    }
}
