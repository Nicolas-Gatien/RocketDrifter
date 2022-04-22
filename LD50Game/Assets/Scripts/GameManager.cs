using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject scoreIndicator;

    // Global Variables
    public static GameObject player;

    public static LayerMask ENEMY_LAYER;

    private void Start()
    {
        ENEMY_LAYER = LayerMask.GetMask("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
    }

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
