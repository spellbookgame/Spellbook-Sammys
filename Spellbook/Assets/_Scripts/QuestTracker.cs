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
        PanelHolder.instance.displayNotify(q.questName + " Failed...", "You failed to complete the quest in time. You paid " + q.consequenceMana + " for the trouble.", "OK");
        localPlayer.Spellcaster.LoseMana(q.consequenceMana);
    }

    IEnumerator QuestCompleted(Quest q)
    {
        SoundManager.instance.PlaySingle(SoundManager.questsuccess);
        PanelHolder.instance.displayNotify(q.questName + " Completed!",
                                            "You completed the quest! You earned:\n\n" + q.DisplayReward(), "OK");
        localPlayer.Spellcaster.activeQuests.Remove(q);

        yield return new WaitForSeconds(1f);
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
                    StartCoroutine("QuestCompleted", q);
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
                    StartCoroutine("QuestCompleted", q);
                }
            }
        }
    }

    // IllusionSpaceQuest - Checked in CustomEventHandler.cs in ScanItem()
    public void CheckSpaceQuest(string spaceName)
    {
        foreach (Quest q in localPlayer.Spellcaster.activeQuests.ToArray())
        {
            if (q.questType.Equals("Specific Space"))
            {
                if (spaceName.Equals(q.spaceName))
                {
                    ++q.spacesLanded;
                }

                if (q.spacesLanded >= q.spacesRequired)
                {
                    StartCoroutine("QuestCompleted", q);
                }
            }
        }
    }

    // ElementalErrandQuest -  checked in CustomEventHandler.cs in ScanItem()
    public void CheckErrandQuest(string spaceName)
    {
        foreach (Quest q in localPlayer.Spellcaster.activeQuests.ToArray())
        {
            if (q.questType.Equals("Errand"))
            {
                if (spaceName.Equals(q.spaceName))
                {
                    // check if player has the item for the errand
                    if(localPlayer.Spellcaster.inventory.Contains(q.item))
                    {
                        q.questCompleted = true;
                    }
                }
                if (q.questCompleted)
                {
                    SoundManager.instance.PlaySingle(SoundManager.questsuccess);
                    PanelHolder.instance.displayNotify(q.questName + " Completed!",
                                                        "You completed the quest! You earned:\n\n" + q.DisplayReward(), "OK");
                    localPlayer.Spellcaster.activeQuests.Remove(q);
                    // localPlayer.Spellcaster.inventory.Remove(q.item);
                    GiveRewards(q);
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
            case "Rune":
                PanelHolder.instance.displayNotify("Rune Reward", "Take a " + value + " from the deck.", "OK");
                return value;
            case "Class Rune":
                PanelHolder.instance.displayNotify("Class Rune", "Take a " + localPlayer.Spellcaster.classType + " " + value + " from the deck.", "OK");
                return value;
            case "Mana":
                PanelHolder.instance.displayNotify("Mana Reward", "You earned " + value + " mana!", "OK");
                localPlayer.Spellcaster.CollectMana(Int32.Parse(value));
                return value;
            case "Item":
                PanelHolder.instance.displayNotify("Item Reward", "You received " + value + "!", "OK");
                return value;
            default:
                return value;
        }
    }
}
