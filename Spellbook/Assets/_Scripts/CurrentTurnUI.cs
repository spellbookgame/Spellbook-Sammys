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
        currentTurnStatus.text = "Current Turn: " + NetworkGameState.instance.getTurnSpellcasterName();
    }

    public void UpdateText()
    {
        currentTurnStatus.text = "Current Turn: " + NetworkGameState.instance.getTurnSpellcasterName();
    }

    //TODO take out of Update later and fix bug.  This is just a patch so we can turn it in on time.
    void Update()
    {
        currentTurnStatus.text = "Current Turn: " + NetworkGameState.instance.getTurnSpellcasterName();
    }

}
