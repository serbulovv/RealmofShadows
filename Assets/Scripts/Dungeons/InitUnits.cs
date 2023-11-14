using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class InitUnits : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public int level_id;
        public string level_name;
        public int[] level_unit_ids;
        public bool unlocked;
    }

    [System.Serializable]
    public class Levels
    {
        public Level[] levels;
    }

    [System.Serializable]
    public class Unit
    {
        public int unit_id;
        public bool  unlocked;
        public string unit_name;
        public int unit_level;
        public string unit_image_path;
        public int unit_base_hp;
        public int unit_base_def;
        public int unit_base_phys_damage;
    }

    [System.Serializable]
    public class Units
    {
        public Unit[] units;
    }

    public Text unitsCountText;

    void Awake()
    {
        if (PlayerPrefs.HasKey("levels") && PlayerPrefs.HasKey("units"))
        {
            string levelsJsonString = PlayerPrefs.GetString("levels");
            string unitsJsonString = PlayerPrefs.GetString("units");
            Levels levelsloadedData = JsonUtility.FromJson<Levels>(levelsJsonString);
            Units unitsloadedData = JsonUtility.FromJson<Units>(unitsJsonString);

            Level level;
            Units units;

            unitsCountText.text = "" + LevelDataHolder._unitsCount; // Init unit count if it present

            if (levelsloadedData != null && unitsloadedData != null)
            {
                level = findLevel(LevelDataHolder._levelId, levelsloadedData);
                if (level != null)
                {
                    units = findUnits(level.level_unit_ids, unitsloadedData);
                    fillUnits(units.units);
                }
                else
                {
                    Debug.Log("There is no such level with a specific id or there are no units");
                }
            }
        }
        else
        {
            Debug.Log("PlayerPrefs doesn't contain units data");
        }
    }

    Level findLevel(int _id, Levels _loadedData)
    {
        foreach (Level _level in _loadedData.levels)
        {
            if (_level.level_id == _id)
            {
                return _level;
            }
        }
        return null; // Return null if the level is not found
    }

    Units findUnits(int[] _ids, Units _unitsData)
    {
        Units foundUnits = new Units();
        List<Unit> matchingUnits = new List<Unit>();

        if (_unitsData != null)
        {
            foreach (Unit unit in _unitsData.units)
            {
                if (System.Array.Exists(_ids, id => id == unit.unit_id))
                {
                    matchingUnits.Add(unit);
                }
            }
        }

        foundUnits.units = matchingUnits.ToArray();
        return foundUnits;
    }

    Unit[] SortUnitsByID(Unit[] units)
    {
        return units.OrderBy(unit => unit.unit_id).ToArray();
    }

    void fillUnits(Unit[] units)
    {
        Unit[] sortedUnits = SortUnitsByID(units);
        GameObject[] unitObjects = GameObject.FindGameObjectsWithTag("Unit");
        unitObjects = SortObjectsByHierarchy(unitObjects);
        for (int i = 0; i < unitObjects.Length; i++)
        {
            if (i < sortedUnits.Length) // Check if the index is within the bounds of the units array
            {
                Image[] images = unitObjects[i].GetComponentsInChildren<Image>();
                Button button = unitObjects[i].GetComponent<Button>();
                Text[] texts = unitObjects[i].GetComponentsInChildren<Text>();
                LevelManager levelManager = unitObjects[i].GetComponent<LevelManager>();

                if (images.Length > 0)
                {
                    Image image = images[1]; // Access the first Image component in the array
                    Text unit_name = texts[0];
                    Text unit_level = texts[1];

                    levelManager._unitId = sortedUnits[i].unit_id;
                    unit_name.text = sortedUnits[i].unit_name;
                    unit_level.text = "Level: " + sortedUnits[i].unit_level.ToString();
                    if (!sortedUnits[i].unlocked)
                    {
                        levelManager._unitId = sortedUnits[i].unit_id;
                        unit_name.text = "Locked";
                        unit_level.text = "";
                        button.interactable = false;
                        string imagePath = "Images/Units/lock";
                        Sprite sprite = Resources.Load<Sprite>(imagePath);
                        image.sprite = sprite;
                    }
                    else
                    {
                        levelManager._unitId = sortedUnits[i].unit_id;
                        unit_name.text = sortedUnits[i].unit_name;
                        unit_level.text = "Level: " + sortedUnits[i].unit_level.ToString();
                        string imagePath = sortedUnits[i].unit_image_path;
                        Sprite sprite = Resources.Load<Sprite>(imagePath);
                        image.sprite = sprite;
                    }
                }
            }
            else
            {
                Debug.LogWarning("Not enough units provided for all unit objects.");
                break; // Exit the loop if there are no more units to assign
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
}
