using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

public class InitLevels : MonoBehaviour
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

    void Awake()
    {
        if (PlayerPrefs.HasKey("levels"))
        {
            string levelsJsonString = PlayerPrefs.GetString("levels");
            Levels levelsloadedData = JsonUtility.FromJson<Levels>(levelsJsonString);

            if (levelsloadedData != null)
            {
                levels = FindUnlockedLevels(levelsloadedData);
                fillLevels(levels);
            }
        }
        else
        {
            Debug.Log("There is no levels");
        }
    }

    Level[] SortLevelsByID(Level[] levels)
    {
        return levels.OrderBy(level => level.level_id).ToArray();
    }

    Levels FindUnlockedLevels(Levels _levelsData)
    {
        Levels unlockedLevels = new Levels();

        if (_levelsData != null)
        {
            unlockedLevels.levels = _levelsData.levels.Where(level => level.unlocked == true).ToArray();
            unlockedLevels.levels = SortLevelsByID(unlockedLevels.levels);
        }

        return unlockedLevels;
    }

    void fillLevels(Levels levels)
    {
        Level[] sortedLevels = levels.levels;
        GameObject[] levelObjects = FindAllObjectsWithTag("DungeonLevel");
        levelObjects = SortObjectsByHierarchy(levelObjects);

        for (int i = 0; i < levelObjects.Length; i++)
        {
            if (i < sortedLevels.Length)
            {
                Image[] images = levelObjects[i].GetComponentsInChildren<Image>(true);
                Text[] texts = levelObjects[i].GetComponentsInChildren<Text>(true);
                
                if (images.Length > 0)
                {
                    Image image = images[0]; // Access the first Image component in the array
                    Text level_name = texts[0];

                    levelObjects[i].gameObject.SetActive(true);
                    level_name.text = sortedLevels[i].level_name;
                    string imagePath = sortedLevels[i].level_image_path;
                    Sprite sprite = Resources.Load<Sprite>(imagePath);
                    image.sprite = sprite;
                }
            }
            else
            {
                Debug.LogWarning("Not enough levels provided for all unit objects.");
                break; // Exit the loop if there are no more levels to assign
            }
        }
    }

    private GameObject[] SortObjectsByHierarchy(GameObject[] objects)
    {
        Array.Sort(objects, (obj1, obj2) =>
        {
            Transform transform1 = obj1.transform;
            Transform transform2 = obj2.transform;

            // Сравниваем индексы объектов в иерархии
            if (transform1.GetSiblingIndex() < transform2.GetSiblingIndex())
                return -1;
            else if (transform1.GetSiblingIndex() > transform2.GetSiblingIndex())
                return 1;
            else
                return 0;
        });

        return objects;
    }

    GameObject[] FindAllObjectsWithTag(string tag)
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        List<GameObject> objectsWithTag = new List<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag(tag))
            {
                objectsWithTag.Add(obj);
            }
        }

        return objectsWithTag.ToArray();
    }
}
