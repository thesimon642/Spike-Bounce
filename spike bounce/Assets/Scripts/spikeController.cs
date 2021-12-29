using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D playerHopefully)
    {
        if (playerHopefully.gameObject.tag =="Player")
        {
            PlayerController.justDied = true;
        }
    }
}

