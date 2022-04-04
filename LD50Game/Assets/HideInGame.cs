using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HideInGame : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            transform.position = Vector2.zero;

        }else
        {
            transform.position = new Vector2(500, 500);

        }
    }
}
