using System.Collections.Generic;
using UnityEngine;

// example spell for Chronomancy class
public class Accelerate : Spell
{
    public Accelerate()
    {
        sSpellName = "Accelerate";
        iTier = 3;
        iManaCost = 100;
        sSpellClass = "Chronomancer";

        requiredGlyphs.Add("Time C Glyph", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        bool canCast = false;
        // checking if player can actually cast the spell
        foreach (KeyValuePair<string, int> kvp in requiredGlyphs)
        {
            if (player.glyphs[kvp.Key] >= 1)
                canCast = true;
        }
        if (canCast)
        {
            // subtract mana and glyph costs
            player.iMana -= iManaCost;
            foreach (KeyValuePair<string, int> kvp in requiredGlyphs)
                player.glyphs[kvp.Key] -= 1;

            PanelHolder.instance.displayNotify("You cast Accelerate. Your next move dice will roll a five or a six.");
            player.activeSpells.Add(sSpellName);
        }
        else if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("You don't have enough mana to cast this spell.");
        }
        else
        {
            PanelHolder.instance.displayNotify("You don't have enough glyphs to cast this spell.");
        }
    }
}
