using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour
{
    [Header("Behaviors")]
    public ITakeDamageBehavior takeDamageBehavior;
    public IDeathBehavior deathBehavior;
    public IMovementBehavior movementBehavior;

    [Header("Common Variables")]
    public int maxHealth;
    public int health;

    [Header("Components")]
    public Rigidbody2D rb;

    // Functionality Variables
    bool hasDied; // Used to make sure the death behavior is only called once

    public Enemy()
    {

    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    // Set Behavior Functions
    public void SetMovementBehavior(IMovementBehavior _movementBehavior)
    {
        movementBehavior = _movementBehavior;
    }

    public void SetDeathBehavior(IDeathBehavior _deathBehavior)
    {
        deathBehavior = _deathBehavior;
    }

    public void SetTakeDamageBehavior(ITakeDamageBehavior _takeDamageBehavior)
    {
        takeDamageBehavior = _takeDamageBehavior;
    }

    // Call Functions
    public void PerformMovement()
    {
        movementBehavior.Move(gameObject);
    }

    public void PerformDeath()
    {
        deathBehavior.Die(gameObject);
    }

    public void PerformTakeDamage(int _damage)
    {
        takeDamageBehavior.TakeDamage(gameObject, _damage);
    }

    // Universal Functions
    public void Update()
    {
        CheckHealth();
        PerformMovement();
    }

    // Checks Wheather The Ship Should Die or Not
    public void CheckHealth()
    {
        if (health <= 0 && hasDied == false)
        {
            hasDied = true;
            PerformDeath();
        }
    }

    public void CalculateScore()
    {
        float curSpeed = rb.velocity.magnitude;
        int score = (int)(maxHealth * curSpeed);

        if (ScoreManager.isPlayerMoving == true)
        {
            score *= (int)FindObjectOfType<PlayerMovement>().curSpeed;
        }

        score /= 5;
        ScoreManager.score += score;
        FindObjectOfType<GameManager>().DisplayScore(score, transform);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            PerformTakeDamage(1);
            Destroy(other.gameObject);
        }
    }
}
