using System.Collections.Generic;
using UnityEngine;

// spell for Elemental class
public class Tailwind : Spell
{
    public Tailwind()
    {
        iTier = 1;
        iManaCost = 2500;

        sSpellName = "Tailwind";
        sSpellClass = "Elementalist";
        sSpellInfo = "Everyone will receive an extra D6 to their movement next turn.";

        requiredGlyphs.Add("Elemental A Glyph", 1);
        requiredGlyphs.Add("Elemental B Glyph", 1);
        requiredGlyphs.Add("Elemental C Glyph", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("Not enough Mana!", "You do not have enough mana to cast this spell.", "OK");
        }
        else
        {
            SoundManager.instance.PlaySingle(SoundManager.spellcast);

            // subtract mana
            player.iMana -= iManaCost;

            PanelHolder.instance.displayNotify(sSpellName, "Everyone will receive a D6 to their movement next time they roll.", "OK");
            player.activeSpells.Add(this);
        }
    }
}
