using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextOutput : MonoBehaviour
{
    public InputField inputField;
    public void OnClick()
    {
        inputField.Select();
        inputField.text = "";
    }
}
