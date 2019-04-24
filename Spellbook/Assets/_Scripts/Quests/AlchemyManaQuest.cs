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
        questFlavor = "Mana is an important source of energy for us Spellcasters, and you need to know it very well.";
        questTask = "Collect 1500 mana crystals.";

        startTurn = turnStarted;
        turnLimit = 4;

        rewards.Add("Rune", "Alchemist A Rune");
        rewards.Add("Item", "item name");

        consequenceMana = 1000;

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
