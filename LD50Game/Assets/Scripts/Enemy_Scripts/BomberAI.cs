using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberAI : Enemy
{
    public BomberAI()
    {
        movementBehavior = new ConstantMovement(this, 0.2f, 8, 5, 0, 1);
        deathBehavior = new Explode(this);
        takeDamageBehavior = new StopMovement(this);
    }

    public BomberAI(IMovementBehavior mb, IDeathBehavior db, ITakeDamageBehavior tdb)
    {
        movementBehavior = mb;
        deathBehavior = db;
        takeDamageBehavior = tdb;
    }

    public GameObject engineParticle;

    public Transform leftAnchor;
    public Transform rightAnchor;

    // Update is called once per frame
    void Update()
    {
        base.Update();
        Instantiate(engineParticle, leftAnchor.position, leftAnchor.rotation);
        Instantiate(engineParticle, rightAnchor.position, rightAnchor.rotation);
    }
}
