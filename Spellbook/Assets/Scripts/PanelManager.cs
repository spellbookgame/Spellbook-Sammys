using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    private bool panelOpen = false;
    [SerializeField] private GameObject panel;

    public void showPanel()
    {
        if(!panelOpen)
        {
            panel.SetActive(true);
            panelOpen = true;
        }
    }

    public void setPanelText(string text)
    {
        panel.transform.GetChild(0).GetComponent<Text>().text = text;
    }

    public void okClick()
    {
        if(panelOpen)
        {
            panel.SetActive(false);
            panelOpen = false;
        }
    }
}
