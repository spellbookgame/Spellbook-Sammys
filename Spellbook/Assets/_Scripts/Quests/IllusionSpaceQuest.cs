using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class IllusionSpaceQuest : Quest
{
    public IllusionSpaceQuest()
    {
        questName = "Illusion Glyph Quest";
        questType = "Specific Space";
        questDescription = "Land on 3 Glyph spaces in the Summoner Town.";

        spaceName = "glyph_summoner";
        spacesLanded = 0;
        spacesRequired = 3;

        List<string> rewardList = new List<string>();
        rewardList.Add("Illusion A Glyph");
        rewardList.Add("Illusion B Glyph");

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