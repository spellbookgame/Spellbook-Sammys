using System.Collections.Generic;

public abstract class Quest
{
    public string questName;
    public string questType;
    public string questTask;
    public string questFlavor;
    public string questHint;

    public int startTurn;
    public int expiration;

    public bool questCompleted;

    // <reward type, reward names>
    public Dictionary<string, string> rewards;

    // tracking variables for mana quests
    public int manaTracker;
    public int manaRequired;

    // tracking variables for specific space quests
    public string spaceName;
    public int spacesLanded;
    public int spacesRequired;

    // tracking variables for errand quests (uses spaceName as well)
    public ItemObject item;
    public string itemName;

    // tracking variables for movement quests
    public int spacesTraveled;

    // tracking variables for spell quests
    public int spellTier;

    public Quest()
    {
        rewards = new Dictionary<string, string>();
    }

    public virtual string DisplayReward()
    {
        return "";
    }
}
