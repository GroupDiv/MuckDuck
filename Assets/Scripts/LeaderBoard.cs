using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    //high score
    public int highScore;

    string highScoreKey = "HighScore";
    
    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
    }

    // Update is called once per frame
    void Update()
    {
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        //use highScore to print hgih score to start screen or game over screen (whichever we decide)
    }
}