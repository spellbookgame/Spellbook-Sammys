using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TimeMoveQuest : Quest
{
    public TimeMoveQuest(int turnStarted)
    {
        questName = "Time Move Quest";
        questType = "Move";
        questFlavor = "Movement is a subset of time, and something that we Chronomancers take seriously. Show me what you can do.";
        questTask = "Travel 20 spaces.";

        startTurn = turnStarted;
        turnLimit = 4;

        spacesTraveled = 0;
        spacesRequired = 20;

        rewards.Add("Rune", "Chronomancer A Rune");
        rewards.Add("Class Rune", "A Rune");

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