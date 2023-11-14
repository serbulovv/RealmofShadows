using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitGame : MonoBehaviour
{
    public void Call()
    {
        if (PlayerPrefs.HasKey("current_user"))
        {
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            SceneManager.LoadScene("RegistrationScene");
        }
    }
}