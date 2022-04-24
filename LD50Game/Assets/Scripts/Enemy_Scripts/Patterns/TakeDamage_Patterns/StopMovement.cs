using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMovement : ITakeDamageBehavior
{
    Enemy enemy;
    Rigidbody2D rb;
    GameObject gameObject;
    Transform transform;
    SpriteRenderer renderer;

    Shader whiteShader;
    Shader defaultShader;

    public StopMovement(Enemy _enemy)
    {
        enemy = _enemy;
    }

    public void TakeDamage(GameObject _gameObject, int damage)
    {
        gameObject = _gameObject;
        transform = _gameObject.transform;
        rb = _gameObject.GetComponent<Rigidbody2D>();
        renderer = _gameObject.GetComponent<SpriteRenderer>();
        whiteShader = Shader.Find("GUI/Text Shader");
        defaultShader = Shader.Find("Sprites/Default");

        rb.velocity = Vector2.zero;

        enemy.health -= damage;
        WhiteSprite();
        enemy.StartCoroutine(NormalSprite(0.1f));
    }

    void WhiteSprite()
    {
        renderer.material.shader = whiteShader;
        renderer.color = Color.white;
    }

    IEnumerator NormalSprite(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        renderer.material.shader = defaultShader;
        renderer.color = Color.white;
    }
}
