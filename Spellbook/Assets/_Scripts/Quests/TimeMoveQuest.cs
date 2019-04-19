using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TimeMoveQuest : Quest
{
    public TimeMoveQuest(int turnStarted)
    {
        questName = "Time Move Quest";
        questType = "Movement";
        questTask = "Travel 12 spaces.";

        startTurn = turnStarted;
        turnLimit = 5;

        spacesTraveled = 0;
        spacesRequired = 12;

        List<string> rewardList = new List<string>();
        rewardList.Add("Time A Glyph");
        rewardList.Add("Time B Glyph");

        rewards.Add("Glyph", rewardList);

        questCompleted = false;
    }

    // return a string that contains the rewards of the quest
    public override string DisplayReward()
    {
        StringBuilder sb = new StringBuilder();

        foreach (KeyValuePair<string, List<string>> kvp in rewards)
        {
            foreach (string s in kvp.Value)
            {
                sb.Append(s);
                sb.Append("\n");
            }
        }

        return sb.ToString();
    }
}