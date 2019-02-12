using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    private bool panelOpen = false;
    private GameObject panelClone;
    private Button button;
    private Image image;

    [SerializeField] private GameObject panel;

    public void ShowPanel()
    {
        if(!panelOpen)
        {
            panelClone = Instantiate(panel);
            panelClone.transform.SetParent(GameObject.Find("Canvas").transform);
            panelClone.transform.localPosition = new Vector3(0, 0, 0);
            panelClone.transform.localScale = new Vector3(1, 1, 1);

            button = panelClone.transform.GetChild(1).GetComponent<Button>();
            button.onClick.AddListener(OkClick);

            panelOpen = true;
        }
    }

    // fix this to load resources of each image
    public void SetPanelImage(string imageName)
    {
        image = panelClone.transform.GetChild(2).GetComponent<Image>();
        // not working D:
        image.sprite = Resources.Load("Spell Pieces/" + imageName) as Sprite;
    }

    public void SetPanelText(string text)
    {
        panelClone.transform.GetChild(0).GetComponent<Text>().text = text;
    }
     
    private void OkClick()
    {
        if(panelOpen)
        {
            Destroy(panelClone.gameObject);
            panelOpen = false;
        }
    }
}
