using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class IllusionErrandQuest : Quest
{
    public IllusionErrandQuest(int turnStarted)
    {
        questName = "Complete Illusion";
        questType = "Errand";
        questFlavor = "I've been trying to perfect this illusion... Can you bring me a Crystal Mirror?";
        questTask = "Bring a Crystal Mirror to Parados.";
        questHint = "The Capital sells cool items! Or maybe one of your friends can give one to you.";

        startTurn = turnStarted;
        expiration = 20;

        spaceName = "town_illusionist";
        itemName = "Crystal Mirror";

        rewards.Add("Rune", "Illusionist B Rune");
        rewards.Add("Dice", "D9");

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
