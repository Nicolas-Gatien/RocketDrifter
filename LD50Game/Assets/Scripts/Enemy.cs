using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Enemy : MonoBehaviour
{
    public static GameObject player;

    public int health;
    public int maxHealth;

    public float turnSpeed;
    public float maxSpeed;
    public float moveSpeed;
    public float radarSize;
    public int safetyPriority;
    protected float curSpeed;

    public LayerMask avoidThese;

    int turnDirection;
    protected Rigidbody2D rb;

    private SpriteRenderer myRenderer;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;

    public int score;

    AudioSource source;

    protected void Start()
    {
        source = GetComponent<AudioSource>();
        if (Random.Range(0, 2) == 0)
        {
            turnDirection = -1;
        }else
        {
            turnDirection = 1;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default"); // or whatever sprite shader is being used
    }

    private void LateUpdate()
    {
        if(health <= 0)
        {
            Die();
        }
    }

    protected void LookAtPlayer()
    {
        float rotZ = GetAngleDifference(player.transform);
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, rotZ - 90);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
    }

    protected void LookAtPlayerAndAvoidEnemies()
    {
        float rotZ = GetAngleDifference(player.transform);
        float enemiesRotZ = 0;

        Collider2D[] tempEnemies = Physics2D.OverlapCircleAll(transform.position, radarSize, avoidThese);
        List<Collider2D> enemies = new List<Collider2D>(tempEnemies);
        enemies.Remove(GetComponent<Collider2D>());
        Debug.Log(enemies.Count);
        for (int i = 0; i < enemies.Count; i++)
        {
            enemiesRotZ = (enemiesRotZ + (GetAngleAway(enemies[i].gameObject.transform)) / 2);
        }
        float targetAngle;
        if (enemies.Count > 0)
        {
           targetAngle = (((rotZ * (safetyPriority - 1)) + enemiesRotZ) / safetyPriority) * turnDirection;
        }else
        {
            targetAngle = rotZ;
        }
        enemies.Clear();
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle - 90);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
    }

    protected void MoveForwards()
    {
        // Moving The Ship
        Vector2 moveVelocity = transform.up * moveSpeed;
        rb.velocity += moveVelocity;

        // Capping Speed
        curSpeed = rb.velocity.magnitude;
        if (curSpeed > maxSpeed)
        {
            float reduction = maxSpeed / curSpeed;
            rb.velocity *= reduction;
        }
    }

    float GetAngleDifference(Transform target)
    {
        Vector3 diff = target.position - transform.position;
        diff.Normalize();

        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return rotZ;
    }

    float GetAngleAway(Transform target)
    {
        Vector3 diff = transform.position - target.position;
        diff.Normalize();

        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return rotZ;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage();
            Destroy(other.gameObject);
        }
    }

    void TakeDamage()
    {
        if(source != null)
        {
            source.Play();
        }
        health--;
        WhiteSprite();
        rb.velocity = Vector2.zero;
        Invoke("NormalSprite", 0.1f);
    }

    public virtual void Die()
    {
        FindObjectOfType<GameManager>().Explode(transform);
        CalculateScore();
        FindObjectOfType<ScreenShaker>().Shake();
        Destroy(gameObject);
    }

    void WhiteSprite()
    {
        myRenderer.material.shader = shaderGUItext;
        myRenderer.color = Color.white;
    }

    void NormalSprite()
    {
        myRenderer.material.shader = shaderSpritesDefault;
        myRenderer.color = Color.white;
    }

    void CalculateScore()
    {
        score = (int)(((maxHealth * turnSpeed * maxSpeed) / turnSpeed) * curSpeed);
        if(ScoreManager.isPlayerMoving == true)
        {
            score *= (int)FindObjectOfType<PlayerMovement>().curSpeed;
        }
        score /= 10;
        score += 5;
        ScoreManager.score += score;
        FindObjectOfType<GameManager>().DisplayScore(score, transform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radarSize);
    }
}
