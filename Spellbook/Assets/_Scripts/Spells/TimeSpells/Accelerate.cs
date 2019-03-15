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
        iTurnsActive = 2;

        sSpellName = "Accelerate";
        sSpellClass = "Chronomancer";
        sSpellInfo = "Your next move dice will roll between 5-9. Can cast on an ally.";

        requiredGlyphs.Add("Time D Glyph", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // subtract mana and glyph costs
        player.iMana -= iManaCost;

        PanelHolder.instance.displayNotify("You cast " + sSpellName, "Your next move dice will roll a 5 or 6.");
        player.activeSpells.Add(this);
    }
}
