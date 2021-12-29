using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Transform playerPosition;
    public Transform cameraPosition;
    // Update is called once per frame
    void FixedUpdate()
    {
        //player can move in left of camera freely but otherwise will be followed if tries to cross half way
        if (playerPosition.position.x > cameraPosition.transform.position.x)
        {
            cameraPosition.position = new Vector3(playerPosition.position.x,cameraPosition.position.y,cameraPosition.position.z);
        }
    }
}
