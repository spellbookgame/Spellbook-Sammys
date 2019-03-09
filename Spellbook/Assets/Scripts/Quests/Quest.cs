using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest
{
    public string questName;
    public string questType;
    public string questDescription;

    public bool questCompleted;

    // <reward type, reward names (stored in list)>
    public Dictionary<string, List<string>> rewards;

    // tracking variables for mana quests
    public int manaTracker;
    public int manaRequired;

    public Quest()
    {
        rewards = new Dictionary<string, List<string>>();
    }

    public virtual string DisplayReward()
    {
        return "";
    }
}
