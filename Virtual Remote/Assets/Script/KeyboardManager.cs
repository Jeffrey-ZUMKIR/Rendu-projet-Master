using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    public TMP_InputField inputField;


    public void PressKey(string key)
    {
        inputField.text += key;
    }

    public void ResetKey()
    {
        inputField.text = "";
    }
}
