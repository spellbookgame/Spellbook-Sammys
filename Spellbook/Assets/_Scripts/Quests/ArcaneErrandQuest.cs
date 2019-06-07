using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ArcaneErrandQuest : Quest
{
    public ArcaneErrandQuest(int turnStarted)
    {
        questName = "Ingredient Tracker";
        questType = "Errand";
        questFlavor = "Parados is in need of an Abyssal Ore. Find one and bring it to them.";
        questTask = "Bring an Abyssal Ore to Parados.";
        questHint = "If the Capital and Forest don't have it, maybe a friend does.";

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
