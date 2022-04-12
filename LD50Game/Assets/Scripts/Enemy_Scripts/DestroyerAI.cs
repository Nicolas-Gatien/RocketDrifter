using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerAI : EnemyBase
{
    public float timeBtwEnemyCall;
    float timeLeftBeforeCall;

    // Start is called before the first frame update
    void Update()
    {
        LookAtPlayer();
        MoveForwards();
        timeLeftBeforeCall += Time.deltaTime;
        if(timeLeftBeforeCall > timeBtwEnemyCall)
        {
            Call();
        }
    }

    void Call()
    {
        FindObjectOfType<WaveSpawner>().SpawnEnemy();
        FindObjectOfType<WaveSpawner>().SpawnEnemy();
        timeLeftBeforeCall = 0;
    }

    public override void Die()
    {
        base.Die();
        FindObjectOfType<WaveSpawner>().SpawnEnemy();
        FindObjectOfType<WaveSpawner>().SpawnEnemy();
        FindObjectOfType<WaveSpawner>().SpawnEnemy();
        FindObjectOfType<WaveSpawner>().SpawnEnemy();
        FindObjectOfType<WaveSpawner>().SpawnEnemy();
    }
}
