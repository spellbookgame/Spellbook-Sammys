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
        questFlavor = "Every spellcaster needs some money to travel. Collect some mana for me.";
        questTask = "Collect 1200 mana.";
        questHint = "The Crystal Mines give lots of mana.";

        startTurn = turnStarted;
        expiration = 20;

        manaTracker = 0;
        manaRequired = 1200;

        rewards.Add("Class Rune", "A Rune");
        rewards.Add("Item", "Mystic Translocator");

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