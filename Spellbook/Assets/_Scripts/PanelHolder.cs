using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelHolder : MonoBehaviour
{
    public YourTurnUI yourTurnPanel;
    public NotifyUI notifyPanel;
    public QuestUI questPanel;

    // Start is called before the first frame update
    public static PanelHolder instance = null;

    private void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);
    }

    public void displayYourTurn()
    {
        yourTurnPanel.Display();
    }

    public void displayEvent(string title, string info)
    {
        notifyPanel.DisplayEvent(title, info);
    }

    public void displayNotify(string title, string info)
    {
        notifyPanel.DisplayNotify(title, info);
    }

    public void displayCombat(string title, string info)
    {
        notifyPanel.DisplayCombat(title, info);
    }

    public void displayQuest(Quest quest)
    {
        Debug.Log("PanelHolder called, calling displayQuestGlyphs");
        questPanel.DisplayQuestGlyphs(quest);
    }
}
