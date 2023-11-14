using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentUserInventoryClass : MonoBehaviour
{
    private static Dictionary<int, int> inventoryItems = new Dictionary<int, int>();
    private static string inventoryKey = "current_user_inventory";

    static CurrentUserInventoryClass()
    {
        LoadInventory();
    }

    public static void AddItemToInventory(int itemId, int amount)
    {
        if (inventoryItems.ContainsKey(itemId))
        {
            inventoryItems[itemId] += amount;
        }
        else
        {
            inventoryItems.Add(itemId, amount);
        }
        SaveInventory();
    }

    public static void AddItemsToInventory(List<DropedItemData> items)
    {
        foreach (DropedItemData item in items)
        {
            AddItemToInventory(item.item_id, item.amount);
        }
    }

    private static void LoadInventory()
    {
        string inventoryJson = PlayerPrefs.GetString(inventoryKey, "{\"inventory\":[]}");
        InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(inventoryJson);

        if (inventoryData != null)
        {
            foreach (InventoryItemData itemData in inventoryData.inventory)
            {
                inventoryItems[itemData.itemId] = itemData.amount;
            }
        }
    }

    private static void SaveInventory()
    {
        InventoryData inventoryData = new InventoryData();

        foreach (var item in inventoryItems)
        {
            InventoryItemData itemData = new InventoryItemData();
            itemData.itemId = item.Key;
            itemData.amount = item.Value;
            inventoryData.inventory.Add(itemData);
        }

        string inventoryJson = JsonUtility.ToJson(inventoryData);
        PlayerPrefs.SetString(inventoryKey, inventoryJson);
        PlayerPrefs.Save();
    }

    public static void RemoveItemFromInventory(int itemId)
    {
        if (inventoryItems.ContainsKey(itemId))
        {
            inventoryItems.Remove(itemId);

            SaveInventory();
        }
    }

    [System.Serializable]
    private class InventoryData
    {
        public List<InventoryItemData> inventory = new List<InventoryItemData>();
    }

    [System.Serializable]
    private class InventoryItemData
    {
        public int itemId;
        public int amount;
    }
}
