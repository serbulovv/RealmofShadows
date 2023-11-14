using UnityEngine;

[System.Serializable]
public class CurrentUser
{
    public bool init;
    public string username;
    public int level;
    public int next_level_exp;
    public int current_exp;
    public string title;
    public int money;
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

public class CurrentUserClass : MonoBehaviour
{
    public static bool init;
    public static string username;
    public static int level;
    public static int current_exp;
    public static int next_level_exp;
    public static string title;
    public static int money;
    public static string image_path;
    public static int base_hp;
    public static int base_def;
    public static int base_phys_damage;

    void Awake()
    {
        loadData();
    }

    public static void addExp(int _exp)
    {
        current_exp += _exp;

        if (current_exp >= next_level_exp)
        {
            level++;
            current_exp = 0;
            next_level_exp = Mathf.RoundToInt(next_level_exp * 1.5f);
        }

        saveData();
    }

    public static void addMoney(int _money)
    {
        money += _money;
        saveData();
    }

    private void loadData()
    {
        if (PlayerPrefs.HasKey("current_user"))
        {
            string jsonString = PlayerPrefs.GetString("current_user");
            UserData loadedData = JsonUtility.FromJson<UserData>(jsonString);

            if (loadedData != null)
            {
                init = loadedData.current_user.init;
                username = loadedData.current_user.username;
                level = loadedData.current_user.level;
                next_level_exp = loadedData.current_user.next_level_exp;
                current_exp = loadedData.current_user.current_exp;
                title = loadedData.current_user.title;
                money = loadedData.current_user.money;
                image_path = loadedData.current_user.image_path;
                base_hp = loadedData.current_user.base_hp;
                base_def = loadedData.current_user.base_def;
                base_phys_damage = loadedData.current_user.base_phys_damage;
            }
        }
    }

    private static void saveData()
    {
        CurrentUser currentUser = new CurrentUser();
        currentUser.init = init;
        currentUser.username = username;
        currentUser.level = level;
        currentUser.next_level_exp = next_level_exp;
        currentUser.current_exp = current_exp;
        currentUser.title = title;
        currentUser.money = money;
        currentUser.image_path = image_path;
        currentUser.base_hp = base_hp;
        currentUser.base_def = base_def;
        currentUser.base_phys_damage = base_phys_damage;

        UserData userData = new UserData();
        userData.current_user = currentUser;
        string jsonString = JsonUtility.ToJson(userData);

        PlayerPrefs.SetString("current_user", jsonString);
        PlayerPrefs.Save();
    }

}
