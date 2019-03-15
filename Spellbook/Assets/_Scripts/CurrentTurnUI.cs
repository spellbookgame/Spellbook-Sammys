using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentTurnUI : MonoBehaviour
{

    public Text currentTurnStatus;

    // Start is called before the first frame update
    void Start()
    {
        currentTurnStatus.text = "Current Turn: "+ NetworkGameState.instance.getTurnSpellcasterName();
    }

    
}
