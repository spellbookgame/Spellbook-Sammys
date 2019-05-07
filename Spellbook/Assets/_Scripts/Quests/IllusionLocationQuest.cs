using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class IllusionLocationQuest : Quest
{
    public IllusionLocationQuest(int turnStarted)
    {
        questName = "Illusion Location Quest";
        questType = "Specific Location";
        questFlavor = "There's a disturbance in the Swamp. Go there and find out what's going on.";
        questTask = "Visit the Swamp.";

        startTurn = turnStarted;
        turnLimit = 4;

        spaceName = "location_swamp";

        rewards.Add("Item", "Infused Sapphire");
        rewards.Add("Dice", "D5");

        consequenceMana = 900;

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
