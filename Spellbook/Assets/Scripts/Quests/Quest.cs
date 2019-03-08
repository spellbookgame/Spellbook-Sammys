using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest
{
    public string questName;
    public string questType;
    public string questDescription;
    public string questReward;

    public bool questCompleted;

    // tracking variables for mana quests
    public int manaTracker;
    public int manaRequired;
}
