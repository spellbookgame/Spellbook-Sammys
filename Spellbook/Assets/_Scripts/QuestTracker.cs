using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Grace Ko
/// singleton used to track player's quests
/// instantiated in MainPageHandler.setUpMainPage();
/// </summary>
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

    private void Update()
    {
        if (localPlayer.Spellcaster.activeQuests.Count > 0)
            UpdateActiveQuests();
    }

    public bool HasQuest(Quest q)
    {
        bool hasQuest = false;
        foreach (Quest quest in localPlayer.Spellcaster.activeQuests)
        {
            if (quest.questName.Equals(q.questName))
            {
                hasQuest = true;
            }
            else
            {
                hasQuest = false;
            }
        }
        return hasQuest;
    }

    // updating the list of active quests
    private void UpdateActiveQuests()
    {
        foreach (Quest q in localPlayer.Spellcaster.activeQuests.ToArray())
        {
            // if the player's turns from starting the quest exceeded the turn limit
            if (localPlayer.Spellcaster.NumOfTurnsSoFar - q.startTurn > q.turnLimit)
            {
                QuestFailed(q);
            }
        }
    }

    // notify player of quest failed, remove from their list of active quests, subtract mana
    private void QuestFailed(Quest q)
    {
        SoundManager.instance.PlaySingle(SoundManager.questfailed);
        localPlayer.Spellcaster.activeQuests.Remove(q);
        PanelHolder.instance.displayNotify(q.questName + " Failed...", "You failed to complete the quest in time. You paid " + 
                                            q.consequenceMana + " mana for the trouble.", "OK");
        localPlayer.Spellcaster.LoseMana(q.consequenceMana);
    }

    private void QuestCompleted(Quest q)
    {
        SoundManager.instance.PlaySingle(SoundManager.questsuccess);
        localPlayer.Spellcaster.activeQuests.Remove(q);
        PanelHolder.instance.displayQuestRewards(q);
        GiveRewards(q);
    }

    public void TrackManaQuest(int mana)
    {
        foreach(Quest q in localPlayer.Spellcaster.activeQuests.ToArray())
        {
            if (q.questType.Equals("Collect Mana"))
            {
                q.manaTracker += mana;
                Debug.Log("Quest mana tracker: " + q.manaTracker);

                if (q.manaTracker >= q.manaRequired)
                {
                    QuestCompleted(q);
                }
            }
        }
    }

    public void TrackMoveQuest(int moveSpaces)
    {
        foreach (Quest q in localPlayer.Spellcaster.activeQuests.ToArray())
        {
            if (q.questType.Equals("Move"))
            {
                q.spacesTraveled += moveSpaces;
                if (q.spacesTraveled >= q.spacesRequired)
                {
                    QuestCompleted(q);
                }
            }
        }
    }

    public void TrackErrandQuest(string location)
    {
        foreach (Quest q in localPlayer.Spellcaster.activeQuests.ToArray())
        {
            if (q.questType.Equals("Errand"))
            {
                // if player is at the space and has the item requested
                if (q.spaceName.Equals(location) && localPlayer.Spellcaster.inventory.Contains(q.item))
                {
                    QuestCompleted(q);
                }
            }
        }
    }

    // give player rewards when quest is completed
    public void GiveRewards(Quest q)
    {
        foreach (KeyValuePair<string, string> kvp in q.rewards)
        {
            // calls switch statement in another method b/c we don't want to break loop
            string r = CheckRewards(kvp.Key, kvp.Value);
        }
    }

    // checks w/ switch statement to give rewards (from dictionary's list value)
    private string CheckRewards(string key, string value)
    {
        switch(key)
        {
            case "Mana":
                localPlayer.Spellcaster.CollectMana(Int32.Parse(value));
                return value;
            case "Item":
                ItemObject item = GameObject.Find("ItemList").GetComponent<ItemList>().GetItemFromName(value);
                localPlayer.Spellcaster.AddToInventory(item);
                return value;
            default:
                return value;
        }
    }
}
