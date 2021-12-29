using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonMenu : MonoBehaviour
{
    public void ButtonPress()
    {
        Application.Quit();
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
