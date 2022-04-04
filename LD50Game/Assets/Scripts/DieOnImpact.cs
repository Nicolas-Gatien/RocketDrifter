using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieOnImpact : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if(other != null)
            {
                Die();
            }
        }
    }

    void Die()
    {
        FindObjectOfType<GameManager>().Explode(transform);

        if (this.gameObject.CompareTag("Player"))
        {
            MenuManager.SendScore(MenuManager.playerName);
            FindObjectOfType<SceneLoader>().StartCoroutine("RestartGame");
        }
        if (this.gameObject.CompareTag("Enemy"))
        {
            GetComponent<Enemy>().Die();
            return;
        }
        Destroy(gameObject);

    }
}
