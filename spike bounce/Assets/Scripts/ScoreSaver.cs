using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreSaver : MonoBehaviour
{
    public static int score = 0;
    public static int highScore = 0;
    public static ScoreSaver instance;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;
            highScore = PlayerPrefs.GetInt("High Score");
        }
        else
        { Destroy(gameObject); }
    }

    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().name == "BaseGame")
        {
            
            score = ScoreController.scoreValue;
        }
        highScore = Mathf.Max(highScore,score);
    }
}
