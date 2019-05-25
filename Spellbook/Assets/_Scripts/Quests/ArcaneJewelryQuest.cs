using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ArcaneJewelryQuest : Quest
{
    public ArcaneJewelryQuest(int turnStarted)
    {
        questName = "Jewelery Collector";
        questType = "Errand";
        questFlavor = "I'm collecting jewelry. Can you bring me an Opal Ammonite?";
        questTask = "Bring 1 Opal Ammonite to Zandria.";
        questHint = "Looking for low tier items? The Shrine gives those out the most!";

        startTurn = turnStarted;
        expiration = 20;

        spaceName = "town_arcanist";
        itemName = "Opal Ammonite";

        rewards.Add("Rune", "Arcanist B Rune");
        rewards.Add("Mana", "1000");

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
