using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelHolder : MonoBehaviour
{
    public YourTurnUI yourTurnPanel;
    // Start is called before the first frame update
    
    public void displayYourTurn()
    {
        yourTurnPanel.Display();
    }
}
