using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

// script to manage combat in DungeonScene
public class CombatManager : MonoBehaviour
{
    // serializefield private variables
    [SerializeField]
    private Button Button_ok;

    [SerializeField]
    private GameObject Panel_starting;

    [SerializeField]
    private GameObject Panel_choices;

    [SerializeField]
    private GameObject Panel_inventory;

    [SerializeField]
    private GameObject Panel_help;

    // private variables
    private bool bInventoryOpen = false;
    private bool bHelpOpen = false;

    // when ok button is clicked
    public void okClick()
    {
        Panel_choices.SetActive(true);
        Panel_starting.SetActive(false);
    }

    // when inventory button is clicked
    public void inventoryClick()
    {
        if (bInventoryOpen == false)
        {
            Panel_inventory.SetActive(true);
            bInventoryOpen = true;
        }
        else if (bInventoryOpen == true)
        {
            Panel_inventory.SetActive(false);
            bInventoryOpen = false;
        }
    }

    public void helpClick()
    {
        if (bHelpOpen == false)
        {
            Panel_help.SetActive(true);
            bHelpOpen = true;
        }
        else if (bHelpOpen == true)
        {
            Panel_help.SetActive(false);
            bHelpOpen = false;
        }
    }
}
