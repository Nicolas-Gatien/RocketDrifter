using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject scoreIndicator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<SceneLoader>().StartCoroutine("PreviousLevel");
        }
    }

    public void Explode(Transform position)
    {
        Instantiate(explosionPrefab, position.position, Quaternion.identity);
    }

    public void DisplayScore(int score, Transform position)
    {
        GameObject scoreInd = Instantiate(scoreIndicator, position.position, Quaternion.identity);
        scoreInd.GetComponent<ScoreIndicator>().score = score;
    }
}
