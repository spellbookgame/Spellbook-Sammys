using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest
{
    public string questName;
    public string questType;
    public string questDescription;

    public int startTurn;
    public int turnLimit;

    public bool questCompleted;

    // <reward type, reward names (stored in list)>
    public Dictionary<string, List<string>> rewards;

    // tracking variables for mana quests
    public int manaTracker;
    public int manaRequired;

    // tracking variables for specific space quests
    public string spaceName;
    public int spacesLanded;
    public int spacesRequired;

    // tracking variables for errand quests (uses spaceName as well)
    public ItemObject item;

    // tracking variables for movement quests
    public int spacesTraveled;

    // tracking variables for spell quests
    public List<Spell> spellsCast;

    public Quest()
    {
        rewards = new Dictionary<string, List<string>>();
    }

    public virtual string DisplayReward()
    {
        return "";
    }
}
