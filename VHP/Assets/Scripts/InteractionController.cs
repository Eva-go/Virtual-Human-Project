using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    RaycastHit hitInfo;
    [SerializeField] Camera cam;
    [SerializeField] GameObject go_NoramlCrossHair;
    [SerializeField] GameObject go_InteractiveCrossHair;
    [SerializeField] GameObject go_KioskUIPanel;
    [SerializeField] GameObject go_BackGround;
    [SerializeField] GameObject go_MovieSelection;
    [SerializeField] GameObject go_MovieInformation;
    [SerializeField] GameObject go_MovieTime;
    [SerializeField] GameObject go_Text;
    // Update is called once per frame

    bool isContact = false;
    bool isInteractionView = false;
   
    void Start()
    {
        go_KioskUIPanel.SetActive(false);
        go_BackGround.SetActive(false);
        go_MovieSelection.SetActive(false);
        go_MovieInformation.SetActive(false);
        go_MovieTime.SetActive(false);
        go_Text.SetActive(false);


    }
    void Update()
    {
        CheckObject();
        Chatting();
    }

    private void Chatting()
    {
        if (PlayerController.isChatting)
        {
            PlayerController.isKoskPause = !PlayerController.isKoskPause;
            go_Text.SetActive(true);
            go_NoramlCrossHair.SetActive(false);
            Cursor.visible = true;

        }
        else
        {
            
            go_Text.SetActive(false);
            go_NoramlCrossHair.SetActive(true);
            Cursor.visible = false;
        }
            
    }

    private void CheckObject()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, 20))
        {
            Debug.DrawRay(cam.transform.position, cam.transform.forward * hitInfo.distance, Color.red);
            Contect();
            
        }
        else
        {
            NotContect();
        }
    }
    void Contect()
    {
        if (hitInfo.transform.CompareTag("Interaction"))
        {
            if(!isContact)
            {
                isContact = true;
                go_InteractiveCrossHair.SetActive(true);
                go_NoramlCrossHair.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                React();
                PlayerController.isKoskPause = !PlayerController.isKoskPause;
                Debug.Log(PlayerController.isKoskPause);
               
                //PlayerController.isKoskPause = !PlayerController.isKoskPause;

            }
        }
        else
        {
            NotContect();
        }
    }
    void React()
    {
        isInteractionView = !isInteractionView;
        if(isInteractionView)
        {
           go_KioskUIPanel.SetActive(true);
           go_BackGround.SetActive(true);
        }
        else
        {
            go_KioskUIPanel.SetActive(false);
            go_BackGround.SetActive(false);
        }
    }
    void NotContect()
    {
        if(isContact)
        {
            isContact = false;
            go_InteractiveCrossHair.SetActive(false);
            go_NoramlCrossHair.SetActive(true);
        }
       
    }
}
