using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class InitFight : MonoBehaviour
{
    public Image victoryImage;
    public Image deffeatImage;
    public Text counterText;
    public Text finalExpText;
    public Text finalMoneyText;
    public Text unitNameText;
    public Color modifiedColor = new Color(0.5f, 0f, 0.5f); // Custom purple color
    public Color legendaryColor = new Color(1f, 0.84f, 0f); // Custom gold color
   
    private CurrentUser current_user; // current USER variable for all this code file
    private Unit current_unit; // current UNIT variable for all this code file
    private int userCurrentHp;
    private int unitCurrentHp;
    private GameObject[] unitObjects;

    private Image[] currentUserImages;
    private Image currentUserImage;
    private Text[] currentUserTexts;
    private Slider[] currentUserSlider;
    private Text[] currentUserHpText;

    private Image[] unitImages;
    private Image unitImage;
    private Text[] unitTexts;
    private Slider[] unitSlider;
    private Text[] unitHpText;

    private float hitDelay = 0.7f; // deffault is 0.7f. Need to make delay between attack.
    private int base_experience = 10; // base number of expirience. Generally final exp its -> base_expirience + unit lvl
    private int base_money = 10; // base number of money.
    private int deviation = 5; // We also need this variable to setup custom number of ex. For example if i get 10, it will be +5 pr -5 -> 5 or 10
    private int monye_deviation = 5;
    private int remainingBattles; // Counter to understand how many fights with unit left
    private int finalExp; // variable to store final user exp. we need to store this exp and display on win screen
    private int finalMoney; // variable to store final user exp. we need to store this exp and display on win screen
    private List<DropedItemData> resultItems = new List<DropedItemData>(); // final items that user will get

    [System.Serializable]
    public class Unit
    {
        public int unit_id;
        public bool unlocked;
        public int money;
        public string unit_name;
        public int unit_level;
        public string unit_image_path;
        public int unit_base_hp;
        public int unit_base_def;
        public int unit_base_phys_damage;
        public int[] dropped_items_id;
    }

    [System.Serializable]
    public class Units
    {
        public Unit[] units;
    }

    [System.Serializable]
    public class CurrentUser
    {
        public bool init;
        public string username;
        public int level;
        public string title;
        public string image_path;
        public int base_hp;
        public int base_def;
        public int base_phys_damage;
    }

    [System.Serializable]
    public class UserData
    {
        public CurrentUser current_user;
    }

    [System.Serializable]
    public class InitFightItem
    {
        public int item_id;
        public string item_name;
        public float drop_chance;
        public string item_type;
        public int avg_amount;
        public string image_path;
    }

    void Awake()
    {
        // When fight starts we find current user and current unit. After that we start fight procces coroutine.
        current_unit = findUnit(LevelDataHolder._unitId);
        current_user = getUserData();
        userCurrentHp = current_user.base_hp + UserStatsDataHolder.heal_points;
        unitCurrentHp = current_unit.unit_base_hp;
        InitData(current_user, current_unit);
        fightProcess();
    }

    CurrentUser getUserData()
    {
        // Finding current user data
        string jsonString = PlayerPrefs.GetString("current_user");
        UserData loadedData = JsonUtility.FromJson<UserData>(jsonString);
        return loadedData.current_user;
    }

    Unit findUnit(int _unitId)
    {
        // Tips.
        // 1. Add here random lucky units which will bring more treasure for user
        // 2. Done. Add here units with higher stats than usualy. Modifyed and Legendary unity tip. Legendary unit has 2% to find that, modified has 10%

        bool modified_unit = false;
        bool legendary_unit = false;

        float modifiedUnitPercentage = 0.2f; // 20% chance for modified unit
        float legendaryUnitPercentage = 0.07f; // 7% chance for legendary unit

        float randomValue = UnityEngine.Random.value;

        unitNameText.color = Color.white;

        if (randomValue <= legendaryUnitPercentage)
        {
            unitNameText.color = legendaryColor;
            legendary_unit = true;
        }
        else if (randomValue <= modifiedUnitPercentage)
        {
            unitNameText.color = modifiedColor;
            modified_unit = true;
        }

        string unitsJsonString = PlayerPrefs.GetString("units");
        Units unitsloadedData = JsonUtility.FromJson<Units>(unitsJsonString);

        // We need next percents to make unit harder depens on his modifier ( mod or leg )
        float modifier10Percent = 1.3f; // Modify this value as desired for the 30% increase.
        float modifier20Percent = 1.5f; // Modify this value as desired for the 50% increase

        if (unitsloadedData != null)
        {
            foreach (Unit unit in unitsloadedData.units)
            {
                if (unit.unit_id == _unitId)
                {
                    if (modified_unit)
                    {
                        // Add 10% to HP and attack damage
                        unit.unit_base_hp = Mathf.RoundToInt(unit.unit_base_hp * modifier10Percent);
                        unit.unit_base_phys_damage = Mathf.RoundToInt(unit.unit_base_phys_damage * modifier10Percent);
                    }

                    if (legendary_unit)
                    {
                        // Add additional 20% to HP and attack damage
                        unit.unit_base_hp = Mathf.RoundToInt(unit.unit_base_hp * modifier20Percent);
                        unit.unit_base_phys_damage = Mathf.RoundToInt(unit.unit_base_phys_damage * modifier20Percent);
                    }

                    return unit;
                }
            }
        }

        return null;
    }

    void InitData(CurrentUser _currentUser, Unit _unit)
    {
        // This method need just to init all fight images, texts, values.

        GameObject[] unitObjects = GameObject.FindGameObjectsWithTag("Unit");
        unitObjects = SortObjectsByHierarchy(unitObjects);

        // User setup
        currentUserImages = unitObjects[0].GetComponentsInChildren<Image>();
        currentUserImage = currentUserImages[0];
        currentUserTexts = unitObjects[0].GetComponentsInChildren<Text>();
        currentUserSlider = unitObjects[0].GetComponentsInChildren<Slider>(true);
        currentUserHpText = currentUserSlider[0].GetComponentsInChildren<Text>();

        string currentUserimagePath = _currentUser.image_path;
        Sprite currentUsersprite = Resources.Load<Sprite>(currentUserimagePath);
        currentUserImage.sprite = currentUsersprite;
        currentUserTexts[0].text = _currentUser.username;
        int current_base_hp = _currentUser.base_hp + UserStatsDataHolder.heal_points;
        currentUserSlider[0].maxValue = current_base_hp;
        currentUserSlider[0].value = current_base_hp;
        currentUserHpText[0].text = current_base_hp + "  /  " + current_base_hp;

        // Unit setup
        unitImages = unitObjects[1].GetComponentsInChildren<Image>();
        unitImage = unitImages[0];
        unitTexts = unitObjects[1].GetComponentsInChildren<Text>();
        unitSlider = unitObjects[1].GetComponentsInChildren<Slider>(true);
        unitHpText = unitSlider[0].GetComponentsInChildren<Text>();

        string unitImagePath = _unit.unit_image_path;
        Sprite unitSprite = Resources.Load<Sprite>(unitImagePath);
        unitImage.sprite = unitSprite;
        unitTexts[0].text = _unit.unit_name;
        unitSlider[0].maxValue = _unit.unit_base_hp;
        unitSlider[0].value = _unit.unit_base_hp;
        unitHpText[0].text = _unit.unit_base_hp + "  /  " + _unit.unit_base_hp;
    }

    private GameObject[] SortObjectsByHierarchy(GameObject[] objects)
    {
        // We need this method to be sure that units going on the same position on every device.
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

    // Main Fight method --------------------------------------------------- ---------------------------------------------------

    void fightProcess()
    {
        // This method just starts fight

        remainingBattles = LevelDataHolder._unitsCount;
        counterText.text = LevelDataHolder._unitsCount + " / " + LevelDataHolder._unitsCount;
        StartCoroutine(FightCoroutine());
    }

    // Main Fight method --------------------------------------------------- ---------------------------------------------------

    void userDealDamage()
    {
        // Deal damage to the unit based on the user's physical damage
        int damage = calculatePhysDamage(current_user.base_phys_damage, current_unit.unit_base_def, 0, UserStatsDataHolder.attack_rating);
        unitCurrentHp -= damage;
        unitHpText[0].text = unitCurrentHp + "  /  " + current_unit.unit_base_hp;
        unitSlider[0].value = unitCurrentHp;
        StartCoroutine(ShakeImage(unitImage));
        ShowDamageText(damage, unitImage); 
    }

    void unitDealDamage()
    {
        // Deal damage to the user based on the unit's physical damage
        int damage = calculatePhysDamage(current_unit.unit_base_phys_damage, current_user.base_def);
        userCurrentHp -= damage;
        currentUserHpText[0].text = userCurrentHp + "  /  " + current_user.base_hp;
        currentUserSlider[0].value = userCurrentHp;

        StartCoroutine(ShakeImage(currentUserImage));
        ShowDamageText(damage, currentUserImage);
    }

    bool userAlive()
    {
        // Check if the user is alive based on their current HP
        return userCurrentHp > 0;
    }

    bool unitAlive()
    {
        // Check if the unit is alive based on their current HP
        return unitCurrentHp > 0;
    }

    int calculatePhysDamage(int _damage, int _deff, int user_deff_stat = 0, int user_damage_stat = 0)
    {
        // Calculate physical damage considering the defense
        Debug.Log(user_damage_stat);
        float baseDamage = _damage * (1 - ((float)_deff + user_deff_stat) / 100) + user_damage_stat;
        float randomFactor = UnityEngine.Random.Range(0.95f, 1.05f);

        float modifiedDamage = baseDamage * randomFactor;
        return Mathf.RoundToInt(modifiedDamage);
    }

    void refreshStats(int _remainingBattles)
    {
        // Refresh the user and unit stats after each battle
        current_unit = findUnit(LevelDataHolder._unitId);
        counterText.text = _remainingBattles + " / " + LevelDataHolder._unitsCount;
        userCurrentHp = current_user.base_hp + UserStatsDataHolder.heal_points;
        unitCurrentHp = current_unit.unit_base_hp;

        currentUserHpText[0].text = userCurrentHp + "  /  " + (current_user.base_hp + UserStatsDataHolder.heal_points);
        currentUserSlider[0].value = userCurrentHp;

        unitHpText[0].text = unitCurrentHp + "  /  " + current_unit.unit_base_hp;
        unitSlider[0].value = unitCurrentHp;
        unitSlider[0].maxValue = unitCurrentHp;
    }

    private int calculateExp()
    {
        // Calculate the experience gained based on the unit's level and user's level
        float multiplier = (float)current_unit.unit_level / current_user.level;
        int experience = Mathf.RoundToInt((base_experience + UnityEngine.Random.Range(-deviation, deviation)) * multiplier);
        return experience * LevelDataHolder._unitsCount;
    }

    private IEnumerator FightCoroutine()
    {
        // Reduce remaining battles and check if there are more battles to continue
        // Also retrieve the items gained after the fight and fill the resultItems list

        yield return StartCoroutine(UserTurnCoroutine());

        if (!unitAlive())
        {
            remainingBattles -= 1;
            List<DropedItemData> itemsAfterFight = ItemsClass.getItemsAfterFight(current_unit.dropped_items_id);
            if(itemsAfterFight != null)
            {
                if(itemsAfterFight.Count > 0)
                {
                    fillResultItems(itemsAfterFight);
                }
            }

            if (remainingBattles > 0) {
                yield return new WaitForSeconds(0.3f);
                refreshStats(remainingBattles);
                yield return StartCoroutine(FightCoroutine());
                yield break;
            }
            if (remainingBattles <= 0)
            {
                yield return new WaitForSeconds(0.7f);
                int exp = calculateExp();
                finalMoney = CalculateMoney();
                finalExp += exp;
                CurrentUserClass.addExp(exp);
                CurrentUserClass.addMoney(finalMoney);
                StartCoroutine(ActivateVictoryImageAfterBattles());
                if (resultItems.Count > 0)
                {
                    showGainedItems(resultItems);
                    CurrentUserInventoryClass.AddItemsToInventory(resultItems);
                }
                FindNextUnitAndUnlock();
                yield break;
            }
        }

        yield return StartCoroutine(UnitTurnCoroutine());

        if (!userAlive())
        {
            yield return new WaitForSeconds(0.7f);
            StartCoroutine(ActivateDeffeatImageAfterBattles());
            yield break;
        }

        yield return StartCoroutine(FightCoroutine());
    }

    private IEnumerator UserTurnCoroutine()
    {
        yield return new WaitForSeconds(hitDelay);
        userDealDamage();
    }

    private IEnumerator UnitTurnCoroutine()
    {
        yield return new WaitForSeconds(hitDelay);
        unitDealDamage();
    }

    private IEnumerator ActivateVictoryImageAfterBattles()
    {
        // Coroutine to activate victory image after battles
        while (unitAlive() && userAlive())
        {
            yield return null;
        }

        counterText.gameObject.SetActive(false);
        finalExpText.text = "Experience: " + finalExp;
        finalMoneyText.text = "Money: " + finalMoney;
        victoryImage.gameObject.SetActive(true);
    }

    private IEnumerator ActivateDeffeatImageAfterBattles()
    {
        // Coroutine to activate defeat image after battles
        while (unitAlive() && userAlive())
        {
            yield return null;
        }

        counterText.gameObject.SetActive(false);
        deffeatImage.gameObject.SetActive(true);
    }

    private IEnumerator ShakeImage(Image image)
    {
        // Coroutine to shake the image
        Vector3 originalPosition = image.transform.position;
        float shakeDuration = 0.3f;
        float shakeIntensity = 0.1f;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float offsetX = UnityEngine.Random.Range(-shakeIntensity, shakeIntensity);
            float offsetY = UnityEngine.Random.Range(-shakeIntensity, shakeIntensity);

            image.transform.position = originalPosition + new Vector3(offsetX, offsetY, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        image.transform.position = originalPosition;
    }
    
    private void showGainedItems(List<DropedItemData> items)
    {
        // Show the gained items in the UI
        GameObject rewardsList = GameObject.Find("rewardsList");
        if (rewardsList != null)
        {
            Image[] imageComponents = rewardsList.GetComponentsInChildren<Image>(true);

            for (int i = 0; i < imageComponents.Length; i++)
            {
                if (i < items.Count)
                {
                    Image image = imageComponents[i];
                    DropedItemData item = items[i];

                    if (item != null)
                    {
                        Sprite sprite = Resources.Load<Sprite>(item.image_path);
                        if (sprite != null)
                        {
                            image.sprite = sprite;
                            image.gameObject.SetActive(true);
                            Text text = image.GetComponentInChildren<Text>();
                            if (text != null)
                            {
                                text.text = item.amount.ToString();
                            }
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("rewardsList wansnt founded");
        }
    }

    private void fillResultItems(List<DropedItemData> newItems)
    {
        // Fill the resultItems list with the new items gained after the fight
        foreach (DropedItemData newItem in newItems)
        {
            DropedItemData existingItem = resultItems.Find(item => item.item_name == newItem.item_name);

            if (existingItem != null)
            {
                existingItem.amount += newItem.amount;
            }
            else
            {
                resultItems.Add(newItem);
            }
        }
    }

    private void FindNextUnitAndUnlock()
    {
        int nextUnitId = current_unit.unit_id + 1; // Get the ID of the next unit

        string unitsJsonString = PlayerPrefs.GetString("units");
        Units unitsloadedData = JsonUtility.FromJson<Units>(unitsJsonString);

        if (unitsloadedData != null)
        {
            foreach (Unit unit in unitsloadedData.units)
            {
                if (unit.unit_id == nextUnitId)
                {
                    unit.unlocked = true; // Set the next unit as unlocked

                    // Save the modified units data back to PlayerPrefs
                    Units updatedUnitsData = unitsloadedData;
                    string updatedUnitsJsonString = JsonUtility.ToJson(updatedUnitsData);
                    PlayerPrefs.SetString("units", updatedUnitsJsonString);
                    PlayerPrefs.Save();

                    break;
                }
            }
        }
    }

    private int CalculateMoney()
    {
        int money = (base_money + UnityEngine.Random.Range(-monye_deviation, monye_deviation)) * (current_unit.unit_level / current_user.level) * LevelDataHolder._unitsCount;
        if(money > 0)
        {
            return money;
        }
        return 0;
    }

    void ShowDamageText(int damage, Image image)
    {
        GameObject damageTextObject = new GameObject("DamageText");
        damageTextObject.transform.SetParent(image.transform);

        // Calculate a random position within the image
        float randomX = UnityEngine.Random.Range(-image.rectTransform.rect.width / 2f, image.rectTransform.rect.width / 2f);
        float randomY = UnityEngine.Random.Range(-image.rectTransform.rect.height / 2f, image.rectTransform.rect.height / 2f);
        damageTextObject.transform.localPosition = new Vector3(randomX, randomY, 0f);

        Text damageText = damageTextObject.AddComponent<Text>();
        damageText.font = Resources.GetBuiltinResource<Font>("Arial.ttf"); // Use your own font here
        damageText.fontSize = 40; // Adjust the font size as desired
        damageText.alignment = TextAnchor.MiddleCenter;
        damageText.color = Color.red;
        damageText.text = damage.ToString();

        // Set the scale of the damage text
        damageTextObject.transform.localScale = new Vector3(1.5f, 1.5f, 1f); // Adjust the scale as desired

        // Add a floating effect
        float floatingSpeed = 2f;
        float floatingHeight = 40f; // Adjust the floating height as desired
        StartCoroutine(FloatingEffect(damageTextObject.transform, floatingSpeed, floatingHeight));

        Destroy(damageTextObject, 1f);
    }

    IEnumerator FloatingEffect(Transform transform, float floatingSpeed, float floatingHeight)
    {
        float elapsedTime = 0f;
        float startY = transform.localPosition.y;
        float targetY = startY + floatingHeight;

        while (elapsedTime < 1f)
        {
            float newY = Mathf.Lerp(startY, targetY, elapsedTime);
            transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);

            elapsedTime += Time.deltaTime * floatingSpeed;
            yield return null;
        }
    }
}
