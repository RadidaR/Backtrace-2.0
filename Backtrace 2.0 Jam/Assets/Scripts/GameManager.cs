using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MEC;

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
        public GameEvent eSwipe;
        public GameEvent eWindowMissed;

        Input input;

        public float buildUp;
        public float inputWindow;
        public bool openWindow;
        public bool swiping;


        public void AddPhysical(int add) => physical += add;
        public void AddMental(int add) => mental += add;
        public void AddSpiritual(int add) => spiritual += add;

        public void UpdateStats(int addP, int addM, int addS)
        {
            physical += addP;
            mental += addM;
            spiritual += addS;
        }

        private void Awake()
        {
            input = new Input();
            physical = 9;
            mental = 9;
            spiritual = 9;

            input.Play.Choice.performed += ctx => PlayerInput();
            //input.Play.Right.performed += ctx => eRightPressed.Raise();

            Timing.RunCoroutine(_BuildUp(), Segment.Update);
        }

        private void OnEnable()
        {
            input.Enable();
        }

        private void OnDisable()
        {
            input.Disable();
        }

        void PlayerInput()
        {
            if (!openWindow)
                return;

            if (input.Play.Choice.ReadValue<float>() == -1)
            {
                //Debug.Log("Left");
                eLeftPressed.Raise();
                openWindow = false;
            }
            else if (input.Play.Choice.ReadValue<float>() == 1)
            {
                //Debug.Log("Right");
                eRightPressed.Raise();
                openWindow = false;
            }
        }

        IEnumerator<float> _BuildUp()
        {
            openWindow = false;
            swiping = true;
            yield return Timing.WaitForSeconds(buildUp);
            Timing.RunCoroutine(_InputWindow(), Segment.Update);
        }

        IEnumerator<float> _InputWindow()
        {
            openWindow = true;
            swiping = false;
            float timer = inputWindow;
            while (timer >= 0)
            {
                yield return Timing.WaitForSeconds(Time.deltaTime);
                timer -= Time.deltaTime;
                if (!openWindow)
                {
                    eSwipe.Raise();
                    Timing.RunCoroutine(_BuildUp(), Segment.Update);
                    yield break;
                }

            }
            if (openWindow)
            {
                Punish();
            }

            eSwipe.Raise();
            Timing.RunCoroutine(_BuildUp(), Segment.Update);
        }

        void Punish()
        {
            if (physical <= 11)
                AddPhysical(-1);
            else
                AddPhysical(1);

            if (mental <= 7)
                AddMental(-1);
            else
                AddMental(1);

            if (spiritual <= 8)
                AddSpiritual(-1);
            else
                AddSpiritual(1);

            eWindowMissed.Raise();
        }

    }
}
