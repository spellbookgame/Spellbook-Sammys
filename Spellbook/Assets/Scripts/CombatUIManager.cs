using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

// script to manage UI in CombatScene
public class CombatUIManager : MonoBehaviour
{
    // serializefield private variables
    [SerializeField] private GameObject Panel_starting;
    [SerializeField] private GameObject Panel_main;
    [SerializeField] private GameObject Panel_inventory;
    [SerializeField] private GameObject Panel_help;
    [SerializeField] private GameObject Panel_spell;

    // private variables
    private bool bInventoryOpen = false;
    private bool bHelpOpen = false;
    private bool bSpellOpen = false;

    // when ok button is clicked
    public void okClick()
    {
        Panel_main.SetActive(true);
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

    // when help button is clicked
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

    // when spell button is clicked
    public void spellClick()
    {
        if (bSpellOpen == false)
        {
            Panel_spell.SetActive(true);
            bSpellOpen = true;
        }
        else if (bSpellOpen == true)
        {
            Panel_spell.SetActive(false);
            bSpellOpen = false;
        }
    }
}
