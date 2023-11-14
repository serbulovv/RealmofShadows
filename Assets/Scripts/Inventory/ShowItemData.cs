using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowItemData : MonoBehaviour
{
    public GameObject materialInfoBackground;
    public Text itemNameText;
    public Text itemAmountText;
    public Text itemRarityText;
    public Text itemDescriptionText;

    // Stuff stats
    public GameObject stuffInfoBackground;
    public Text stuffItemNameText;
    public Text stuffItemRarityText;
    public Text itemAttackRattingText;
    public Text itemHealPointsText;
    public Text itemDefenceText;
    public Text itemDodgeText;
    public Text itemCritChanceText;
    public Text itemCritDamageText;
    public Text itemResistanceText;
    public Text itemAccuracyText;


    public void ShowDataOnMaterialClick(Item _item, InventoryItemData _item_data)
    {
        switch (_item.rarity)
        {
            case "Common":
                itemRarityText.color = Color.white;
                stuffItemRarityText.color = Color.white;
                break;
            case "Rare":
                itemRarityText.color = new Color(0, 0.4f, 0);
                stuffItemRarityText.color = new Color(0, 0.4f, 0);
                break;
            case "Magic":
                itemRarityText.color = Color.blue;
                stuffItemRarityText.color = Color.blue;
                break;
            case "Legendary":
                itemRarityText.color = new Color(1, 0.8f, 0);
                stuffItemRarityText.color = new Color(1, 0.8f, 0);
                break;
            case "Deadly":
                itemRarityText.color = Color.black;
                stuffItemRarityText.color = Color.black;
                break;
            default:
                break;
        }

        if (materialInfoBackground != null)
        {
            if(_item.item_type == "resource")
            {
                materialInfoBackground.SetActive(true);
                itemNameText.text = _item.item_name;
                itemAmountText.text = "Amount:  " + _item_data.amount;
                itemRarityText.text = _item.rarity;
                itemDescriptionText.text = "Default Item Description";
            }

            if(_item.item_type == "stuff")
            {
                stuffInfoBackground.SetActive(true);
                stuffItemNameText.text = _item.item_name;
                stuffItemRarityText.text = _item.rarity;
                itemAttackRattingText.text = "Attack ratting: " + _item.attack_rating;
                itemHealPointsText.text = "Heal points: " + _item.heal_point;
                itemDefenceText.text = "Defence: " + _item.defence;
                itemDodgeText.text = "Dodge: " + _item.dodge;
                itemCritChanceText.text = "Crit Chance: " + _item.crit_chance;
                itemCritDamageText.text = "Crit Damage: " + _item.crit_damage;
                itemResistanceText.text = "Resistance: " + _item.resistance;
                itemAccuracyText.text = "Accuracy: " + _item.accuracy;
            }
        }
    }
}
