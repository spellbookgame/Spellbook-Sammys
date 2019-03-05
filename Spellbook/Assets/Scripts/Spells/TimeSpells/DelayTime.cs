using System.Collections.Generic;
using UnityEngine;

// spell for Chronomancy class
public class DelayTime : Spell
{
    public DelayTime()
    {
        iTier = 1;
        iManaCost = 3000;
        iCoolDown = 5;

        sSpellName = "Delay Time";
        sSpellClass = "Chronomancer";
        sSpellInfo = "Delay the amount of time before the next global event by one turn.";

        requiredGlyphs.Add("Time A Glyph", 1);
        requiredGlyphs.Add("Time B Glyph", 1);
        requiredGlyphs.Add("Arcane A Glyph", 1);
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

            PanelHolder.instance.displayNotify("You cast " + sSpellName, "The next event will come 1 turn later.");
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
