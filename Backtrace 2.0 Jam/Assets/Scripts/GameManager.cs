using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//public enum contentType

namespace MMC
{
    public class GameManager : MonoBehaviour
    {
        [Range(0, 20)] public int physical;
        [Range(0, 20)] public int mental;
        [Range(0, 20)] public int spiritual;

        public GameEvent eLeftPressed;
        public GameEvent eRightPressed;

        Input input;


        public void AddPhysical(int add) => physical += add;
        public void AddMental(int add) => mental += add;
        public void AddSpiritual(int add) => spiritual += add;

        private void Awake()
        {
            input = new Input();
            physical = 9;
            mental = 9;
            spiritual = 9;

            input.Play.Left.performed += ctx => eLeftPressed.Raise();
            input.Play.Right.performed += ctx => eRightPressed.Raise();
        }

        private void OnEnable()
        {
            input.Enable();
        }

        private void OnDisable()
        {
            input.Disable();
        }

    }
}