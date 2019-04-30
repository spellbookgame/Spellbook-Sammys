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
        /*if (SceneManager.GetActiveScene().name.Equals("VuforiaScene"))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Image>().enabled = true;
        }*/

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
            case "Main":
                singleButton.onClick.AddListener((MainClick));
                break;
            default:
                break;
        }

        gameObject.SetActive(true);

        // if proclamation panel is found in the scene, disable this panel 
        if (GameObject.Find("Proclamation Panel"))
        {
            DisablePanel();
        }

        // if next panel in queue is NOT a notify panel, disable this panel
        if (!PanelHolder.panelQueue.Peek().Equals(panelID))
        {
            DisablePanel();
        }
    }
    
    private void OkClick()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        gameObject.SetActive(false);

        if (PanelHolder.panelQueue.Count > 0)
            PanelHolder.panelQueue.Dequeue();
        PanelHolder.instance.CheckPanelQueue();
    }
    private void VuforiaClick()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        gameObject.SetActive(false);
        SceneManager.LoadScene("VuforiaScene");

        if (PanelHolder.panelQueue.Count > 0)
            PanelHolder.panelQueue.Dequeue();
        PanelHolder.instance.CheckPanelQueue();
    }
    private void ShopClick()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        gameObject.SetActive(false);
        SceneManager.LoadScene("ShopScene");

        if (PanelHolder.panelQueue.Count > 0)
            PanelHolder.panelQueue.Dequeue();
        PanelHolder.instance.CheckPanelQueue();
    }
    private void MainClick()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        gameObject.SetActive(false);
        SceneManager.LoadScene("MainPlayerScene");

        if(PanelHolder.panelQueue.Count > 0)
            PanelHolder.panelQueue.Dequeue();
        PanelHolder.instance.CheckPanelQueue();
    }
}