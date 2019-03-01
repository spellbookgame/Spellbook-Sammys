using System.Collections.Generic;
using UnityEngine;

// spell for Chronomancy class
public class Accelerate : Spell
{
    public Accelerate()
    {
        iTier = 3;
        iManaCost = 400;
        iCoolDown = 0;

        sSpellName = "Accelerate";
        sSpellClass = "Chronomancer";
        sSpellInfo = "Your next move dice will roll a 5 or a 6. Can cast on an ally.";

        requiredGlyphs.Add("Time D Glyph", 1);
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
        if (canCast && player.iMana > iManaCost)
        {
            // subtract mana and glyph costs
            player.iMana -= iManaCost;
            foreach (KeyValuePair<string, int> kvp in requiredGlyphs)
                player.glyphs[kvp.Key] -= 1;

            PanelHolder.instance.displayNotify("You cast " + sSpellName, "Your next move dice will roll a 5 or 6.");
            player.activeSpells.Add(this);
        }
        else if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("Not enough mana!", "You don't have enough mana to cast this spell.");
        }
        else
        {
            PanelHolder.instance.displayNotify("Not enough glyphs!", "You don't have enough glyphs to cast this spell.");
        }
    }
}
