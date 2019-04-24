using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SummoningManaQuest : Quest
{
    public SummoningManaQuest(int turnStarted)
    {
        questName = "Summoning Mana Quest";
        questType = "Collect Mana";
        questFlavor = "If you can show me that you can collect 1300 mana, I'll give you some runes.";
        questTask = "Collect 1300 mana crystals.";

        startTurn = turnStarted;
        turnLimit = 4;

        manaTracker = 0;
        manaRequired = 1300;

        rewards.Add("Rune", "Summoner A Rune");
        rewards.Add("Class Rune", "B Rune");

        consequenceMana = 700;

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
