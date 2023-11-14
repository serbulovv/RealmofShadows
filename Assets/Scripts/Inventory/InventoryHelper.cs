using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryHelper : MonoBehaviour
{
    static public int itemId;
    public GameObject materialsList;
    public GameObject stuffList;

    public void RemoveItem()
    {
        ItemsClass.ResetItemFields(itemId);
        CurrentUserInventoryClass.RemoveItemFromInventory(itemId);
        SceneManager.LoadScene("InventoryScene");
    }
    
    public void EquipItem()
    {
        ItemsClass.EquipItem(itemId);
        SceneManager.LoadScene("InventoryScene");
    }
}
