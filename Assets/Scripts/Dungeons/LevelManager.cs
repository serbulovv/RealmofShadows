using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int _id;
    public int _unitId;

    public void StartLevel()
    {
        LevelDataHolder._levelId = _id;
        SceneManager.LoadScene("UniversalDungeonScene");
    }

    public void StartFight()
    {
        LevelDataHolder._unitId = _unitId;
        SceneManager.LoadScene("UniversalFightScene");
    }
}
