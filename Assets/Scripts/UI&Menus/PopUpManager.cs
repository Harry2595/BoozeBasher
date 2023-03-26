using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    //Menus
    public GameObject playerUI;
    public GameObject settingsMenu;

    //Text
    public GameObject openSettingsTXT;

    //Bools
    bool inSettingsBuilding = false;
    bool inSettings = false;

    //Refs
    public Player PlayerRef;

    void Update()
    {
        if (inSettingsBuilding)
        {
            if(inSettings != true)
            {
                openSettingsTXT.SetActive(true);
            }

            if (Input.GetKeyDown("e"))
            {
                OpenSettings();
            }
        }
        else
        {
            openSettingsTXT.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Settings"))
        {
            inSettingsBuilding = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Settings"))
        {
            inSettingsBuilding = false;
        }
    }

    public void OpenSettings()
    {
        PlayerRef.canMove = false;

        openSettingsTXT.SetActive(false);
        playerUI.SetActive(false);
        settingsMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        inSettings = true;
    }

    public void CloseSettings()
    {
        PlayerRef.canMove = true;
        playerUI.SetActive(true);
        settingsMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        inSettings = false;
    }
}
