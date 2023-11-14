using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hellper : MonoBehaviour
{
    public GameObject obj;

    public void SetObjInactive()
    {
        obj.SetActive(false);
    }
}
