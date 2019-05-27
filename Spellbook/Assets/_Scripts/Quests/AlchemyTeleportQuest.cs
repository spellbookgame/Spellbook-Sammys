using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AlchemyTeleportQuest : Quest
{
    public AlchemyTeleportQuest(int turnStarted)
    {
        questName = "Teleport Dreams";
        questType = "Errand";
        questFlavor = "I want to teleport, but I don't have the materials! Can you bring me an Infused Sapphire?";
        questTask = "Bring 1 Infused Sapphire to Regulus.";
        questHint = "The Forest and Shrine give out items! Or maybe ask a friend if they have one...";

        startTurn = turnStarted;
        expiration = 20;

        spaceName = "town_alchemist";
        itemName = "Infused Sapphire";

        rewards.Add("Rune", "Alchemist B Rune");
        rewards.Add("Dice", "D7");

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
