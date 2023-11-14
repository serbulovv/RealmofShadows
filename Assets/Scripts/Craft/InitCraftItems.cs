using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Craft
{
    public int result_item_id;
    public int min_level;
    public List<int> components_id;
    public List<int> amount;
}

[System.Serializable]
public class CraftsContainer
{
    public List<Craft> crafts;
}

public class InitCraftItems : MonoBehaviour
{
    public GameObject items_list;
    public Image coreItemImage;
    public Text coreItemText;
    public GameObject recipeCanvas;
    public Button craftItemButton;

    private void Awake()
    {
        string craftJson = PlayerPrefs.GetString("crafts");
        string jsonString = PlayerPrefs.GetString("current_user");
        CraftsContainer craftData = JsonUtility.FromJson<CraftsContainer>(craftJson);
        UserData userData = JsonUtility.FromJson<UserData>(jsonString);

        if (craftData != null)
        {
            // Images must be! coz craft won't be rendered
            Image[] childImages = items_list.GetComponentsInChildren<Image>(true);

            for (int i = 0; i < craftData.crafts.Count ; i++ )
            {
                // Continue if current user level do not equal min item level
                if(userData.current_user.level < craftData.crafts[i].min_level)
                {
                    continue;
                }

                int[] origItemArray = new int[1];
                int[] craftItemsArray = new int[craftData.crafts[i].components_id.Count];
                int[] amounts = new int[craftData.crafts[i].components_id.Count];

                Button button = childImages[i].GetComponent<Button>();
               
                origItemArray[0] = craftData.crafts[i].result_item_id;
                craftData.crafts[i].components_id.CopyTo(craftItemsArray, 0);
                craftData.crafts[i].amount.CopyTo(amounts, 0);

                Items origItem = ItemsClass.getItemsByIds(origItemArray);
                Item item = origItem.items[0];
                Items craftItems = ItemsClass.getItemsByIds(craftItemsArray);

                Sprite sprite = Resources.Load<Sprite>(item.image_path);

                childImages[i].sprite = sprite;
                coreItemImage.sprite = sprite;

                childImages[i].gameObject.SetActive(true);
                button.onClick.AddListener(() => OnItemClick(item, craftItems, amounts));
            }
        }
    }

    private void showCraftItemInfo(Item origItem, Items craftItems, int[] amounts)
    {
        coreItemImage.sprite = Resources.Load<Sprite>(origItem.image_path);
        coreItemImage.color = new Color(coreItemImage.color.r, coreItemImage.color.g, coreItemImage.color.b, 1);
        coreItemText.text = origItem.item_name;
        coreItemText.gameObject.SetActive(true);
        craftItemButton.gameObject.SetActive(true);
        Text[] childTexts = recipeCanvas.GetComponentsInChildren<Text>(true);

        for(int i = 0; i < craftItems.items.Length; i++)
        {
            childTexts[i].text = craftItems.items[i].item_name + " x"+amounts[i];
            childTexts[i].gameObject.SetActive(true);
        }
    }

    private void OnItemClick(Item origItem, Items craftItems, int[] amounts)
    {
        showCraftItemInfo(origItem, craftItems, amounts);
        CraftDataHolder.craftItemId = origItem.item_id;
    }
}
