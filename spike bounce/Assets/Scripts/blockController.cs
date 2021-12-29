using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D playerHopefully)
    {
        if (playerHopefully.gameObject.tag == "Player"&&PlayerController.amIPurple)
        {
            PlayerController.justDied = true;
        }
    }
}
