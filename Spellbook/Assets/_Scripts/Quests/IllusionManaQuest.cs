using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class IllusionManaQuest : Quest
{
    public IllusionManaQuest(int turnStarted)
    {
        questName = "Illusion Retrieval Quest";
        questType = "Collect Mana";
        questFlavor = "Every spellcaster needs some money to travel.";
        questTask = "Collect 1200 mana.";

        startTurn = turnStarted;
        turnLimit = 3;

        manaTracker = 0;
        manaRequired = 1200;

        rewards.Add("Mana", "400");
        rewards.Add("Item", "item name");

        consequenceMana = 600;

        questCompleted = false;
    }

    // return a string that contains the rewards of the quest
    public override string DisplayReward()
    {
        StringBuilder sb = new StringBuilder();

        foreach (KeyValuePair<string, string> kvp in rewards)
        {
            sb.Append(kvp.Value);
            sb.Append("\n");
        }

        return sb.ToString();
    }
}