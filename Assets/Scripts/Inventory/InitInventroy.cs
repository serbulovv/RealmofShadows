using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitInventroy : MonoBehaviour
{
    public GameObject inventory;
    public GameObject stuff_inventory;
    public ShowItemData showItemDataScript;
    public InventoryHelper inventoryHelperScript;

    private void Awake()
    {
        string inventoryJson = PlayerPrefs.GetString("current_user_inventory");
        InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(inventoryJson);

        if (inventoryData != null && inventoryData.inventory.Count > 0)
        {
            List<int> itemIds = new List<int>();
            foreach (InventoryItemData itemData in inventoryData.inventory)
            {
                itemIds.Add(itemData.itemId);
            }

            Items items = ItemsClass.getItemsByIds(itemIds.ToArray());

            if (items != null && items.items != null && items.items.Length > 0)
            {
                for (int i = 0; i < inventory.transform.childCount; i++)
                {
                    Transform child = inventory.transform.GetChild(i);
                    Image image = child.GetComponent<Image>();
                    Text amount_text = image.GetComponentInChildren<Text>();
                    Button button = child.GetComponent<Button>();

                    if (button == null)
                    {
                        // If the Button component doesn't exist, add it.
                        button = child.gameObject.AddComponent<Button>();
                    }

                    if (i < items.items.Length)
                    {
                        if (items.items[i].item_type != "resource") { continue; }
                        Item item = items.items[i];
                        Sprite sprite = Resources.Load<Sprite>(item.image_path);

                        if (sprite != null)
                        {
                            image.sprite = sprite;
                            image.gameObject.SetActive(true);

                            InventoryItemData itemData = inventoryData.inventory.Find(data => data.itemId == items.items[i].item_id);

                            if (itemData != null)
                            {
                                amount_text.text = itemData.amount.ToString();
                            }
                            else
                            {
                                amount_text.text = "0";
                            }

                            int index = i;
                            button.onClick.AddListener(() => OnItemClick(items.items[index], itemData));
                        }
                    }
                    else
                    {
                        image.gameObject.SetActive(false);
                    }
                }

                // stuff inventory 
                for (int i = 0; i < stuff_inventory.transform.childCount; i++)
                {
                    Transform child = stuff_inventory.transform.GetChild(i);
                    Image image = child.GetComponent<Image>();
                    Text amount_text = image.GetComponentInChildren<Text>();
                    Button button = child.GetComponent<Button>();

                    if (button == null)
                    {
                        // If the Button component doesn't exist, add it.
                        button = child.gameObject.AddComponent<Button>();
                    }

                    if (i < items.items.Length)
                    {
                        if (items.items[i].item_type != "stuff") { continue; }
                        Item item = items.items[i];
                        Sprite sprite = Resources.Load<Sprite>(item.image_path);

                        if (sprite != null)
                        {
                            image.sprite = sprite;
                            image.gameObject.SetActive(true);

                            InventoryItemData itemData = inventoryData.inventory.Find(data => data.itemId == items.items[i].item_id);

                            if (itemData != null)
                            {
                                amount_text.text = itemData.amount.ToString();
                            }
                            else
                            {
                                amount_text.text = "0";
                            }

                            int index = i;
                            button.onClick.AddListener(() => OnItemClick(items.items[index], itemData));
                        }
                    }
                    else
                    {
                        image.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void OnItemClick(Item item, InventoryItemData itemData)
    {
        showItemDataScript.ShowDataOnMaterialClick(item, itemData);
        InventoryHelper.itemId = item.item_id;
    }

    [System.Serializable]
    private class InventoryData
    {
        public List<InventoryItemData> inventory = new List<InventoryItemData>();
    }


}

[System.Serializable]
public class InventoryItemData
{
    public int itemId;
    public int amount;
}
