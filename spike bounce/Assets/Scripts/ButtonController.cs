using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonController : MonoBehaviour
{

    public static bool buttonsHaveBeenPressed;

    private void lateUpdate()
    {
        buttonsHaveBeenPressed = false;
    }

    private void Awake()
    {
        buttonsHaveBeenPressed = false;
    }

    public void ButtonDown()
    {
        buttonsHaveBeenPressed = true;
    }

    public void ButtonUp()
    {
        buttonsHaveBeenPressed = false;
    }

}
