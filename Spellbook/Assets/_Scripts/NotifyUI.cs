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

    public void DisplayNotify(string title, string info)
    {
        titleText.text = title;
        infoText.text = info;

        singleButton.onClick.AddListener((okClick));

        gameObject.SetActive(true);

        if (GameObject.Find("Proclamation Panel"))
        {
            DisablePanel();
            /*if (GameObject.FindGameObjectWithTag("LocalPlayer") && GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>().Spellcaster.procPanelShown == false)
            {
                DisablePanel();
                Debug.Log("notify panel disabled");
            }*/
        }

        if (!PanelHolder.panelQueue.Peek().Equals(panelID))
        {
            DisablePanel();
        }
    }
    public void DisplayEvent(string title, string info)
    {
        titleText.text = title;
        infoText.text = info;

        singleButton.onClick.AddListener((eventClick));

        gameObject.SetActive(true);

        if (!PanelHolder.panelQueue.Peek().Equals(panelID))
        {
            DisablePanel();
        }
    }
    public void DisplayCombat(string title, string info)
    {
        titleText.text = title;
        infoText.text = info;

        singleButton.onClick.AddListener((combatClick));

        gameObject.SetActive(true);
    }

    private void okClick()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        gameObject.SetActive(false);

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
            Scene m_Scene = SceneManager.GetActiveScene();
            if (m_Scene.name != "MainPlayerScene")
            {
                SceneManager.LoadScene("MainPlayerScene");
            }

        }
        PanelHolder.panelQueue.Dequeue();
        PanelHolder.instance.CheckPanelQueue();
    }
    private void combatClick()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        gameObject.SetActive(false);
        SceneManager.LoadScene("CombatScene");
    }
}