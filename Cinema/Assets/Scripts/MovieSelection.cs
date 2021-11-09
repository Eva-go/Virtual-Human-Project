using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieSelection : MonoBehaviour
{
    [SerializeField] GameObject go_KioskUIPanel;
    [SerializeField] GameObject go_MovieSelection;
    // Start is called before the first frame update
    public void OnClick()
    {
        go_KioskUIPanel.SetActive(false);
        go_MovieSelection.SetActive(true);

    }
}
