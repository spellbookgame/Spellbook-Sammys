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
        // subtract mana and glyph costs
        player.iMana -= iManaCost;
            
        PanelHolder.instance.displayNotify("You cast " + sSpellName, "", "OK");
        player.activeSpells.Add(this);
    }
}
