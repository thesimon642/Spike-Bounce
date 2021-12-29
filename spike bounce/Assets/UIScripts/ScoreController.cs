using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreController : MonoBehaviour
{
    public Text scoreText;
    public static int scoreValue;
    // Update is called once per frame
    void Update()
    {
        scoreValue = PlayerController.score + InfiniteLevel.scoreContributedFromDistance;
        scoreText.text = scoreValue.ToString();
    }
}
