using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ArcaneErrandQuest : Quest
{
    public ArcaneErrandQuest(int turnStarted)
    {
        questName = "Arcane Errand Quest";
        questType = "Errand";
        questFlavor = "The Illusion Town is in need of an Abyssal Ore. Find one and bring it to them.";
        questTask = "Bring an Abyssal Ore to the Illusion Town.";

        startTurn = turnStarted;
        expiration = 20;

        spaceName = "town_illusionist";
        itemName = "Abyssal Ore";

        rewards.Add("Rune", "Illusionist A Rune");
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
