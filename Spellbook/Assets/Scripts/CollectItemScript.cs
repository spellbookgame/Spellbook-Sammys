using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectItemScript : MonoBehaviour
{
    private CombatUIManager combatUIManager;

    // Start is called before the first frame update
    void Start()
    {
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
        ControlScript.player.numSpellPieces++;

        // setting text of notification panel
        combatUIManager.Text_notify.text = "You found a spell piece!\n\nYou now have " +
                            ControlScript.player.numSpellPieces + " spell pieces.";
    }
}
