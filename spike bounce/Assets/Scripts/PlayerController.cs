using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool firstUpdate = true;
    public Rigidbody2D rb;
    public static bool justDied = false;
    private readonly float moveSpeed = 10f;
    private bool goingUp = false;
    private int bounce;
    private readonly float bounceSpeed = 30f;
    private float previousVerticalSpeed=-1;
    public SpriteRenderer thisSpriteRender;
    public Sprite notPurple;
    public Sprite purple;
    public static bool amIPurple;
    private bool didIjustBecomePurple;
    public static PlayerController instance;
    public static bool doABounce = false;

    public AudioSource jumpSound;
    public AudioSource KillEnemySound;
    public AudioSource explodeSound;
    public AudioSource hitSound;

    public static int deathPhase;
    private float deathphase1counter;

    public ParticleSystem deathParticles;
    public static int score;
    void Awake()
    {
        instance = this;
        doABounce = false;
        previousVerticalSpeed = -1;
        goingUp = false;
        justDied = false;
        firstUpdate = true;
        deathPhase = 0;
        deathphase1counter = 0;
        score = 0;
        amIPurple = false;
        didIjustBecomePurple = false;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f)) ;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerController.justDied == false)
        {
            
            //controlling bouncing
            bounce = 0;
            if (goingUp)
            {
                //if hit head on ceiling, set velocity to zero
                if (previousVerticalSpeed == rb.velocity.y && previousVerticalSpeed <= 0.1f)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0f);
                }

                //if going up, look to go down
                if (rb.velocity.y <= 0)
                {

                    goingUp = false;
                }
            }
            else
            {
                //if going down, wait to come to stop, now is time to bounce so gain a high upward velocity
                if (rb.velocity.y >= -Time.deltaTime)
                {
                    goingUp = true;
                    bounce = 1;
                }

            }
            if (firstUpdate)
            {
                bounce = 0;
                firstUpdate = false;
            }
            //allows other things to cause a bounce
            if (doABounce)
            {
                bounce = 1;
                doABounce = false;
            }
            //controlling sideways movement

            if (bounce == 1)
            {
                rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, bounceSpeed);
            }
            else
            {
                rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
            }

            previousVerticalSpeed = rb.velocity.y;

            //handles changing modes

            if (Input.GetAxis("Jump") != 0)
            {
                if (!didIjustBecomePurple)
                {
                    //controls turning 45 degrees
                    if (!amIPurple)
                    { transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, transform.rotation.z + 45f)); }
                    else
                    { transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, transform.rotation.z)); }

                    didIjustBecomePurple = true;
                    amIPurple = !amIPurple;
                }
            }
            else
            {
                didIjustBecomePurple = false;
            }


            if (amIPurple)
            { thisSpriteRender.sprite = purple; }
            else
            { thisSpriteRender.sprite = notPurple; }

            if (bounce == 1&&firstUpdate==false)
            {
                jumpSound.Play();
            }

        }
        else
        {

            switch (deathPhase)
            {
                case 0:
                    rb.velocity = new Vector2(0f, 0f);
                    rb.gravityScale = 0f;
                    deathPhase = 1;
                    hitSound.Play();
                    break;
                case 1:
                    deathphase1counter += 1f;
                    if (deathphase1counter >= 50f)
                    { deathPhase = 2; }
                    break;
                case 2:
                    explodeSound.Play();
                    deathParticles.Play();
                    thisSpriteRender.sprite = null;
                    deathPhase = 3;
                    deathphase1counter = 0f;
                    break;
                case 3:
                    deathphase1counter += 1f;
                    if (deathphase1counter >= 180)
                    { deathPhase = 4; }
                    break;
            }
        }
    }
}
