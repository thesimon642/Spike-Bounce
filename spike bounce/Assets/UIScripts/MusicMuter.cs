using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicMuter : MonoBehaviour
{

    public AudioSource music;
    public Image linethrough;
    private void Start()
    {
        linethrough.color = new Color(0, 0, 0, 0);
    }
    public void ButtonPress()
    {
        if (music.volume == 0)
        {
            music.volume = 0.05f;
            linethrough.color = new Color(0,0,0,0);
        }
        else
        {
            music.volume = 0;
            linethrough.color = new Color(0, 0, 0, 255);
        }
    }
}
