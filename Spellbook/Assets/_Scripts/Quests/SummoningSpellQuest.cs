using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SummoningSpellQuest : Quest
{
    public SummoningSpellQuest(int turnStarted)
    {
        questName = "Black Magic";
        questType = "Spell";
        questFlavor = "I've always been curious about Black Magic. Collect a Black Magic Spell.";
        questTask = "Collect a Black Magic Spell.";
        questHint = "The Witch in the Swamp gives these out... for a rare item.";

        startTurn = turnStarted;
        expiration = 20;

        spellTier = 0;

        rewards.Add("Class Rune", "B Rune");
        rewards.Add("Item", "Rift Talisman");

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
