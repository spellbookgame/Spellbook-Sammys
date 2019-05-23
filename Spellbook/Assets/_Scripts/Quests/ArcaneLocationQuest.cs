using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ArcaneLocationQuest : Quest
{
    public ArcaneLocationQuest(int turnStarted)
    {
        questName = "Forest Arts";
        questType = "Specific Location";
        questFlavor = "The Forest is an enormous source of energy for the Arcane Town. If you want to uncover the secrets of the Arcane arts, visit the Forest.";
        questTask = "Visit the Forest.";
        questHint = "Perhaps a friend has a spell to help you get there faster.";

        startTurn = turnStarted;
        expiration = 20;

        spaceName = "location_forest";

        rewards.Add("Rune", "Arcanist A Rune");
        rewards.Add("Class Rune", "A Rune");

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
