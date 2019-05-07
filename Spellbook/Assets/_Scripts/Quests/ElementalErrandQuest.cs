using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ElementalErrandQuest : Quest
{
    public ElementalErrandQuest(int turnStarted)
    {
        questName = "Elemental Errand Quest";
        questType = "Errand";
        questFlavor = "The apothecary needs a Glowing Mushroom. Can you bring one to them?";
        questTask = "Bring a Glowing Mushroom to the Alchemy Town.";

        startTurn = turnStarted;
        turnLimit = 4;

        spaceName = "town_alchemist";
        itemName = "Glowing Mushroom";

        rewards.Add("Rune", "Alchemist B Rune");
        rewards.Add("Item", "Mimetic Vellum");

        consequenceMana = 500;

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
