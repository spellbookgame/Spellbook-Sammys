using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// script to manage UI in CombatScene
// CURRENT ISSUE: panels can open on top of each other
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

    private CollectItemScript collectItemScript;

    // notify panel is also used in CollectItemScript
    public GameObject Panel_notify;
    public Text Text_notify;
    public bool bnotifyPanelOpen = false;

    private void Start()
    {
        collectItemScript = gameObject.GetComponent<CollectItemScript>();
    }

    public void closePanels()
    {
        if (bInventoryOpen)
            Panel_inventory.SetActive(false);
        if (bHelpOpen)
            Panel_help.SetActive(false);
        if (bSpellOpen)
            Panel_spell.SetActive(false);
    }

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

    // when OK button on Panel_notify is clicked, close the panel
    public void closeNotifyPanel()
    {
        if (bnotifyPanelOpen == true)
        {
            Panel_notify.SetActive(false);
            bnotifyPanelOpen = false;
        }
    }

    // ----------------------------------- DEBUGGING: ALL SPELL PIECE BUTTONS ------------------------------------------
    public void arcaneSPClick()
    {
        collectItemScript.CollectSpellPiece("Arcane");
    }
    public void alchemySPClick()
    {
        collectItemScript.CollectSpellPiece("Alchemy");
    }
    public void chronomancySPClick()
    {
        collectItemScript.CollectSpellPiece("Chronomancy");
    }
    public void elementalSPClick()
    {
        collectItemScript.CollectSpellPiece("Elemental");
    }
    public void summoningSPClick()
    {
        collectItemScript.CollectSpellPiece("Summoning");
    }
    public void tricksterSPClick()
    {
        collectItemScript.CollectSpellPiece("Trickster");
    }
}
