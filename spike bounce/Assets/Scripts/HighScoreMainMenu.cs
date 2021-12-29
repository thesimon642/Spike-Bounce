using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreMainMenu : MonoBehaviour
{
    public Text highscore;
    // Update is called once per frame
    void Update()
    {
        highscore.text ="Highscore: " + ScoreSaver.highScore.ToString();
    }
}
