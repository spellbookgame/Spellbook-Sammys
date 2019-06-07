using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class IllusionLocationQuest : Quest
{
    public IllusionLocationQuest(int turnStarted)
    {
        questName = "Swamp Disturbance";
        questType = "Specific Location";
        questFlavor = "There's a disturbance in the Swamp. Go there and find out what's going on.";
        questTask = "Visit the Swamp.";
        questHint = "Utilize those magic dice!";

        startTurn = turnStarted;
        expiration = 20;

        spaceName = "location_swamp";

        rewards.Add("Item", "Infused Sapphire");
        rewards.Add("Dice", "D5");

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
