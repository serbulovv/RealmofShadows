using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegistrationInputForm : MonoBehaviour
{
    public InputField inputField;
    public int maxCharacterLimit = 10;

    private void Start()
    {
        // Set the maximum character limit for the input field
        inputField.characterLimit = maxCharacterLimit;
    }
}
