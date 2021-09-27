using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScoreText;
    AudioManager audio;
    private void Awake()
    {
        audio = FindObjectOfType<AudioManager>();
        PlaySound("MenuMusic");
        highScoreText.text = $"{PlayerPrefs.GetInt("Highscore", 0)}";
    }
    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }


    public void PlaySound(string name) => audio.PlaySound(name);
    public void StopSound(string name) => audio.StopSound(name);


}
