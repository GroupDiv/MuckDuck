using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //! "Play" button object
    public GameObject playButton;

    //! Amount of time in seconds that each word will delay to be printed
    public float textDisplayTime;
    

    void Start()
    {
        muckText.GetComponent<UnityEngine.UI.Text>().text = " ";
        duckText.GetComponent<UnityEngine.UI.Text>().text = " ";
        //playButton.SetActive(!gameObject.activeSelf);
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

    private IEnumerator textDelay() {
        yield return new WaitForSeconds(textDisplayTime);
        muckText.GetComponent<UnityEngine.UI.Text>().text = "MUCK";
        yield return new WaitForSeconds(textDisplayTime);
        duckText.GetComponent<UnityEngine.UI.Text>().text = "DUCK";
        //yield return new WaitForSeconds(textDisplayTime);
        //gameObject.SetActive(gameObject.activeSelf);
    }
}