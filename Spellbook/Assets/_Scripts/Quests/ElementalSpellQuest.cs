using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ElementalSpellQuest : Quest
{
    public ElementalSpellQuest(int turnStarted)
    {
        questName = "Abilities Test";
        questType = "Spell";
        questFlavor = "I want to see your spell-creation abilities. Collect a TIER 2 spell.";
        questTask = "Collect a TIER 2 spell.";
        questHint = "Tier 2 spells are colored as Purple in your Spell Recipes list!";

        startTurn = turnStarted;
        expiration = 20;

        spellTier = 2;

        rewards.Add("Dice", "D8");
        rewards.Add("Mana", "600");

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
