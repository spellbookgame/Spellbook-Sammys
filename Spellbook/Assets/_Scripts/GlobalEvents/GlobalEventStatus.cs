using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalEventStatus : MonoBehaviour
{
    public Text text_GlobalEventStatus;
    // Start is called before the first frame update
    void Start()
    {
        text_GlobalEventStatus.text = NetworkGameState.instance.getGlobalEventString();
    }

    public void OnYearPassed()
    {
        text_GlobalEventStatus.text = NetworkGameState.instance.getGlobalEventString();
    }

}
