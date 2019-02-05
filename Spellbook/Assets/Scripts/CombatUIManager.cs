using UnityEngine;
using UnityEngine.UI;

// script to manage UI in CombatScene
public class CombatUIManager : MonoBehaviour
{
    // serializefield private variables
    [SerializeField] private GameObject Panel_starting;
    [SerializeField] private GameObject Panel_main;
    [SerializeField] private GameObject Panel_inventory;
    [SerializeField] private GameObject Panel_help;
    [SerializeField] private Text Text_mana;

    // private variables
    private bool bInventoryOpen = false;
    private bool bHelpOpen = false;

    private CollectItemScript collectItemScript;

    // notify panel is also used in CollectItemScript
    public GameObject Panel_notify;
    public Text Text_notify;
    public bool bnotifyPanelOpen = false;

    Player localPlayer;

    private void Start()
    {
        collectItemScript = gameObject.GetComponent<CollectItemScript>();

        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        Text_mana.text = localPlayer.Spellcaster.iMana.ToString();
    }

    private void Update()
    {
        Text_mana.text = localPlayer.Spellcaster.iMana.ToString();
    }

    public void closePanels()
    {
        if (bInventoryOpen)
            Panel_inventory.SetActive(false);
        if (bHelpOpen)
            Panel_help.SetActive(false);
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
        collectItemScript.CollectSpellPiece("Arcane Spell Piece");
    }
    public void alchemySPClick()
    {
        collectItemScript.CollectSpellPiece("Alchemy Spell Piece");
    }
    public void chronomancySPClick()
    {
        collectItemScript.CollectSpellPiece("Time Spell Piece");
    }
    public void elementalSPClick()
    {
        collectItemScript.CollectSpellPiece("Elemental Spell Piece");
    }
    public void summoningSPClick()
    {
        collectItemScript.CollectSpellPiece("Summoning Spell Piece");
    }
    public void tricksterSPClick()
    {
        collectItemScript.CollectSpellPiece("Illusion Spell Piece");
    }
}
