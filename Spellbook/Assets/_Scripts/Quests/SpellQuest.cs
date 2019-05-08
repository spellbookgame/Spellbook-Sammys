using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SpellQuest : Quest
{
    public SpellQuest(int turnStarted)
    {
        questName = "Spell Quest";
        questType = "Spell";
        questFlavor = "Let's learn the basics! Create a tier 3 spell.";
        questTask = "Create a tier 3 spell.";
        questHint = "Your Recipes list should tell you how to create one!";

        startTurn = turnStarted;
        expiration = 20;

        spellTier = 3;

        rewards.Add("Class Rune", "B Rune");
        rewards.Add("Item", "Hollow Cabochon");

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
