using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SummoningManaQuest : Quest
{
    public SummoningManaQuest(int turnStarted)
    {
        questName = "Summoning Mana Quest";
        questType = "Collect Mana";
        questDescription = "Collect 2000 mana crystals.";

        startTurn = turnStarted;
        turnLimit = 6;

        List<string> rewardList = new List<string>();
        rewardList.Add("Summoning A Glyph");
        rewardList.Add("Summoning B Glyph");

        rewards.Add("Glyph", rewardList);

        questCompleted = false;

        manaTracker = 0;
        manaRequired = 2000;
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
