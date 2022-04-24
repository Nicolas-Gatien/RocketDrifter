using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();

    public float startTimeBtwSpawn;
    public float timeBtwSpawn;

    public GameObject tank;
    public GameObject bomber;
    public GameObject fighter;

    public int enemiesSpawned;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeBtwSpawn -= Time.deltaTime;
        if(timeBtwSpawn <= 0)
        {
            SpawnEnemy();

            timeBtwSpawn = startTimeBtwSpawn;
        }

        if(enemiesSpawned >= 10)
        {
            enemies.Insert(1, bomber);
        }
        if (enemiesSpawned >= 20)
        {
            enemies.Insert(2, fighter);
        }
    }

    public void SpawnEnemy()
    {
        enemiesSpawned++;
        float xPos = Random.Range(Object.topRightCorner.x, Object.bottomLeftCorner.x);
        float yPos = Random.Range(Object.topRightCorner.y, Object.bottomLeftCorner.y);

        if (Random.Range(0, 2) == 0)
        {
            if (xPos < 0)
            {
                xPos = Object.bottomLeftCorner.x;
            }
            else
            {
                xPos = Object.topRightCorner.x;
            }
        }
        else
        {
            if (yPos < 0)
            {
                yPos = Object.bottomLeftCorner.y;
            }
            else
            {
                yPos = Object.topRightCorner.y;
            }
        }
        startTimeBtwSpawn *= 0.99f;

        Instantiate(enemies[Random.Range(0, enemies.Count)], new Vector2(xPos, yPos), Quaternion.identity);
    }
}
