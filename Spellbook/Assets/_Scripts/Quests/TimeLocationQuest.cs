using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TimeLocationQuest : Quest
{
    public TimeLocationQuest(int turnStarted)
    {
        questName = "Time Location Quest";
        questType = "Specific Location";
        questFlavor = "I heard the mines are having trouble but I'm busy with my project. Can you visit the Mines for me?";
        questTask = "Visit the Mines.";
        questHint = "Perhaps a friend has a spell to help you get there faster.";

        startTurn = turnStarted;
        expiration = 20;

        spaceName = "location_mines";

        rewards.Add("Item", "Abyssal Ore");
        rewards.Add("Rune", "Chronomancer A Rune");

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
