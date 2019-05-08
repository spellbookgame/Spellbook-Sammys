using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AlchemyManaQuest : Quest
{
    public AlchemyManaQuest(int turnStarted)
    {
        questName = "Alchemy Mana Quest";
        questType = "Collect Mana";
        questFlavor = "Mana is an important source of energy for us Spellcasters, and you need to study it very well.";
        questTask = "Collect 1500 mana crystals.";
        questHint = "I heard the Opal Ammonite gives a ton of mana...";

        startTurn = turnStarted;
        expiration = 20;

        rewards.Add("Rune", "Alchemist A Rune");
        rewards.Add("Item", "Crystal Mirror");

        questCompleted = false;

        manaTracker = 0;
        manaRequired = 1500;
    }

    // return a string that contains the rewards of the quest
    public override string DisplayReward()
    {
        StringBuilder sb = new StringBuilder();

        foreach(KeyValuePair<string, string> kvp in rewards)
        {
            sb.Append(kvp.Value);
            sb.Append("\n");
        }

        return sb.ToString();
    }
}
