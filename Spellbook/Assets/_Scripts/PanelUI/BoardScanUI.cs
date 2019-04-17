using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

// used to display all notification panels
public class BoardScanUI : MonoBehaviour
{
    [SerializeField] private Text titleText;
    [SerializeField] private Text infoText;
    [SerializeField] private Button singleButton;
    [SerializeField] private GameObject ribbon;

    public bool panelActive = false;
    public string panelID = "boardscan";

    private void DisablePanel()
    {
        gameObject.SetActive(false);
    }
    public void EnablePanel()
    {
        gameObject.SetActive(true);
    }

    public void DisplayScanEvent(string title, string info, Sprite sprite)
    {
        titleText.text = title;
        infoText.text = info;
        gameObject.transform.Find("Image").GetComponent<Image>().sprite = sprite;

        // if current scene is Vuforia, change everything to image
        if(SceneManager.GetActiveScene().name.Equals("VuforiaScene"))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Image>().enabled = true;

            foreach (Transform t in ribbon.transform)
            {
                t.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                t.gameObject.GetComponent<Image>().enabled = true;
            }
        }

        singleButton.onClick.AddListener((eventClick));

        gameObject.SetActive(true);

        if (!PanelHolder.panelQueue.Peek().Equals(panelID))
        {
            DisablePanel();
        }
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