using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static bool isPlayerMoving;
    public static int score;

    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
        if (score > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", score);
        }
    }
}
