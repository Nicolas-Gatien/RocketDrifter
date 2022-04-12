using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithRadar : IMovementBehavior
{
    EnemyBase enemy;
    Rigidbody2D rb;

    public float curSpeed;
    public float accelerationSpeed;
    public float maxSpeed;

    public MoveWithRadar(EnemyBase _enemy, Rigidbody2D _rb, float _accelerationSpeed, float _maxSpeed)
    {
        enemy = _enemy;
        rb = _rb;
        accelerationSpeed = _accelerationSpeed;
        maxSpeed = _maxSpeed;

    }

    public void Move(GameObject gameObject)
    {
        Transform transform = gameObject.transform;

        // Moving The Ship
        Vector2 moveVelocity = transform.up * accelerationSpeed;
        rb.velocity += moveVelocity;

        // Capping Speed
        curSpeed = rb.velocity.magnitude;
        if (curSpeed > maxSpeed)
        {
            float reduction = maxSpeed / curSpeed;
            rb.velocity *= reduction;
        }
    }
}
