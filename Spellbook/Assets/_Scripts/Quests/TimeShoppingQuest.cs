using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TimeShoppingQuest : Quest
{
    public TimeShoppingQuest(int turnStarted)
    {
        questName = "Shopping";
        questType = "Specific Location";
        questFlavor = "Shopping is fun! The Capital has a great Marketplace you should check out!";
        questTask = "Visit the Capital.";
        questHint = "The Alchemist has a spell that can teleport you to the Capital!";

        startTurn = turnStarted;
        expiration = 20;

        spaceName = "location_capital";

        rewards.Add("Item", "Aromatic Tea Leaves");
        rewards.Add("Class Rune", "B Rune");

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
