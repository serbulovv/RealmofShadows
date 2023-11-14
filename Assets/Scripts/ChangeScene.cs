using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string targetSceneName;

    public void SwitchToScene(string targetSceneName)
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
