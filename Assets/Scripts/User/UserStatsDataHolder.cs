using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserStatsDataHolder : MonoBehaviour
{
    public static int attack_rating = 0;
    public static int heal_points = 0;
    public static int defence = 0;
    public static int dodge = 0;
    public static int crit_chance = 0;
    public static int crit_damage = 0;
    public static int resistance = 0;
    public static int accuracy = 0;
    public static int dropped_gold_chance = 0;
    public static int items_drop_booster = 0;
    public static int exp_booster = 0;

    public void Awake()
    {
        ItemsClass.loadData();
        refreshStats();
        Items items = ItemsClass.GetEquippedItems();

        for (int i = 0; i < items.items.Length; i++)
        {
            switch (items.items[i].item_classification)
            {
                case "weapon":
                    attack_rating += items.items[i].attack_rating;
                    break;
                case "helmet":
                    heal_points += items.items[i].heal_point;
                    break;
                case "armor":
                    defence += items.items[i].defence;
                    break;
                case "robers":
                    resistance += items.items[i].resistance;
                    break;
                case "boots":
                    dodge += items.items[i].dodge;
                    break;
                case "ring":
                    crit_damage += items.items[i].crit_damage;
                    break;
                case "amulet":
                    crit_chance += items.items[i].crit_chance;
                    break;
                case "gloves":
                    accuracy += items.items[i].accuracy;
                    break;
            }
        }
    }

    static public void updateStats()
    {

    }

    static public void refreshStats()
    {
        attack_rating = 0;
        heal_points = 0;
        defence = 0;
        dodge = 0;
        crit_chance = 0;
        crit_damage = 0;
        resistance = 0;
        accuracy = 0;
        dropped_gold_chance = 0;
        items_drop_booster = 0;
        exp_booster = 0;
    }
}
