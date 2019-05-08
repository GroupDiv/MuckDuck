using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! Manages behavior of the start menu and handles user input
public class MainMenuBehavior : MonoBehaviour
{
    //! where user enters username
    public InputField userInputField;
    //! username
    public static string usernameInputText;

    //! Text for "Muck"
    public GameObject muckText;

    //! Text for "Duck"
    public GameObject duckText;

    //! Text for high score
    public GameObject highScoreText;

    //! "Play" button object
    public GameObject playButton;

    //! Test button object
    public GameObject testButton;

    //! Amount of time in seconds that each word will delay to be printed
    public float textDisplayTime;

    //! Stores the high score
    private int highScore;

    //! String used to search PlayerPrefs for the high score
    private string highScoreKey = "HighScore";

    //! The game object to track the test mode flag
    public GameObject testModeManager;

    public AudioClip textSound;
    

    void Start()
    {
        DontDestroyOnLoad(testModeManager);
        muckText.GetComponent<UnityEngine.UI.Text>().text = " ";
        duckText.GetComponent<UnityEngine.UI.Text>().text = " ";
        highScoreText.GetComponent<UnityEngine.UI.Text>().text = " ";
        
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);

        if (usernameInputText != null)
        {
            userInputField.text = usernameInputText;
        }
        StartCoroutine(textDelay());
    }

    public void SetUsername(string newUser)
    {
        PlayerPrefs.SetString("Username", newUser);
    }

    public void MenuDisplay()
    {
        
    }

    public void PlayGame() {
        Application.LoadLevel(1);
    }
    public void QuitGame() {
        Application.Quit();
    }

    public void PlayTest() {
        testModeManager.GetComponent<TestModeManager>().testFlag = true;
        Application.LoadLevel(1);
    }

    private IEnumerator textDelay() {
        yield return new WaitForSeconds(textDisplayTime);
        muckText.GetComponent<UnityEngine.UI.Text>().text = "MUCK";
        AudioSource.PlayClipAtPoint(textSound, new Vector3(0, 0, -10f), 1f);
        yield return new WaitForSeconds(textDisplayTime);
        duckText.GetComponent<UnityEngine.UI.Text>().text = "DUCK";
        AudioSource.PlayClipAtPoint(textSound, new Vector3(0, 0, -10f), 1f);
        yield return new WaitForSeconds(textDisplayTime);
        yield return new WaitForSeconds(textDisplayTime);   // wait twice as long because it's a little cooler
        highScoreText.GetComponent<UnityEngine.UI.Text>().text = "High Score: " + highScore.ToString();
    }
}