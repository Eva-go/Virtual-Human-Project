using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
    public InputField inputField;
    public void Chat(Text text)
    {
        text.text = inputField.text;
    }

}
