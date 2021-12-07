using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieTime : MonoBehaviour
{
    [SerializeField] GameObject go_KioskUIPanel;
    [SerializeField] GameObject go_MovieTime;
    public void OnClick()
    {
        go_KioskUIPanel.SetActive(false);
        go_MovieTime.SetActive(true);

    }
}
