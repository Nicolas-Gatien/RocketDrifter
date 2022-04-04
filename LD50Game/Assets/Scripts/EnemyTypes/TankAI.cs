using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAI : Enemy
{
    public GameObject engineParticle;
    public Transform anchor;

    void Update()
    {
        LookAtPlayerAndAvoidEnemies();
        MoveForwards();
        Instantiate(engineParticle, anchor.position, anchor.rotation);

    }

    public override void Die()
    {
        base.Die();
        FindObjectOfType<WaveSpawner>().SpawnEnemy();
    }
}
