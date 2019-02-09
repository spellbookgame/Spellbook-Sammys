using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    private bool panelOpen = false;
    [SerializeField] private GameObject panel;
    private GameObject panelClone;
    private Button button;
    
    public void showPanel()
    {
        if(!panelOpen)
        {
            panelClone = Instantiate(panel);
            panelClone.transform.SetParent(GameObject.Find("Canvas").transform);
            panelClone.transform.localPosition = new Vector3(0, 0, 0);
            panelClone.transform.localScale = new Vector3(1, 1, 1);

            button = panelClone.transform.GetChild(1).GetComponent<Button>();
            button.onClick.AddListener(okClick);

            panelOpen = true;
        }
    }

    public void setPanelText(string text)
    {
        panelClone.transform.GetChild(0).GetComponent<Text>().text = text;
    }
     
    private void okClick()
    {
        if(panelOpen)
        {
            Destroy(panelClone.gameObject);
            panelOpen = false;
        }
    }
}
