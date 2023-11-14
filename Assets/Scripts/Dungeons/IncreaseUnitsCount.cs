using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseUnitsCount : MonoBehaviour
{
    public Text countText;
    public int value = 0;

    public void ChangeValue()
    {
        int parsedNumber = int.Parse(countText.text);
        if (parsedNumber == 1 && value == -1) { return; }
        if (parsedNumber == 10 && value == 1) { return; }

        parsedNumber += value;
        LevelDataHolder._unitsCount = parsedNumber;
        countText.text = parsedNumber.ToString();
    }
}
