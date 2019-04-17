using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

// used to display all notification panels
public class NotifyUI : MonoBehaviour
{
    [SerializeField] private Text titleText;
    [SerializeField] private Text infoText;
    [SerializeField] private Button singleButton;

    public bool panelActive = false;
    public string panelID = "notify";

    private void DisablePanel()
    {
        gameObject.SetActive(false);
    }
    public void EnablePanel()
    {
        gameObject.SetActive(true);
    }

    // used to notify players of various events. input a buttonClick string to change the onClick listener
    public void DisplayNotify(string title, string info, string buttonClick)
    {
        titleText.text = title;
        infoText.text = info;

        // if current scene is Vuforia, change everything to image
        if (SceneManager.GetActiveScene().name.Equals("VuforiaScene"))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Image>().enabled = true;
        }

        // different onclick listeners for different button inputs
        switch(buttonClick)
        {
            case "OK":
                singleButton.onClick.AddListener((OkClick));
                break;
            case "Vuforia":
                singleButton.onClick.AddListener((VuforiaClick));
                break;
            case "Shop":
                singleButton.onClick.AddListener((ShopClick));
                break;
            default:
                break;
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
    // Display Event button click leads to player ending turn and going to home scene
    public void DisplayEvent(string title, string info)
    {
        titleText.text = title;
        infoText.text = info;

        // if current scene is Vuforia, change everything to image
        if (SceneManager.GetActiveScene().name.Equals("VuforiaScene"))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Image>().enabled = true;
        }

        singleButton.onClick.AddListener((eventClick));

        gameObject.SetActive(true);

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
    private void VuforiaClick()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        gameObject.SetActive(false);
        SceneManager.LoadScene("VuforiaScene");

        PanelHolder.panelQueue.Dequeue();
        PanelHolder.instance.CheckPanelQueue();
    }
    private void ShopClick()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        gameObject.SetActive(false);
        SceneManager.LoadScene("ShopScene");

        PanelHolder.panelQueue.Dequeue();
        PanelHolder.instance.CheckPanelQueue();
    }
    private void eventClick()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        gameObject.SetActive(false);


        Debug.Log("On click");
        //GameObject player = GameObject.Find("LocalPlayer(Clone)");
        GameObject player = GameObject.FindGameObjectWithTag("LocalPlayer");

        bool endSuccessful = player.GetComponent<Player>().onEndTurnClick();
        if (endSuccessful)
        {
            player.GetComponent<Player>().Spellcaster.hasAttacked = false;
            player.GetComponent<Player>().Spellcaster.turnJustEnded = true;

            Scene m_Scene = SceneManager.GetActiveScene();
            if (m_Scene.name != "MainPlayerScene")
            {
                SceneManager.LoadScene("MainPlayerScene");
            }

        }
        PanelHolder.panelQueue.Dequeue();
        PanelHolder.instance.CheckPanelQueue();
    }
}