using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelHolder : MonoBehaviour
{
    //Used for casting a spell on ally.
    private IAllyCastable currentSpell;

    public YourTurnUI yourTurnPanel;
    public NotifyUI notifyPanel;
    public QuestUI questPanel;
    public QuestRewardUI questRewardPanel;
    public BoardScanUI boardScanPanel;
    public CrisisUI crisisPanel;
    public PlayerChooseUI chooseSpellcasterPanel; 
    
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

    // keep panelholder as topmost image
    public void SetPanelHolderLast()
    {
        transform.SetAsLastSibling();
    }

    // enables panel if it's next in queue
    public void CheckPanelQueue()
    {
        if (panelQueue.Count > 0)
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
            else if (panelQueue.Peek().Equals(chooseSpellcasterPanel.panelID))
                chooseSpellcasterPanel.EnablePanel();
        }
    }

    public void displayYourTurn()
    {
        panelQueue.Enqueue(yourTurnPanel.panelID);
        Debug.Log("Queued: " + yourTurnPanel.panelID);
        yourTurnPanel.Display();
        CheckPanelQueue();
    }

    public void displayNotify(string title, string info, string buttonClick)
    {
        panelQueue.Enqueue(notifyPanel.panelID);
        Debug.Log("Queued: " + notifyPanel.panelID);
        notifyPanel.DisplayNotify(title, info, buttonClick);
        CheckPanelQueue();
    }

    public void displayQuest(Quest quest)
    {
        panelQueue.Enqueue(questPanel.panelID);
        Debug.Log("Queued: " + questPanel.panelID);
        questPanel.DisplayQuest(quest);
        CheckPanelQueue();
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
        CheckPanelQueue();
    }

    public void displayCrisis(string info, int rounds)
    {
        panelQueue.Enqueue(crisisPanel.panelID);
        Debug.Log("Queued: " + crisisPanel.panelID);
        crisisPanel.DisplayCrisis(info, rounds);
        CheckPanelQueue();
    }

    // delete after scenes for each board scan is created
    public void displayBoardScan(string title, string info, Sprite sprite, string scene)
    {
        panelQueue.Enqueue(boardScanPanel.panelID);
        Debug.Log("Queued: " + boardScanPanel.panelID);
        boardScanPanel.DisplayScanEvent(title, info, sprite, scene);
        CheckPanelQueue();
    }

    //Input: spell reference that allows player to cast spell on another player
    public void displayChooseSpellcaster(IAllyCastable spell)
    {
        currentSpell = spell;
        panelQueue.Enqueue(chooseSpellcasterPanel.panelID);
        Debug.Log("Queued: " + chooseSpellcasterPanel.panelID);
        chooseSpellcasterPanel.DisplayPlayerChoose();
        CheckPanelQueue();
    }

    //Called from PlayerChooseUI when player chooses a spellcaster ally
    //Input: the ally's spellcaster ID
    public void ChooseAlly(int sID, SpellCaster player)
    {
        currentSpell.SpellcastPhase2(sID, player);
    }

}
