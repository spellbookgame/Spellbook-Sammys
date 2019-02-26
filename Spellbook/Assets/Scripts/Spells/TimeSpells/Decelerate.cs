using System.Collections.Generic;
using UnityEngine;

// spell for Chronomancy class
public class Decelerate : Spell
{
    public Decelerate()
    {
        iTier = 3;
        iManaCost = 100;
        iCoolDown = 0;

        sSpellName = "Decelerate";
        sSpellClass = "Chronomancer";
        sSpellInfo = "Your next move dice will roll a one, two, or a three. Can cast on an ally.";

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

            PanelHolder.instance.displayNotify("You cast Decelerate. Your next move dice will roll a 1, 2, or 3.");
            player.activeSpells.Add(this);
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
