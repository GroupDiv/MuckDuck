using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! Controls setting/retreiving high score from PlayerPrefs file
public class LeaderBoard : MonoBehaviour
{
    //! The high score to be retreived/updated if necessary
    public int highScore;

    //! The key Unity searches for to set to attach the high score value to
    string highScoreKey = "HighScore";
    

    void Start()
    {
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
    }

    void Update()
    {
        //! use highScore to print high score to start screen and game over screen
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
    }
}