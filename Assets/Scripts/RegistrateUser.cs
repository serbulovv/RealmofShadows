using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RegistrateUser : MonoBehaviour
{
    public InputField inputField;
    
    public void Registrate()
    {
        string userInput = inputField.text;

        if (!string.IsNullOrEmpty(userInput))
        {
            string current_user_json = "{\"current_user\":{\"init\":true, \"username\":\"" + userInput + "\", \"level\":1, \"next_level_exp\":120, \"title\":\"Nebulae Warriors\",\"money\":0, \"image_path\":\"Images/Units/main_character\", \"base_hp\":100, \"base_def\":0, \"base_phys_damage\":20}}";

            string levels_json = "{\"levels\":[{\"level_id\":100,\"level_name\":\"The Dark Descent\",\"level_unit_ids\":[1000,1001,1002,1003,1004,1005],\"unlocked\":true,\"level_image_path\":\"Images/Dungeons/the_dark_descent\"}," +
               "{\"level_id\":101,\"level_name\":\"The Ancient Pyramid\",\"level_unit_ids\":[1006, 1007, 1008, 1009, 1010, 1011],\"unlocked\":false,\"level_image_path\":\"Images/Dungeons/the_ancient_pyramid\"}," +
               "{\"level_id\":100,\"level_name\":\"The Dark Descent\",\"level_unit_ids\":[1001,1002,1003,1004,1005,1006],\"unlocked\":false,\"level_image_path\":\"no_image\"}," +
               "{\"level_id\":100,\"level_name\":\"The Dark Descent\",\"level_unit_ids\":[1001,1002,1003,1004,1005,1006],\"unlocked\":false,\"level_image_path\":\"no_image\"}," +
               "{\"level_id\":100,\"level_name\":\"The Dark Descent\",\"level_unit_ids\":[1001,1002,1003,1004,1005,1006],\"unlocked\":false,\"level_image_path\":\"no_image\"}," +
               "{\"level_id\":100,\"level_name\":\"The Dark Descent\",\"level_unit_ids\":[1001,1002,1003,1004,1005,1006],\"unlocked\":false,\"level_image_path\":\"no_image\"}]}";


            string current_user_inventory_json = "{\"inventor\":[]}";

            string units_json = "{\"units\":[" +
                "{\"unit_id\":1000,\"unlocked\":true,\"unit_name\":\"Dungeon Guardian\",\"unit_image_path\":\"Images/Units/dungeon_guard_100\",\"unit_level\":1,\"dropped_items_id\":[1,2],\"unit_base_hp\":40,\"unit_base_def\":1,\"unit_base_phys_damage\":20,\"level_to_unlock\":0}," +
                "{\"unit_id\":1001,\"unlocked\":false,\"unit_name\":\"Giant Rat\",\"unit_image_path\":\"Images/Units/huge_rat_100\",\"unit_level\":2,\"dropped_items_id\":[1,3],\"unit_base_hp\":50,\"unit_base_def\":1,\"unit_base_phys_damage\":20,\"level_to_unlock\":0}," +
                "{\"unit_id\":1002,\"unlocked\":false,\"unit_name\":\"Dungeon Skeleton\",\"unit_image_path\":\"Images/Units/dungeon_skeleton_100\",\"unit_level\":2,\"dropped_items_id\":[1,4],\"unit_base_hp\":50,\"unit_base_def\":1,\"unit_base_phys_damage\":40,\"level_to_unlock\":0}," +
                "{\"unit_id\":1003,\"unlocked\":false,\"unit_name\":\"Brutal Prisoner\",\"unit_image_path\":\"Images/Units/brutal_prisoner_100\",\"unit_level\":3,\"dropped_items_id\":[1,5],\"unit_base_hp\":200,\"unit_base_def\":1,\"unit_base_phys_damage\":20,\"level_to_unlock\":0}," +
                "{\"unit_id\":1004,\"unlocked\":false,\"unit_name\":\"Defiled Knight\",\"unit_image_path\":\"Images/Units/defiled_knight_100\",\"unit_level\":4,\"dropped_items_id\":[1,6],\"unit_base_hp\":200,\"unit_base_def\":1,\"unit_base_phys_damage\":20,\"level_to_unlock\":0}," +
                "{\"unit_id\":1005,\"unlocked\":true,\"unit_name\":\"Ruler Of The Descent\",\"unit_image_path\":\"Images/Units/ruler_of_the_descent_100\",\"unit_level\":5,\"dropped_items_id\":[1,7],\"unit_base_hp\":300,\"unit_base_def\":1,\"unit_base_phys_damage\":20,\"level_to_unlock\":101}," +
                "{\"unit_id\":1006,\"unlocked\":false,\"unit_name\":\"Desert Scorpion\",\"unit_image_path\":\"Images/Units/desert_scorpion_101\",\"unit_level\":5,\"dropped_items_id\":[1,7],\"unit_base_hp\":100,\"unit_base_def\":1,\"unit_base_phys_damage\":20,\"level_to_unlock\":101}," +
                "{\"unit_id\":1007,\"unlocked\":false,\"unit_name\":\"Pharaoh Sentinel\",\"unit_image_path\":\"Images/Units/pharaoh_sentinel_101\",\"unit_level\":6,\"dropped_items_id\":[1,7],\"unit_base_hp\":100,\"unit_base_def\":1,\"unit_base_phys_damage\":20,\"level_to_unlock\":101}," +
                "{\"unit_id\":1008,\"unlocked\":false,\"unit_name\":\"Darkness Siphoner\",\"unit_image_path\":\"Images/Units/darkness_siphoner_101\",\"unit_level\":6,\"dropped_items_id\":[1,7],\"unit_base_hp\":100,\"unit_base_def\":1,\"unit_base_phys_damage\":20,\"level_to_unlock\":101}," +
                "{\"unit_id\":1009,\"unlocked\":false,\"unit_name\":\"Tempest Sphinx\",\"unit_image_path\":\"Images/Units/tempest_sphinx_101\",\"unit_level\":7,\"dropped_items_id\":[1,7],\"unit_base_hp\":100,\"unit_base_def\":1,\"unit_base_phys_damage\":20,\"level_to_unlock\":101}," +
                "{\"unit_id\":1010,\"unlocked\":false,\"unit_name\":\"Sand Wraith\",\"unit_image_path\":\"Images/Units/sand_wraith_101\",\"unit_level\":8,\"dropped_items_id\":[1,7],\"unit_base_hp\":100,\"unit_base_def\":1,\"unit_base_phys_damage\":20,\"level_to_unlock\":101}" +
                "{\"unit_id\":1011,\"unlocked\":false,\"unit_name\":\"Sand Creature\",\"unit_image_path\":\"Images/Units/sand_wraith_101\",\"unit_level\":8,\"dropped_items_id\":[1,7],\"unit_base_hp\":100,\"unit_base_def\":1,\"unit_base_phys_damage\":20,\"level_to_unlock\":101}" +
                "]}";


            string items_json = "{\"items\":[" +
              "{\"item_id\": 1, \"item_name\": \"Common Upgrage Stone\", \"drop_chance\": 0.7, \"item_type\": \"resource\", \"avg_amount\": 10, \"image_path\": \"Images/Items/common-upgrade-stone\", \"uniq\": false, \"rarity\": \"Common\", \"heal_point\": 0, \"attack_rating\": 0, \"defence\": 0, \"dodge\": 0, \"crit_chance\": 0, \"crit_damage\": 0, \"resistance\": 0, \"accuracy\": 0, \"item_classification\": \"\", \"equiped\": false}," +
              "{\"item_id\": 2, \"item_name\": \"Armor fragment\", \"drop_chance\": 0.5, \"item_type\": \"resource\", \"avg_amount\": 3, \"image_path\": \"Images/Items/armor-fragment\", \"uniq\": false, \"rarity\": \"Rare\", \"heal_point\": 0, \"attack_rating\": 0, \"defence\": 0, \"dodge\": 0, \"crit_chance\": 0, \"crit_damage\": 0, \"resistance\": 0, \"accuracy\": 0, \"item_classification\": \"\", \"equiped\": false}," +
              "{\"item_id\": 3, \"item_name\": \"Rat fur\", \"drop_chance\": 0.7, \"item_type\": \"resource\", \"avg_amount\": 3, \"image_path\": \"Images/Items/rat-fur\", \"uniq\": false, \"rarity\": \"Legendary\", \"heal_point\": 0, \"attack_rating\": 0, \"defence\": 0, \"dodge\": 0, \"crit_chance\": 0, \"crit_damage\": 0, \"resistance\": 0, \"accuracy\": 0, \"item_classification\": \"\", \"equiped\": false}," +
              "{\"item_id\": 4, \"item_name\": \"Bones\", \"drop_chance\": 0.9, \"item_type\": \"resource\", \"avg_amount\": 10, \"image_path\": \"Images/Items/bones-1\", \"uniq\": false, \"rarity\": \"Deadly\", \"heal_point\": 0, \"attack_rating\": 0, \"defence\": 0, \"dodge\": 0, \"crit_chance\": 0, \"crit_damage\": 0, \"resistance\": 0, \"accuracy\": 0, \"item_classification\": \"\", \"equiped\": false}," +
              "{\"item_id\": 5, \"item_name\": \"Zombie fingers\", \"drop_chance\": 0.7, \"item_type\": \"resource\", \"avg_amount\": 3, \"image_path\": \"Images/Items/zombie-fingers\", \"uniq\": false, \"rarity\": \"Magic\", \"heal_point\": 0, \"attack_rating\": 0, \"defence\": 0, \"dodge\": 0, \"crit_chance\": 0, \"crit_damage\": 0, \"resistance\": 0, \"accuracy\": 0, \"item_classification\": \"\", \"equiped\": false}," +
              "{\"item_id\": 6, \"item_name\": \"Crushed blue minerals\", \"drop_chance\": 0.3, \"item_type\": \"resource\", \"avg_amount\": 10, \"image_path\": \"Images/Items/crushed-blue-minerals\", \"uniq\": false, \"rarity\": \"Common\", \"heal_point\": 0, \"attack_rating\": 0, \"defence\": 0, \"dodge\": 0, \"crit_chance\": 0, \"crit_damage\": 0, \"resistance\": 0, \"accuracy\": 0, \"item_classification\": \"\", \"equiped\": false}," +
              "{\"item_id\": 7, \"item_name\": \"Worn fabric\", \"drop_chance\": 0.4, \"item_type\": \"resource\", \"avg_amount\": 3, \"image_path\": \"Images/Items/warn-fabric\", \"uniq\": false, \"rarity\": \"Common\", \"heal_point\": 0, \"attack_rating\": 0, \"defence\": 0, \"dodge\": 0, \"crit_chance\": 0, \"crit_damage\": 0, \"resistance\": 0, \"accuracy\": 0, \"item_classification\": \"\", \"equiped\": false}," +
              "{\"item_id\": 8, \"item_name\": \"Iron Sword\", \"drop_chance\": 1.0, \"item_type\": \"stuff\", \"avg_amount\": 1, \"image_path\": \"Images/Items/sword\", \"uniq\": true, \"rarity\": \"Common\", \"heal_point\": 0, \"attack_rating\": 40, \"defence\": 0, \"dodge\": 0, \"crit_chance\": 2, \"crit_damage\": 0, \"resistance\": 0, \"accuracy\": 0, \"item_classification\": \"weapon\", \"equiped\": false}," +
              "{\"item_id\": 9, \"item_name\": \"Iron Helmet\", \"drop_chance\": 1.0, \"item_type\": \"stuff\", \"avg_amount\": 1, \"image_path\": \"Images/Items/warn-fabric\", \"uniq\": true, \"rarity\": \"Deadly\", \"heal_point\": 20, \"attack_rating\": 0, \"defence\": 0, \"dodge\": 0, \"crit_chance\": 0, \"crit_damage\": 0, \"resistance\": 0, \"accuracy\": 0, \"item_classification\": \"helmet\", \"equiped\": false}" +
              "]}";

            string crafts_json = "{\"crafts\": [" +
                "{\"result_item_id\": 8, \"components_id\": [2,4], \"amount\": [20, 50], \"min_level\": 2}," +
                "{\"result_item_id\": 9, \"components_id\": [3,5], \"amount\": [20, 10], \"min_level\": 3}" +
                "]}";

            PlayerPrefs.SetString("current_user", current_user_json);
            PlayerPrefs.SetString("levels", levels_json);
            PlayerPrefs.SetString("units", units_json);
            PlayerPrefs.SetString("inventory", current_user_inventory_json);
            PlayerPrefs.SetString("items", items_json);
            PlayerPrefs.SetString("crafts", crafts_json);
            PlayerPrefs.Save();
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            Debug.LogError("Error: Input field is empty!");
        }
    }
}
