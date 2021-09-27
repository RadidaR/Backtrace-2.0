using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using MPUIKIT;
using TMPro;
using MEC;
using UnityEngine.SceneManagement;

//public enum contentType

namespace MMC
{
    public class GameManager : MonoBehaviour
    {
        public int statMax;
        public int physical;
        public int mental;
        public int spiritual;

        public int score;
        public int baseScore;
        public int comboMultiplier;

        public int swipes;

        public bool gameOver;
        public bool paused;

        public GameEvent eLeftPressed;
        public GameEvent eRightPressed;
        public GameEvent eSwipe;
        public GameEvent eWindowMissed;
        public GameEvent eGameOver;
        public GameEvent ePause;
        public GameEvent eUnpause;

        Input input;

        public float buildUp;
        public float inputWindow;

        public float windowDecreasePercent;
        public float buildUpDecreasePercent;

        public float minWindow;
        public float minBuildUp;

        float startingWindow;
        float startingBuildUp;


        public bool openWindow;
        public bool swiping;

        public Slider physicalSlider;
        public Slider mentalSlider;
        public Slider spiritualSlider;

        public MPImage stateTimer;
        public SpriteRenderer shareToFeed;

        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI highscoreText;
        public MPImage comboImage;
        public TextMeshProUGUI multiplierText;
        public GameObject newHighScore;

        public Color start;
        public Color end;


        public void AddPhysical(int add) => physical += add;
        public void AddMental(int add) => mental += add;
        public void AddSpiritual(int add) => spiritual += add;

        public void UpdateStats(int addP, int addM, int addS)
        {
            swipes++;
            physical += addP;
            mental += addM;
            spiritual += addS;
            physicalSlider.value = physical;
            mentalSlider.value = mental;
            spiritualSlider.value = spiritual;

            bool multiplierGoing = false;
            if (physical == statMax - 2 || physical == 2)
            {
                multiplierGoing = true;
                comboMultiplier++;
            }
            else if (physical == statMax - 1 || physical == 1)
            {
                multiplierGoing = true;
                comboMultiplier += 2;
            }

            if (mental == statMax - 2 || mental == 2)
            {
                multiplierGoing = true;
                comboMultiplier++;
            }
            else if (mental == statMax - 1 || mental == 1)
            {
                multiplierGoing = true;
                comboMultiplier += 2;
            }

            if (spiritual == statMax - 2 || spiritual == 2)
            {
                multiplierGoing = true;
                comboMultiplier++;
            }
            else if (spiritual == statMax - 1 || spiritual == 1)
            {
                multiplierGoing = true;
                comboMultiplier += 2;
            }

            if (!multiplierGoing)
                comboMultiplier = 1;

            scoreText.text = $"Score: {score}";
            multiplierText.text = $"x{comboMultiplier}";
            multiplierText.color = start.LerpToColor(end, comboMultiplier * 0.1f);
            multiplierText.fontSize = Mathf.Lerp(24, 48, comboMultiplier * 0.1f);

            if (comboMultiplier > 1)
            {
                multiplierText.gameObject.SetActive(true);
                comboImage.gameObject.SetActive(true);
            }
            else
            {
                multiplierText.gameObject.SetActive(false);
                comboImage.gameObject.SetActive(false);
            }

            if (physical <= 0 || physical >= statMax || mental <= 0 || mental >= statMax || spiritual <= 0 || spiritual >= statMax)
                GameOver();

            if (inputWindow <= minWindow)
                inputWindow = minWindow;
            else
                inputWindow = startingWindow - (swipes * windowDecreasePercent);

            if (buildUp <= minBuildUp)
                buildUp = minBuildUp;
            else
                buildUp = startingBuildUp - (swipes * buildUpDecreasePercent);


        }

        void GameOver()
        {
            eGameOver.Raise();
            gameOver = true;

            Color a = stateTimer.color;
            a.a = 0;
            stateTimer.color = a;


            Time.timeScale = 0;

            int highscore = PlayerPrefs.GetInt("Highscore", 0);

            if (score > highscore)
            {
                newHighScore.SetActive(true);
                PlayerPrefs.SetInt("Highscore", score);
            }

        }

        private void Awake()
        {
            gameOver = false;
            paused = false;
            Time.timeScale = 1;
            input = new Input();
            score = 0;
            comboMultiplier = 1;
            swipes = 0;

            startingWindow = inputWindow;
            startingBuildUp = buildUp;

            physical = Mathf.RoundToInt(statMax / 2);
            mental = Mathf.RoundToInt(statMax / 2);
            spiritual = Mathf.RoundToInt(statMax / 2);

            input.Play.Choice.performed += ctx => PlayerInput();
            input.Play.Pause.performed += ctx => EscapePressed();
            //input.Play.Right.performed += ctx => eRightPressed.Raise();

            physicalSlider.maxValue = statMax;
            mentalSlider.maxValue = statMax;
            spiritualSlider.maxValue = statMax;

            physicalSlider.value = physical;
            mentalSlider.value = mental;
            spiritualSlider.value = spiritual;

            highscoreText.text = $"{PlayerPrefs.GetInt("Highscore", 000000)}";
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

        void EscapePressed()
        {
            if (gameOver)
                return;

            if (!paused)
            {
                Time.timeScale = 0;
                ePause.Raise();
                paused = true;
            }
            else
            {
                Unpause();
            }
        }

        public void Unpause()
        {
            Time.timeScale = 1;
            eUnpause.Raise();
            paused = false;

        }


        void PlayerInput()
        {
            if (gameOver)
                return;

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
            shareToFeed.color = Color.yellow;
            Color a = stateTimer.color;
            a.a = 0.3f;
            stateTimer.color = a;
            float startValue = stateTimer.fillAmount;
            float timer = buildUp + 0.25f;
            while (timer >= 0)
            {
                yield return Timing.WaitForSeconds(Time.deltaTime);
                timer -= Time.deltaTime;
                stateTimer.fillAmount = Mathf.Lerp(startValue, 1, 1 - timer / buildUp + 0.25f);
            }
            //yield return Timing.WaitForSeconds(buildUp + 0.25f);
            Timing.RunCoroutine(_InputWindow(), Segment.Update);
        }

        IEnumerator<float> _InputWindow()
        {
            openWindow = true;
            swiping = false;
            shareToFeed.color = Color.green;
            stateTimer.fillAmount = 1;
            stateTimer.color = Color.white;
            float timer = inputWindow;
            while (timer >= 0)
            {
                yield return Timing.WaitForSeconds(Time.deltaTime);
                timer -= Time.deltaTime;
                stateTimer.fillAmount = Mathf.Lerp(1, 0, 1 - timer / inputWindow);
                if (!openWindow)
                {
                    score += baseScore * comboMultiplier;
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
            comboMultiplier = 1;
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

        public void LoadScene(int scene)
        {
            SceneManager.LoadScene(scene);
        }

    }
}
