using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScoreText;

    private void Awake()
    {
        highScoreText.text = $"{PlayerPrefs.GetInt("Highscore", 0)}";
    }
    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }


}
