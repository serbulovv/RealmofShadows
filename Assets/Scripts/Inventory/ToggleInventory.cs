using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleInventory : MonoBehaviour
{
    public GameObject materialsList;
    public GameObject stuffList;

    public void ToogleMaterials()
    {
        materialsList.SetActive(true);
        stuffList.SetActive(false);
    }

    public void ToogleStuff()
    {
        materialsList.SetActive(false);
        stuffList.SetActive(true);
    }
}
