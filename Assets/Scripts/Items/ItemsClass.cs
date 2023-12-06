using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int item_id;
    public string item_name;
    public float drop_chance;
    public string item_type;
    public string item_classification;
    public bool equiped;
    public int avg_amount;
    public string image_path;
    public string rarity;
    public int heal_point;
    public int attack_rating;
    public int defence;
    public int dodge;
    public int crit_chance;
    public int crit_damage;
    public int resistance;
    public int accuracy;
    public string default_item_stat_name;
    public int default_item_stat_value;
}

[System.Serializable]
public class Items
{
    public Item[] items;
}

[System.Serializable]
public class DropedItemData
{
    public int item_id;
    public string item_name;
    public int amount;
    public string image_path;
    public string item_type;
    public string item_classification;
    public bool equiped;
    public string rarity;
    public int heal_point;
    public int attack_rating;
    public int defence;
    public int dodge;
    public int crit_chance;
    public int crit_damage;
    public int resistance;
    public int accuracy;
    public string default_item_stat_name;
    public int default_item_stat_value;
}

public class ItemsClass : MonoBehaviour
{

    public static Items items;

    void Awake()
    {
        loadData();
    }

    public static Items getItemsByIds(int[] ids)
    {
        List<Item> foundedItems = new List<Item>();

        foreach (Item item in items.items)
        {
            if (Array.Exists(ids, id => id == item.item_id))
            {
                foundedItems.Add(item);
            }
        }

        Items result = new Items();
        result.items = foundedItems.ToArray();
        return result;
    }

    public static Items GetEquippedItems()
    {
        string inventoryJson = PlayerPrefs.GetString("current_user_inventory");
        InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(inventoryJson);

        List<Item> equippedItems = new List<Item>();

        if (inventoryData != null && inventoryData.inventory.Count > 0)
        {
            foreach (InventoryItemData itemData in inventoryData.inventory)
            {
                // Find the corresponding Item using the item id
                Item correspondingItem = FindItemById(itemData.itemId);
                if (correspondingItem != null && correspondingItem.equiped && correspondingItem.item_type == "stuff" && itemData.amount > 0)
                {
                    equippedItems.Add(correspondingItem);
                }
            }
        }

        Items result = new Items();
        result.items = equippedItems.ToArray();
        return result;
    }

    public static void loadData()
    {
        if (PlayerPrefs.HasKey("items"))
        {
            string jsonString = PlayerPrefs.GetString("items");
            Items loadedData = JsonUtility.FromJson<Items>(jsonString);

            if (loadedData != null)
            {
                items = loadedData;
            }
        }
    }

    public static List<DropedItemData> getItemsAfterFight(int[] _itemsIds)
    {
        List<DropedItemData> result = new List<DropedItemData>();

        if (items != null && items.items != null)
        {
            foreach (Item item in items.items)
            {
                if (Array.Exists(_itemsIds, id => id == item.item_id))
                {
                    if (UnityEngine.Random.value <= item.drop_chance)
                    {
                        int item_id = item.item_id;
                        int avgAmount = item.avg_amount;
                        float deviationPercentage = UnityEngine.Random.Range(-0.2f, 0.2f);
                        float itemDeviationPercentage = UnityEngine.Random.Range(-0.25f, 0.25f);
                        int amount = Mathf.RoundToInt(avgAmount + avgAmount * deviationPercentage);
                        int item_heal_point = 0;
                        int item_attack_rating = 0;
                        int item_defence = 0;
                        int item_dodge = 0;
                        int item_crit_chance = 0;
                        int item_crit_damage = 0;
                        int item_resistance = 0;
                        int item_accuracy = 0;

                        if(item.item_type == "item")
                        {
                            item_heal_point = (int)(item.heal_point + item.heal_point * itemDeviationPercentage);
                            item_attack_rating = (int)(item.attack_rating + item.attack_rating * itemDeviationPercentage);
                            item_defence = (int)(item.defence + item.defence * itemDeviationPercentage);
                            item_dodge = (int)(item.dodge + item.dodge * itemDeviationPercentage);
                            item_crit_chance = (int)(item.crit_chance + item.crit_chance * itemDeviationPercentage);
                            item_crit_damage = (int)(item.crit_damage + item.crit_damage * itemDeviationPercentage);
                            item_resistance = (int)(item.resistance + item.resistance * itemDeviationPercentage);
                            item_accuracy = (int)(item.accuracy + item.accuracy * itemDeviationPercentage);
                        }
                        DropedItemData itemData = new DropedItemData
                        {
                            item_id = item.item_id,
                            item_name = item.item_name,
                            amount = amount,
                            image_path = item.image_path,
                            heal_point = item_heal_point,
                            attack_rating = item_attack_rating,
                            defence = item_defence,
                            dodge = item.dodge,
                            crit_chance = item.crit_chance,
                            crit_damage = item.crit_damage,
                            resistance = item.resistance,
                            accuracy = item.accuracy,
                            default_item_stat_name = item.default_item_stat_name,
                            default_item_stat_value = item.default_item_stat_value
                        };

                        result.Add(itemData);
                    }
                }
            }
        }

        return result;
    }

    public static void ResetItemFields(int itemId)
    {
        Item itemToReset = FindItemById(itemId);

        if (itemToReset != null)
        {
            itemToReset.equiped = false;

            foreach (var field in itemToReset.GetType().GetFields())
            {
                if(itemToReset.default_item_stat_name == field.Name)
                {
                    itemToReset.GetType().GetField(field.Name).SetValue(itemToReset, itemToReset.default_item_stat_value);
                }
            }

            SaveItems();
        }

        string inventoryJson = PlayerPrefs.GetString("current_user_inventory");
        InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(inventoryJson);
    }

    private static void SaveItems()
    {
        // Serialize the items data to JSON
        string json = JsonUtility.ToJson(items);

        // Save the JSON string to PlayerPrefs
        PlayerPrefs.SetString("items", json);
        PlayerPrefs.Save();
    }

    public static void EquipItem(int itemId)
    {
        Item itemToEquip = FindItemById(itemId);

        if (itemToEquip != null)
        {
            itemToEquip.equiped = true;
            SaveItems();
        }
    }

    private class InventoryData
    {
        public List<InventoryItemData> inventory = new List<InventoryItemData>();
    }

    public static Item FindItemById(int itemId)
    {
        foreach (Item item in items.items)
        {
            if (item.item_id == itemId)
            {
                return item;
            }
        }
        return null; // Return null if the item is not found
    }
}
