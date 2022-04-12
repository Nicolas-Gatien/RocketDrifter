using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Enemy : MonoBehaviour
{
    [Header("Behaviors")]
    public ITakeDamageBehavior takeDamageBehavior;
    public IMovementBehavior movementBehavior;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
