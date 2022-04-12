using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterAI : EnemyBase
{
    public GameObject engineParticle;
    Transform fighterTarget;

    public Transform leftAnchor;
    public Transform rightAnchor;

    public Transform leftFirePoint;
    public Transform rightFirePoint;

    public Transform aimLocation;
    public float aimRadius;

    public LayerMask shootAt;

    public GameObject bullet;
    public float bulletSpeed;

    public float shootDelay;
    public float startShootDelay;

    void Start()
    {
        base.Start();
        fighterTarget = GameObject.FindGameObjectWithTag("FighterTarget").transform;
    }

    // Update is called once per frame
    void Update()
    {
        shootDelay -= Time.deltaTime;
        StaySafe();
        MoveForwards();
        Instantiate(engineParticle, leftAnchor.position, leftAnchor.rotation);
        Instantiate(engineParticle, rightAnchor.position, rightAnchor.rotation);

        bool shoot = Physics2D.OverlapCircle(aimLocation.position, aimRadius, shootAt);
        if (shoot == true)
        {
            if (shootDelay <= 0)
            {
                GameObject curBullet = Instantiate(bullet, leftFirePoint.position, leftFirePoint.rotation);
                curBullet.GetComponent<Rigidbody2D>().velocity = leftFirePoint.up * bulletSpeed;

                curBullet = Instantiate(bullet, rightFirePoint.position, rightFirePoint.rotation);
                curBullet.GetComponent<Rigidbody2D>().velocity = rightFirePoint.up * bulletSpeed;

                shootDelay = startShootDelay;
            }
        }
    }

    void StaySafe()
    {
        float rotZ = GetAngleDifference(fighterTarget.transform);
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
            targetAngle = (((rotZ * (safetyPriority - 1)) + enemiesRotZ) / safetyPriority);
        }
        else
        {
            targetAngle = rotZ;
        }
        enemies.Clear();
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle - 90);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);

    }


    float GetAngleAway(Transform target)
    {
        Vector3 diff = transform.position - target.position;
        diff.Normalize();

        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return rotZ;
    }

    void FindTarget()
    {
        float rotZ = GetAngleDifference(fighterTarget);
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, rotZ - 90);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
    }

    float GetAngleDifference(Transform target)
    {
        Vector3 diff = target.position - transform.position;
        diff.Normalize();

        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return rotZ;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(aimLocation.position, aimRadius);
    }
}
