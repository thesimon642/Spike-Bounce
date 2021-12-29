using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float probabilityofchangingdirection;
    private float cumulativedrawtochangedirection;
    private bool movingRight = false;
    private readonly float speed = 3f;
    public Rigidbody2D rb;
    public SpriteRenderer movingSprite;
    public Sprite alive;
    public Sprite dead;
    private string mode = "Idle";

    public Transform playerPosition;
    public Transform myPosition;

    private float wiggle;

    private GameObject thisObject;
    void Awake()
    {
        //dying.SetBool("Dead", false);
        movingSprite.sprite = alive;
        playerPosition = PlayerController.instance.transform;
        mode = "Idle";
        rb.gravityScale = 1f;
        myPosition = this.transform;
        rb.mass = 1f;
        rb.gravityScale = 1f;
        wiggle = -0.000001f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerController.justDied == false)
        {

            switch (mode)
            {

                case "Walking":
                    //changes direction when moving
                    movingSprite.flipX = !movingRight;
                    //fixes getting stuck
                    rb.transform.position = new Vector2(rb.transform.position.x, rb.transform.position.y + 0.001f);
                    //handles movement
                    if (movingRight)
                    { rb.velocity = new Vector2(speed, rb.velocity.y); }
                    else
                    { rb.velocity = new Vector2(-speed, rb.velocity.y); }
                    //handles changing direction

                    cumulativedrawtochangedirection += Time.fixedDeltaTime * 0.5f;
                    if (cumulativedrawtochangedirection > probabilityofchangingdirection)
                    {
                        //if finally enough draw to change direction then swap and reset values
                        movingRight = !movingRight;
                        cumulativedrawtochangedirection = 0;
                        probabilityofchangingdirection = Random.Range(0f, 1f);
                    }
                    break;

                case "Idle":
                    if (playerPosition.position.x >= myPosition.position.x - Camera.main.orthographicSize * Camera.main.aspect)
                    { mode = "Walking"; }
                    break;
            }
        
        }
        else
        {
            rb.velocity = new Vector2(0f+wiggle, 0f);
            rb.mass = 100000;
            wiggle = -wiggle;
            rb.gravityScale = 0f;
        }
        
        //if (myPosition.position.x + 50 < playerPosition.position.x||myPosition.position.y+50<playerPosition.position.x)
        //{ Destroy(this.gameObject); }

    }

    private void OnCollisionEnter2D(Collision2D playerHopefully)
    {
        if (playerHopefully.gameObject.tag == "Player" )
        {
            PlayerController.doABounce = true;
            if (mode == "Walking")
            {
                //if the player is purple this sprite dies
                if (PlayerController.amIPurple)
                {
                    mode = "dead";
                    rb.velocity = new Vector2(0f, 0f);
                    rb.mass = 100000;
                    rb.gravityScale = 0f;
                    movingSprite.sprite = dead;
                    PlayerController.score += 50;
                    //dying.SetBool("Dead", true);
                }
                //if the player isn't the player dies
                else
                { 
                    PlayerController.justDied = true;
                }
            }
            
        }
    }
}
