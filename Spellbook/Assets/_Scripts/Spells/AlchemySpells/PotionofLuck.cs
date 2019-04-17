using System.Collections.Generic;
using UnityEngine;

// spell for Alchemy class
public class PotionofLuck : Spell
{
    public PotionofLuck()
    {
        iTier = 1;
        iManaCost = 3000;

        sSpellName = "Brew - Potion of Luck";
        sSpellClass = "Alchemist";
        sSpellInfo = "Give you and an ally an extra D8 next time you roll.";

        requiredGlyphs.Add("Alchemy A Glyph", 1);
        requiredGlyphs.Add("Alchemy B Glyph", 1);
        requiredGlyphs.Add("Elemental A Glyph", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // subtract mana and glyph costs
        player.iMana -= iManaCost;

        PanelHolder.instance.displayNotify("You cast " + sSpellName, "You and your ally will receive an extra D8 next time you roll.", "OK");
        player.dice["D8"] += 1;
        player.activeSpells.Add(this);
    }
}
