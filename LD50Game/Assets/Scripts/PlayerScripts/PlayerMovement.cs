using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    public float maxSpeed = 20;
    public float moveSpeed = 5;
    public float turnSpeed;

    public float dashSpeed = 20;

    Vector2 movement;
    [HideInInspector]
    public float curSpeed;

    Animator anim;

    public Transform rightEngine;
    public Transform leftEngine;

    public GameObject engineParticle;
    public GameObject engineBurstParticle;

    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("dash");
            Vector2 dashVelocity = transform.up * dashSpeed;
            rb.velocity = dashVelocity;
            Instantiate(engineBurstParticle, transform.position, transform.rotation);
            //source.Play();
        }
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Vertical")); // Getting Input

        if(movement.y < 0)
        {
            Debug.Log("Going Backwards");
            anim.SetBool("movingBack", true);
        }else
        {
            anim.SetBool("movingBack", false);
        }
    }

    void FixedUpdate()
    {
        // Moving The Ship
        Vector2 moveVelocity = transform.up * moveSpeed * movement.y;
        rb.velocity += moveVelocity;



        // Turning the Ship
        transform.Rotate(0, 0, -(movement.x * turnSpeed) * Time.fixedDeltaTime);

        // Capping Speed
        curSpeed = rb.velocity.magnitude;
        if(curSpeed > moveSpeed)
        {
            ScoreManager.isPlayerMoving = true;
        }else
        {
            ScoreManager.isPlayerMoving = false;
        }

        if (movement.y > 0.1f || movement.y < -0.1f)
        {
            Instantiate(engineParticle, leftEngine.position, leftEngine.rotation);
            Instantiate(engineParticle, rightEngine.position, rightEngine.rotation);
        }
        else if(movement.x > 0.1f)
        {
            Instantiate(engineParticle, leftEngine.position, leftEngine.rotation);
        }
        else if(movement.x < -0.1f)
        {
            Instantiate(engineParticle, rightEngine.position, rightEngine.rotation);
        }

        if (curSpeed > maxSpeed)
        {
            float reduction = maxSpeed / curSpeed;
            rb.velocity *= reduction;
        }
    }
}
