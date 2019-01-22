using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectItemScript : MonoBehaviour
{
    [SerializeField] GameObject notifyPanel;
    [SerializeField] Text notifyText;

    public bool bnotifyPanelOpen;
    private CombatUIManager combatUIManager;
    private HealthManager healthManager;

    // Start is called before the first frame update
    void Start()
    {
        bnotifyPanelOpen = false;

        GameObject eventSystem = GameObject.Find("EventSystem");
        combatUIManager = eventSystem.GetComponent<CombatUIManager>();
        healthManager = eventSystem.GetComponent<HealthManager>();
    }

    public void CollectSpellPiece()
    {
        // setting notifyPanel to active
        if (bnotifyPanelOpen == false)
        {
            combatUIManager.closePanels();
            notifyPanel.SetActive(true);
            bnotifyPanelOpen = true;
        }
        // increment player's number of spell pieces
        healthManager.player.numSpellPieces++;

        // setting text of notification panel
        notifyText.text = "You found a spell piece!\n\nYou now have " + 
                            healthManager.player.numSpellPieces + " spell pieces.";
    }

    public void closeNotifyPanel()
    {
        if(bnotifyPanelOpen == true)
        {
            notifyPanel.SetActive(false);
            bnotifyPanelOpen = false;
        }
    }
}
