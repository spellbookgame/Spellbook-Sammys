using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ArcaneSpellQuest : Quest
{
    public ArcaneSpellQuest(int turnStarted)
    {
        questName = "Arcane Spell Quest";
        questType = "Spell";
        questTask = "Cast the same spell twice in a row.";

        startTurn = turnStarted;
        turnLimit = 3;

        spellsCast = new List<Spell>();

        List<string> rewardList = new List<string>();
        rewardList.Add("Arcane A Glyph");
        rewardList.Add("Arcane B Glyph");

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