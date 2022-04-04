using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public static Vector2 topRightCorner;
    public static Vector2 bottomLeftCorner;

    private void Awake()
    {
        topRightCorner = new Vector2(18, 10);
        bottomLeftCorner = new Vector2(-18, -10);
    }

    void Update()
    {
        if (transform.position.y > (topRightCorner.y + transform.localScale.y))
        {
            transform.position = new Vector2(transform.position.x, bottomLeftCorner.y - transform.localScale.y);
        }
        if (transform.position.y < (bottomLeftCorner.y - transform.localScale.y))
        {
            transform.position = new Vector2(transform.position.x, topRightCorner.y + transform.localScale.y);
        }
        if (transform.position.x > (topRightCorner.x + transform.localScale.x))
        {
            transform.position = new Vector2(bottomLeftCorner.x - transform.localScale.x, transform.position.y);
        }
        if (transform.position.x < (bottomLeftCorner.x - transform.localScale.x))
        {
            transform.position = new Vector2(topRightCorner.x + transform.localScale.x, transform.position.y);
        }

    }
}
