using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject go_KioskUIPanel;
    [SerializeField] GameObject go_MovieSelection;
    [SerializeField] GameObject go_MovieInformation;
    [SerializeField] GameObject go_MovieTime;
    // Start is called before the first frame update
    public void OnClick()
    {
        go_KioskUIPanel.SetActive(true);
        go_MovieSelection.SetActive(false);
        go_MovieInformation.SetActive(false);
        go_MovieTime.SetActive(false);

    }
}
