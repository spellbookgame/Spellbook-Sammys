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
        // subtract mana and glyph costs
        player.iMana -= iManaCost;

        PanelHolder.instance.displayNotify("You cast " + sSpellName, "The next event will come 1 turn later.");
    }
}
