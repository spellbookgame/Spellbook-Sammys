using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectItemScript : MonoBehaviour
{
    private CombatUIManager combatUIManager;
    Player localPlayer;

    // Start is called before the first frame update
    void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        GameObject scriptContainer = GameObject.Find("ScriptContainer");
        combatUIManager = scriptContainer.GetComponent<CombatUIManager>();
    }

    public void CollectSpellPiece()
    {
        // setting notifyPanel to active
        if (combatUIManager.bnotifyPanelOpen == false)
        {
            combatUIManager.closePanels();
            combatUIManager.Panel_notify.SetActive(true);
            combatUIManager.bnotifyPanelOpen = true;
        }
        // increment player's number of spell pieces
        localPlayer.Spellcaster.numSpellPieces++;

        // setting text of notification panel
        combatUIManager.Text_notify.text = "You found a spell piece!\n\nYou now have " +
                            localPlayer.Spellcaster.numSpellPieces + " spell pieces.";
    }
}
