using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

// used to display all notification panels
public class CrisisUI : MonoBehaviour
{
    [SerializeField] private Text titleText;
    [SerializeField] private Text infoText;
    [SerializeField] private Text roundsText;
    [SerializeField] private Button singleButton;

    public bool panelActive = false;
    public string panelID = "crisis";

    private void DisablePanel()
    {
        gameObject.SetActive(false);
    }
    public void EnablePanel()
    {
        gameObject.SetActive(true);
    }

    // used to notify players of various events. input a buttonClick string to change the onClick listener
    public void DisplayCrisis(string info, int numRounds)
    {
        infoText.text = info;
        roundsText.text = "Will arrive in " + numRounds.ToString() + " rounds.";

        singleButton.onClick.AddListener((OkClick));

        gameObject.SetActive(true);

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

        // for demo build, show tutorial after crisis display
        if (!GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>().Spellcaster.mainTutorialShown)
            UICanvasHandler.instance.ShowTutorialPrompt();

        // for start of game: give players a spell quest to start
        // removed for demo build
        // QuestTracker.instance.GiveSpellQuest();
    }
}