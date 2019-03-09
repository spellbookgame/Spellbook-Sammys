using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyManaQuest : Quest
{
    public AlchemyManaQuest()
    {
        questName = "Alchemy Mana Quest";
        questType = "Collect Mana";
        questReward = "Alchemy A and B Glyph";
        questDescription = "Collect 1200 mana crystals and bring them back to me.";

        questCompleted = false;

        manaTracker = 0;
        manaRequired = 1200;
    }
}
