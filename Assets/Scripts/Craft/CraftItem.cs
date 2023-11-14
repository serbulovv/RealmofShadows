using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftItem : MonoBehaviour
{
    public Image alertImage;
    public Text alertText;
    
    public void craftItem()
    {
        string craftJson = PlayerPrefs.GetString("crafts");
        string inventoryJson = PlayerPrefs.GetString("current_user_inventory");
        CraftsContainer craftData = JsonUtility.FromJson<CraftsContainer>(craftJson);
        InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(inventoryJson);

        Craft foundCraft = FindCraftById(CraftDataHolder.craftItemId, craftData);
        bool itemAlreadyExists = ItemExistsInInventory(CraftDataHolder.craftItemId, inventoryData);
        
        // RETURN IF ITEM ALREADY IN INVENTORY
        if (itemAlreadyExists) {
            StartCoroutine(sendNotification("The item is already crafted", 2));
            Debug.Log("The item is already crafted");
            return;
        } 

        bool resourcesAvailable = CheckResourcesAvailability(foundCraft, inventoryData);

        if (resourcesAvailable)
        {
            RemoveCraftComponents(foundCraft, inventoryData);
            inventoryJson = JsonUtility.ToJson(inventoryData);
            PlayerPrefs.SetString("current_user_inventory", inventoryJson);
            PlayerPrefs.Save();
            CurrentUserInventoryClass.AddItemToInventory(foundCraft.result_item_id, 1);
            StartCoroutine(sendNotification("The item is crafted", 2));
        } else
        {
            StartCoroutine(sendNotification("Not enough resources", 2));
            Debug.Log("Not enough resources");
        }
    }

    IEnumerator sendNotification(string text, int time)
    {
        alertImage.gameObject.SetActive(true);
        alertText.gameObject.SetActive(true);
        alertText.text = text;
        yield return new WaitForSeconds(time);
        alertImage.gameObject.SetActive(false);
        alertText.gameObject.SetActive(false);

    }

    static public Craft FindCraftById(int itemId, CraftsContainer craftData)
    {
        foreach (Craft craft in craftData.crafts)
        {
            if (craft.result_item_id == itemId)
            {
                return craft;
            }
        }
        return null;
    }

    static public bool CheckResourcesAvailability(Craft craft, InventoryData inventoryData)
    {
        // Iterate through the components required for crafting
        for (int i = 0; i < craft.components_id.Count; i++)
        {
            int componentId = craft.components_id[i];
            int requiredAmount = craft.amount[i];

            // Find the corresponding inventory item
            InventoryItemData inventoryItem = inventoryData.inventory.Find(item => item.itemId == componentId);

            // Check if the item is in inventory and there are enough of it
            if (inventoryItem == null || inventoryItem.amount < requiredAmount)
            {
                return false; // Not enough of this resource
            }
        }

        return true; // All required resources are available
    }

    static private void RemoveCraftComponents(Craft craft, InventoryData inventoryData)
    {
        // Subtract the required components from the inventory
        for (int i = 0; i < craft.components_id.Count; i++)
        {
            int componentId = craft.components_id[i];
            int requiredAmount = craft.amount[i];

            InventoryItemData inventoryItem = inventoryData.inventory.Find(item => item.itemId == componentId);

            if (inventoryItem != null)
            {
                inventoryItem.amount -= requiredAmount;

                // Ensure that the amount doesn't go below zero
                if (inventoryItem.amount < 0)
                {
                    inventoryItem.amount = 0;
                }
            }
        }
    }

    static private bool ItemExistsInInventory(int itemId, InventoryData inventoryData)
    {
        // Check if an item with the specified ID exists in the inventory
        return inventoryData.inventory.Exists(item => item.itemId == itemId);
    }

    [System.Serializable]
    public class InventoryData
    {
        public List<InventoryItemData> inventory = new List<InventoryItemData>();
    }

}
