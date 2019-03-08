using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// singleton used to track player's quests
// called in MainPageHandler.setUpMainPage();
public class QuestTracker : MonoBehaviour
{
    public static QuestTracker instance = null;

    Player localPlayer;

    void Awake()
    {
        //Check if there is already an instance of QuestTracker
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of QuestTracker.
            Destroy(gameObject);

        //Set QuestTracker to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
    }

    public void CheckQuest(int mana)
    {
        foreach(Quest q in localPlayer.Spellcaster.activeQuests)
        {
            if(q.questType.Equals("Collect Mana"))
            {
                q.manaTracker += mana;
                Debug.Log("Quest mana tracker: " + q.manaTracker);
            }
            if (q.manaTracker >= q.manaRequired)
            {
                q.questCompleted = true;
            }
            if(q.questCompleted)
            {
                PanelHolder.instance.displayNotify(q.questName + " Completed!", "You completed the quest! You earned: " + q.questReward);
                // TODO
                // actually give the rewards to player
            }
        }
    }
}
