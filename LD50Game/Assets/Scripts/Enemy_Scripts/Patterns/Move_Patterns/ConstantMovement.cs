using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameManager;

public class ConstantMovement : IMovementBehavior
{
    Enemy enemy;
    Rigidbody2D rb;
    Transform transform;
    GameObject gameObject;

    public float curSpeed;
    public float accelerationSpeed;
    public float maxSpeed;

    public float turnSpeed;

    public float radarSize;
    public int safety;

    public ConstantMovement(Enemy _enemy, float _accelerationSpeed, float _maxSpeed, float _turnSpeed)
    {
        enemy = _enemy;
        accelerationSpeed = _accelerationSpeed;
        maxSpeed = _maxSpeed;
    }

    public void Move(GameObject _gameObject)
    {
        // Setting Variables
        gameObject = _gameObject;
        transform = gameObject.transform;
        rb = gameObject.GetComponent<Rigidbody2D>();

        ApplyVelocity();
    }

    void ApplyDirection()
    {
        List<Collider2D> enemies = new List<Collider2D>(EnemiesInRange(radarSize, ENEMY_LAYER));  // Put all nearby Enemies Into a List
        enemies.Remove(gameObject.GetComponent<Collider2D>()); // Remove itself from that list

        float angleTowardsPlayer = GetAngleTowards(player.transform);
        float angleAwayFromEnemies = 0;

        // Calculate Angle Away From Enemies
        for (int i = 0; i < enemies.Count; i++)
        {
            angleAwayFromEnemies = (angleAwayFromEnemies + (GetAngleAway(enemies[i].gameObject.transform)) / 2);
        }

        float targetAngle;
        if (enemies.Count > 0)
        {
            targetAngle = (angleTowardsPlayer + (angleAwayFromEnemies * (safety - 1))) / safety;
        }
        else
        {
            targetAngle = angleTowardsPlayer;
        }
        enemies.Clear();
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle - 90);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
    }


    void ApplyVelocity()
    {
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

    float GetAngleTowards(Transform _target)
    {
        Vector3 diff = _target.position - transform.position;
        diff.Normalize();

        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return rotZ;
    }

    float GetAngleAway(Transform _target)
    {
        Vector3 diff = transform.position - _target.position;
        diff.Normalize();

        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return rotZ;
    }

    Collider2D[] EnemiesInRange (float _radarSize, LayerMask _avoidThese)
    {
        return Physics2D.OverlapCircleAll(transform.position, _radarSize, _avoidThese);
    }
}
