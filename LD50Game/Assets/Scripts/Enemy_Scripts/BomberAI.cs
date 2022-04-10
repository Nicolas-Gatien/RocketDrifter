using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberAI : Enemy
{
    public GameObject engineParticle;

    public Transform leftAnchor;
    public Transform rightAnchor;

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        MoveForwards();
        Instantiate(engineParticle, leftAnchor.position, leftAnchor.rotation);
        Instantiate(engineParticle, rightAnchor.position, rightAnchor.rotation);
    }
}
