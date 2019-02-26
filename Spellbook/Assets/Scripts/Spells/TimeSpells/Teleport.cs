using System.Collections.Generic;
using UnityEngine;

// spell for Chronomancy class
public class Teleport : Spell
{
    public Teleport()
    {
        iTier = 2;
        iManaCost = 900;
        iCoolDown = 2;

        sSpellName = "Teleport";
        sSpellClass = "Chronomancer";
        sSpellInfo = "Control your next roll to be what you want (between 1-6). Can cast on an ally.";

        requiredGlyphs.Add("Time B Glyph", 1);
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
        if (canCast && player.iMana > iManaCost)
        {
            // subtract mana and glyph costs
            player.iMana -= iManaCost;
            foreach (KeyValuePair<string, int> kvp in requiredGlyphs)
                player.glyphs[kvp.Key] -= 1;

            PanelHolder.instance.displayNotify("You cast " + sSpellName + "!");
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
