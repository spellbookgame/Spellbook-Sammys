using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

// used to display all notification panels
public class PlayerChooseUI : MonoBehaviour
{
    [SerializeField] private Button bAlchemist;
    [SerializeField] private Button bArcanist;
    [SerializeField] private Button bChronomancer;
    [SerializeField] private Button bElementalist;
    [SerializeField] private Button bSummoner;
    [SerializeField] private Button bTrickster;

    public bool panelActive = false;
    public string panelID = "playerchoose";

    private void DisablePanel()
    {
        gameObject.SetActive(false);
    }
    public void EnablePanel()
    {
        gameObject.SetActive(true);
    }

    // used to notify players of various events. input a buttonClick string to change the onClick listener
    public void DisplayPlayerChoose()
    {
        // if current scene is Vuforia, change everything to image
        if (SceneManager.GetActiveScene().name.Equals("VuforiaScene"))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Image>().enabled = true;
        }

        gameObject.SetActive(true);

        // for start of game
        if (GameObject.Find("Proclamation Panel"))
        {
            DisablePanel();
        }

        if (!PanelHolder.panelQueue.Peek().Equals(panelID))
        {
            DisablePanel();
        }
    }
    
    private void OkClick()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        gameObject.SetActive(false);

        PanelHolder.panelQueue.Dequeue();
        PanelHolder.instance.CheckPanelQueue();
    }
}