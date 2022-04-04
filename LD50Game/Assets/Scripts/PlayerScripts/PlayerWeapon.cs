using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform shootPoint;
    public float bulletSpeed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject curBullet = Instantiate(bullet, shootPoint.position, shootPoint.localRotation);
            curBullet.GetComponent<Rigidbody2D>().velocity = shootPoint.up * bulletSpeed;
        }
    }
}
