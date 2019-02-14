using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelHolder : MonoBehaviour
{
    public YourTurnUI yourTurnPanel;
    public EventUI eventPanel;

    // Start is called before the first frame update
    public static PanelHolder instance = null;

    private void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);
    }

    public void displayYourTurn()
    {
        yourTurnPanel.Display();
    }

    public void displayEvent(string evnt)
    {
        eventPanel.Display(evnt);
    }
}
