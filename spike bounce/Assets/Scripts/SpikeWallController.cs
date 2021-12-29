using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWallController : MonoBehaviour
{
    public Rigidbody2D rb;
    private readonly float moveSpeed =2f;
    public Camera mainCamera;
    public Transform thisWall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerController.justDied == false)
        {
            //keeps constant velocity
            rb.velocity = new Vector2(moveSpeed, 0);
            //keeps on screen
            if (thisWall.transform.position.x <= mainCamera.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect + 1)
            {
                thisWall.position = new Vector3(mainCamera.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect + 1, mainCamera.transform.position.y, thisWall.position.z);
            }
        }
        else
        {
            rb.velocity = new Vector2(0f,0f);
        }
    }
 
    private void OnTriggerStay2D(Collider2D playerHopefully)
    {
        if (playerHopefully.gameObject.tag == "Player")
        {
            PlayerController.justDied = true;
        }
        if (playerHopefully.gameObject.tag == "Enemy")
        {
            Destroy(playerHopefully.gameObject);
        }
    }
}
