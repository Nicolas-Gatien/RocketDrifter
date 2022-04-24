using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : IDeathBehavior
{
    Enemy enemy;

    GameObject gameObject;
    Transform transfrom;

    public Explode(Enemy _enemy)
    {
        enemy = _enemy;
    }

    public void Die(GameObject _gameObject)
    {
        gameObject = _gameObject;
        transfrom = _gameObject.transform;

        enemy.CalculateScore();

        Object.FindObjectOfType<GameManager>().Explode(transfrom);
        Object.FindObjectOfType<ScreenShaker>().Shake();
        Object.Destroy(gameObject);
    }
}
