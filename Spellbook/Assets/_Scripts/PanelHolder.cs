using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelHolder : MonoBehaviour
{
    public YourTurnUI yourTurnPanel;
    public NotifyUI notifyPanel;
    public QuestUI questPanel;
    
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
        // repeatedly checks panelqueue in case a new event comes up
        if(GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>().Spellcaster.procPanelShown)
            if (panelQueue.Count > 0)
                CheckPanelQueue();
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
            else if (panelQueue.Peek().Equals(yourTurnPanel.panelID))
                yourTurnPanel.EnablePanel();
        }
    }

    public void displayYourTurn()
    {
        panelQueue.Enqueue(yourTurnPanel.panelID);
        Debug.Log("Queued: " + yourTurnPanel.panelID);
        yourTurnPanel.Display();
    }

    public void displayEvent(string title, string info)
    {
        panelQueue.Enqueue(notifyPanel.panelID);
        Debug.Log("Queued: " + notifyPanel.panelID);
        notifyPanel.DisplayEvent(title, info);
    }

    public void displayNotify(string title, string info)
    {
        panelQueue.Enqueue(notifyPanel.panelID);
        Debug.Log("Queued: " + notifyPanel.panelID);
        notifyPanel.DisplayNotify(title, info);
    }

    public void displayCombat(string title, string info)
    {
        panelQueue.Enqueue(notifyPanel.panelID);
        Debug.Log("Queued: " + notifyPanel.panelID);
        notifyPanel.DisplayCombat(title, info);
    }

    public void displayQuest(Quest quest)
    {
        panelQueue.Enqueue(questPanel.panelID);
        Debug.Log("Queued: " + questPanel.panelID);
        questPanel.DisplayQuestGlyphs(quest);
    }
}
