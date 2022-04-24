using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAI : Enemy
{
    // Constructor
    public TankAI()
    {
        movementBehavior = new ConstantMovement(this, 0.2f, 3, 3, 2, 2);
        deathBehavior = new Explode();
        takeDamageBehavior = new StopMovement(this);
    }

    public TankAI(IMovementBehavior mb, IDeathBehavior db, ITakeDamageBehavior tdb)
    {
        movementBehavior = mb;
        deathBehavior = db;
        takeDamageBehavior = tdb;
    }

    public GameObject engineParticle;
    public Transform anchor;

    void Update()
    {
        base.Update();
        Instantiate(engineParticle, anchor.position, anchor.rotation);
    }

    /*public override void Die()
    {
        base.Die();
        FindObjectOfType<WaveSpawner>().SpawnEnemy();
    }*/
}
