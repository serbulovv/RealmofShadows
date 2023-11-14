using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitStats : MonoBehaviour
{
    public ShowItemData showItemDataScript;
    // Main stuff
    [Header("Stats images")]
    public Image weapon;
    public Image helmet;
    public Image armor;
    public Image robers;
    public Image boots;
    public Image ring;
    public Image amulet;
    public Image gloves;

    // Third party stuff
    public Image wings;
    public Image emblem;
    public Image sphere;
    public Image booster;

    [Header("Stats texts")]
    public Text attack_rating_text;
    public Text heal_point_text;
    public Text defence_text;
    public Text dodge_text;
    public Text crit_chance_text;
    public Text crit_damage_text;
    public Text resistance_text;
    public Text accuracy_text;
    public Text dropped_gold_chance_text;
    public Text items_drop_booster_text;
    public Text exp_booster_text;

    // slot for pet

    private void Awake()
    {
        Items items = ItemsClass.GetEquippedItems();

        for(int i = 0; i < items.items.Length; i++)
        {
            Sprite sprite;

        switch (items.items[i].item_classification)
            {
                case "weapon":
                    sprite = Resources.Load<Sprite>(items.items[i].image_path);
                    weapon.sprite = sprite;
                    AssignButtonAction(weapon, items.items[i], null);
                    ChangeImageAlpha(weapon, 1);
                    break;
                case "helmet":
                    sprite = Resources.Load<Sprite>(items.items[i].image_path);
                    helmet.sprite = sprite;
                    AssignButtonAction(helmet, items.items[i], null);
                    ChangeImageAlpha(helmet, 1);
                    break;
                case "armor":
                    sprite = Resources.Load<Sprite>(items.items[i].image_path);
                    armor.sprite = sprite;
                    AssignButtonAction(armor, items.items[i], null);
                    break;
                case "robers":
                    sprite = Resources.Load<Sprite>(items.items[i].image_path);
                    robers.sprite = sprite;
                    AssignButtonAction(robers, items.items[i], null);
                    break;
                case "boots":
                    sprite = Resources.Load<Sprite>(items.items[i].image_path);
                    boots.sprite = sprite;
                    AssignButtonAction(boots, items.items[i], null);
                    break;
                case "ring":
                    sprite = Resources.Load<Sprite>(items.items[i].image_path);
                    ring.sprite = sprite;
                    AssignButtonAction(ring, items.items[i], null);
                    break;
                case "amulet":
                    sprite = Resources.Load<Sprite>(items.items[i].image_path);
                    amulet.sprite = sprite;
                    AssignButtonAction(amulet, items.items[i], null);
                    break;
                case "gloves":
                    sprite = Resources.Load<Sprite>(items.items[i].image_path);
                    gloves.sprite = sprite;
                    AssignButtonAction(gloves, items.items[i], null);
                    break;
            }
        }

        // init text stats

    attack_rating_text.text = "ATK: "+ UserStatsDataHolder.attack_rating;
    heal_point_text.text = "HP: " + UserStatsDataHolder.heal_points;
    defence_text.text = "DEFF : " + UserStatsDataHolder.defence;
    dodge_text.text = "Dodge: " + UserStatsDataHolder.dodge;
    crit_chance_text.text = "Crit chance: " + UserStatsDataHolder.crit_chance;
    crit_damage_text.text = "Crit damage: " + UserStatsDataHolder.crit_damage;
    resistance_text.text = "Resistance: " + UserStatsDataHolder.resistance;
    accuracy_text.text = "Accuracy: " + UserStatsDataHolder.accuracy;
    dropped_gold_chance_text.text = "Dropped gold chance: " + UserStatsDataHolder.dropped_gold_chance;
    items_drop_booster_text.text = "Item drop booster: " + UserStatsDataHolder.items_drop_booster;
    exp_booster_text.text = "Exp booster: " + UserStatsDataHolder.exp_booster;
}

    private void AssignButtonAction(Image image, Item item, InventoryItemData itemData)
    {
        Button button = image.GetComponentInChildren<Button>();

        if (button != null)
        {
            button.onClick.AddListener(() => OnItemClick(item, itemData));
        }
        else
        {
            Debug.LogWarning($"Button not found on {image.name}.");
        }
    }

    private void OnItemClick(Item item, InventoryItemData itemData)
    {
        showItemDataScript.ShowDataOnMaterialClick(item, itemData);
    }

    private void ChangeImageAlpha(Image image, float alphaValue)
    {
        Color currentColor = image.color;
        currentColor.a = Mathf.Clamp01(alphaValue);
        image.color = currentColor;
    }
}
