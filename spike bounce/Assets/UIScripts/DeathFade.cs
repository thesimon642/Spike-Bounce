using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathFade : MonoBehaviour
{
    public Image fadescreen;
    private readonly float fadeSpeed = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        fadescreen.color = new Color(0,0,0,0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerController.deathPhase == 4)
        {
            fadescreen.color = new Color(0,0,0,Mathf.MoveTowards(fadescreen.color.a,5,fadeSpeed*Time.deltaTime));
            if (fadescreen.color.a >= 5 - (Time.deltaTime * fadeSpeed))
            {
                PlayerController.deathPhase = 5;
                SceneManager.LoadScene("ScoreMenu");
            }
        }
    }
}
