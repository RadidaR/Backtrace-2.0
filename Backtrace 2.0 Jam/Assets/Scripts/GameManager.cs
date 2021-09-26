using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using MEC;

//public enum contentType

namespace MMC
{
    public class GameManager : MonoBehaviour
    {
        public int statMax;
        public int physical;
        public int mental;
        public int spiritual;


        public GameEvent eLeftPressed;
        public GameEvent eRightPressed;
        public GameEvent eSwipe;
        public GameEvent eWindowMissed;

        Input input;

        public float buildUp;
        public float inputWindow;
        public bool openWindow;
        public bool swiping;

        public Slider physicalSlider;
        public Slider mentalSlider;
        public Slider spiritualSlider;


        public void AddPhysical(int add) => physical += add;
        public void AddMental(int add) => mental += add;
        public void AddSpiritual(int add) => spiritual += add;

        public void UpdateStats(int addP, int addM, int addS)
        {
            physical += addP;
            mental += addM;
            spiritual += addS;
            physicalSlider.value = physical;
            mentalSlider.value = mental;
            spiritualSlider.value = spiritual;
        }

        private void Awake()
        {
            input = new Input();
            physical = Mathf.RoundToInt(statMax / 2);
            mental = Mathf.RoundToInt(statMax / 2);
            spiritual = Mathf.RoundToInt(statMax / 2);

            input.Play.Choice.performed += ctx => PlayerInput();
            //input.Play.Right.performed += ctx => eRightPressed.Raise();

            physicalSlider.maxValue = statMax;
            mentalSlider.maxValue = statMax;
            spiritualSlider.maxValue = statMax;

            physicalSlider.value = physical;
            mentalSlider.value = mental;
            spiritualSlider.value = spiritual;
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
            yield return Timing.WaitForSeconds(buildUp + 0.25f);
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
            int p;
            int m;
            int s;
            if (physical <= statMax / 2)
                p = -1;
            else
                p = 1;

            if (mental <= statMax / 2)
                m = -1;
            else
                m = 1;

            if (spiritual <= statMax / 2)
                s = -1;
            else
                s = 1;

            eWindowMissed.Raise();
            UpdateStats(p, m, s);
        }

    }
}
