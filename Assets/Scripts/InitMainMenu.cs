using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitMainMenu : MonoBehaviour
{
    public Text text_username, text_level, text_title, text_money;

    [System.Serializable]
    public class CurrentUser
    {
        public bool init;
        public string username;
        public int level;
        public string title;
        public int money;
    }

    [System.Serializable]
    public class UserData
    {
        public CurrentUser current_user;
    }

    void Awake()
    {
        if (text_username != null && text_level != null && text_title != null)
        {
            if (PlayerPrefs.HasKey("current_user"))
            {
                string jsonString = PlayerPrefs.GetString("current_user");
                UserData loadedData = JsonUtility.FromJson<UserData>(jsonString);

                if (loadedData != null)
                {
                    text_username.text = loadedData.current_user.username.ToString();
                    text_level.text = "Level " + loadedData.current_user.level.ToString();
                    text_title.text = loadedData.current_user.title.ToString();
                    text_money.text = loadedData.current_user.money.ToString();
                }

            }
            else
            {
            }
        }
        else
        {
        }
    }
}
