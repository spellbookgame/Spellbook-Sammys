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
                PanelHolder.instance.displayNotify(q.questName + " Completed!", "You completed the quest! You earned:\n\n" + q.DisplayReward());
                GiveRewards(q);
            }
        }
    }

    // give player rewards when quest is completed
    public void GiveRewards(Quest q)
    {
        int i = 0;
        foreach (KeyValuePair<string, List<string>> kvp in q.rewards)
        {
            foreach(string s in kvp.Value)
            {
                // calls switch statement in another method b/c we don't want to break loop
                string r = CheckRewards(kvp.Key, kvp.Value[i]);
                ++i;
            }
        }
    }

    // checks w/ switch statement to give rewards (from dictionary's list value)
    private string CheckRewards(string key, string value)
    {
        switch(key)
        {
            case "Glyph":
                localPlayer.Spellcaster.glyphs[value] += 1;
                return value;
            default:
                return value;
        }
    }
}
