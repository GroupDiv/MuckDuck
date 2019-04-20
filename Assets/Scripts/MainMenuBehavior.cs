using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBehavior : MonoBehaviour
{
    //where user enters username
    public InputField userInputField;
    //username
    public static string usernameInputText;

    void Start()
    {
        if (usernameInputText != null)
        {
            userInputField.text = usernameInputText;
        }
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
}