using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AlchemyManaQuest : Quest
{
    public AlchemyManaQuest()
    {
        questName = "Alchemy Mana Quest";
        questType = "Collect Mana";
        questDescription = "Collect 1200 mana crystals.";

        List<string> rewardList = new List<string>();
        rewardList.Add("Alchemy A Glyph");
        rewardList.Add("Alchemy B Glyph");

        rewards.Add("Glyph", rewardList);

        questCompleted = false;

        manaTracker = 0;
        manaRequired = 1200;
    }

    // return a string that contains the rewards of the quest
    public override string DisplayReward()
    {
        StringBuilder sb = new StringBuilder();

        foreach(KeyValuePair<string, List<string>> kvp in rewards)
        {
            foreach(string s in kvp.Value)
            {
                sb.Append(s);
                sb.Append("\n");
            }
        }

        return sb.ToString();
    }
}
