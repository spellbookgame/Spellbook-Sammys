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

        // if current scene is vuforia, remove ribbon from panel
        if (SceneManager.GetActiveScene().name.Equals("VuforiaScene"))
        {
            ribbon.SetActive(false);
        }

        singleButton.onClick.AddListener((okClick));

        gameObject.SetActive(true);

        if (!PanelHolder.panelQueue.Peek().Equals(panelID))
        {
            DisablePanel();
        }
    }
    private void okClick()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        gameObject.SetActive(false);
        SceneManager.LoadScene("MainPlayerScene");

        if(PanelHolder.panelQueue.Count > 0)
            PanelHolder.panelQueue.Dequeue();
        PanelHolder.instance.CheckPanelQueue();
    }
}