using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playButton : MonoBehaviour
{
    private bool spaceUp;
    private void Update()
    {
        if (Input.GetAxis("Jump") == 0)
        { spaceUp = true; }
        if (SceneManager.GetActiveScene().name == "ScoreMenu" && Input.GetAxis("Jump") != 0 && spaceUp)
        {
            SceneManager.LoadScene("BaseGame");
        }
    }

    private void Start()
    {
        spaceUp = false;
    }
    public void ButtonPress()
    {
        SceneManager.LoadScene("BaseGame");


    }
}
