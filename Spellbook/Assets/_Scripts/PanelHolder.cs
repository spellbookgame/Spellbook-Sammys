using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelHolder : MonoBehaviour
{
    public YourTurnUI yourTurnPanel;
    public NotifyUI notifyPanel;
    public QuestUI questPanel;
    public QuestRewardUI questRewardPanel;
    public BoardScanUI boardScanPanel;
    public CrisisUI crisisPanel;
     
    
    public static PanelHolder instance = null;

    // to determine panel display order
    public static Queue<string> panelQueue;

    private void Awake()
    {
        //Check if there is already an instance of PanelHolder
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of PanelHolder.
            Destroy(gameObject);

        panelQueue = new Queue<string>();

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(GameObject.FindGameObjectWithTag("LocalPlayer"))
        {
            // repeatedly checks panelqueue in case a new event comes up
            if (GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>().Spellcaster.procPanelShown)
                if (panelQueue.Count > 0)
                    CheckPanelQueue();
        }
    }

    // enables panel if it's next in queue
    public void CheckPanelQueue()
    {
        if(panelQueue.Count > 0)
        {
            Debug.Log("next in queue is: " + panelQueue.Peek());
            if (panelQueue.Peek().Equals(notifyPanel.panelID))
                notifyPanel.EnablePanel();
            else if (panelQueue.Peek().Equals(questPanel.panelID))
                questPanel.EnablePanel();
            else if (panelQueue.Peek().Equals(questRewardPanel.panelID))
                questRewardPanel.EnablePanel();
            else if (panelQueue.Peek().Equals(yourTurnPanel.panelID))
                yourTurnPanel.EnablePanel();
            else if (panelQueue.Peek().Equals(boardScanPanel.panelID))
                boardScanPanel.EnablePanel();
            else if (panelQueue.Peek().Equals(crisisPanel.panelID))
                crisisPanel.EnablePanel();
        }
    }

    public void displayYourTurn()
    {
        panelQueue.Enqueue(yourTurnPanel.panelID);
        Debug.Log("Queued: " + yourTurnPanel.panelID);
        yourTurnPanel.Display();
    }

    public void displayNotify(string title, string info, string buttonClick)
    {
        // close dice tray if it's open (find a better solution soon; Panels are not showing up above dice tray)
        if (SceneManager.GetActiveScene().name.Equals("MainPlayerScene") && GameObject.Find("Dice Tray"))
        {
            DiceUIHandler diceUIHandler = GameObject.Find("Dice Tray").GetComponent<DiceUIHandler>();
            if (diceUIHandler.diceTrayOpen)
            {
                diceUIHandler.OpenCloseDiceTray();
            }
        }
        panelQueue.Enqueue(notifyPanel.panelID);
        Debug.Log("Queued: " + notifyPanel.panelID);
        notifyPanel.DisplayNotify(title, info, buttonClick);
    }

    public void displayQuest(Quest quest)
    {
        panelQueue.Enqueue(questPanel.panelID);
        Debug.Log("Queued: " + questPanel.panelID);
        questPanel.DisplayQuest(quest);
    }

    public void displayQuestRewards(Quest quest)
    {
        // close dice tray if it's open
        if (SceneManager.GetActiveScene().name.Equals("MainPlayerScene") && GameObject.Find("Dice Tray"))
        {
            DiceUIHandler diceUIHandler = GameObject.Find("Dice Tray").GetComponent<DiceUIHandler>();
            if (diceUIHandler.diceTrayOpen)
            {
                diceUIHandler.OpenCloseDiceTray();
            }
        }
        panelQueue.Enqueue(questRewardPanel.panelID);
        Debug.Log("Queued: " + questRewardPanel.panelID);
        questRewardPanel.DisplayQuestRewards(quest);
    }

    public void displayCrisis(string info, int rounds)
    {
        panelQueue.Enqueue(crisisPanel.panelID);
        Debug.Log("Queued: " + crisisPanel.panelID);
        crisisPanel.DisplayCrisis(info, rounds);
    }

    // delete after scenes for each board scan is created
    public void displayBoardScan(string title, string info, Sprite sprite)
    {
        panelQueue.Enqueue(boardScanPanel.panelID);
        Debug.Log("Queued: " + boardScanPanel.panelID);
        boardScanPanel.DisplayScanEvent(title, info, sprite);
    }
}
