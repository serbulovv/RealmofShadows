using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [System.Serializable]
    public class Levels
    {
        public Level[] levels;
    }

    [System.Serializable]
    public class Level
    {
        public int level_id;
        public string level_name;
        public int[] level_unit_ids;
        public bool unlocked;
        public string level_image_path;
    }

    Levels levels;

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

    public void UnlockLevel(int levelId)
    {
        if (PlayerPrefs.HasKey("levels"))
        {
            string levelsJsonString = PlayerPrefs.GetString("levels");
            Levels loadedData = JsonUtility.FromJson<Levels>(levelsJsonString);

            if (loadedData != null)
            {
                levels = loadedData;
                Level level = FindLevelById(levelId);
                if (level != null)
                {
                    level.unlocked = true;
                    SaveLevelsData();
                }
            }
        }
        else
        {
            Debug.Log("There is no levels");
        }
    }

    Level FindLevelById(int levelId)
    {
        if (levels != null && levels.levels != null)
        {
            return System.Array.Find(levels.levels, level => level.level_id == levelId);
        }
        return null;
    }

    void SaveLevelsData()
    {
        string levelsJsonString = JsonUtility.ToJson(levels);
        PlayerPrefs.SetString("levels", levelsJsonString);
        PlayerPrefs.Save();
    }
}
