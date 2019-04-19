using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AlchemyManaQuest : Quest
{
    public AlchemyManaQuest(int turnStarted)
    {
        questName = "Alchemy Mana Quest";
        questType = "Collect Mana";
        questFlavor = "Mana is an important source of energy for us Spellcasters, and you need to know it very well.";
        questTask = "Collect 1500 mana crystals.";

        startTurn = turnStarted;
        turnLimit = 5;

        List<string> rewardList = new List<string>();
        rewardList.Add("Alchemy A Rune");
        rewardList.Add("Alchemy B Rune");

        rewards.Add("Rune", rewardList);
        consequenceMana = 1000;

        questCompleted = false;

        manaTracker = 0;
        manaRequired = 1500;
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
