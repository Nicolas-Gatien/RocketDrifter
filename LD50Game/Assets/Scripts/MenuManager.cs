using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static string playerName;
    public TMP_InputField input;
    public TextMeshProUGUI highScoreText;
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void PlayButton()
    {
        playerName = input.text;
        sceneLoader.StartCoroutine("NextLevel");
    }

    private void Update()
    {
        highScoreText.text = "Local Highscore: " + PlayerPrefs.GetInt("highscore");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public static void SendScore(string name)
    {
        HighScores.UploadScore(name, ScoreManager.score);
    }
}
