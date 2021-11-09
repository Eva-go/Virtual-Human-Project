using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieInformation : MonoBehaviour
{
    [SerializeField] GameObject go_KioskUIPanel;
    [SerializeField] GameObject go_MovieInformation;
    // Start is called before the first frame update
    public void OnClick()
    {
        Debug.Log("버튼클릭");
        go_KioskUIPanel.SetActive(false);
        go_MovieInformation.SetActive(true);

    }
}
