using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectItemScript : MonoBehaviour
{
    [SerializeField] GameObject notifyPanel;
    [SerializeField] Text notifyText;
    private bool bnotifyPanelOpen;

    // Start is called before the first frame update
    void Start()
    {
        bnotifyPanelOpen = false;
    }

    public void CollectSpellPiece()
    {
        // setting notifyPanel to active
        if (bnotifyPanelOpen == false)
        {
            notifyPanel.SetActive(true);
            bnotifyPanelOpen = true;
        }
        // setting text of notification panel
        notifyText.text = "You found a spell piece!";
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
