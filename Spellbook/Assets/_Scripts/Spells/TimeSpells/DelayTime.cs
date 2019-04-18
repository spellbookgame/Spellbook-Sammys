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
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            SpellTracker.instance.RemoveFromActiveSpells("Call of the Moon - Umbra's Eclipse");
            PanelHolder.instance.displayNotify("You cast " + sSpellName, "The next event will come 1 turn later.", "OK");
        }
        else if(player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("Not enough Mana!", "You don't have enough mana to cast this spell.", "OK");
        }
        else
        {
            // subtract mana
            player.iMana -= iManaCost;

            PanelHolder.instance.displayNotify(sSpellName, "The next event will come 1 turn later.", "OK");
        }
    }
}
