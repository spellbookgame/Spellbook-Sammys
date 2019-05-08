using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AlchemyErrandQuest : Quest
{
    public AlchemyErrandQuest(int turnStarted)
    {
        questName = "Alchemy Errand Quest";
        questType = "Errand";
        questFlavor = "I need Aromatic Tea Leaves to finish this last potion.";
        questTask = "Bring 1 Aromatic Tea Leaves to the Alchemy Town.";

        startTurn = turnStarted;
        expiration = 20;

        spaceName = "town_alchemist";
        itemName = "Aromatic Tea Leaves";

        rewards.Add("Rune", "Alchemist A Rune");
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
