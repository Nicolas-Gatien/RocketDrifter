using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Die", lifeTime);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
