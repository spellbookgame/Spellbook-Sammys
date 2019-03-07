using System.Collections.Generic;
using UnityEngine;

// spell for Alchemy class
public class PotionofBlessing : Spell
{
    public PotionofBlessing()
    {
        iTier = 1;
        iManaCost = 1500;
        iCoolDown = 3;

        sSpellName = "Potion of Blessing";
        sSpellClass = "Alchemist";
        sSpellInfo = "Heal all allies by half of their max health.";

        requiredGlyphs.Add("Alchemy A Glyph", 1);
        requiredGlyphs.Add("Illusion A Glyph", 1);
        requiredGlyphs.Add("Summoning B Glyph", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // subtract mana and glyph costs
        player.iMana -= iManaCost;

        PanelHolder.instance.displayNotify("You cast " + sSpellName, "You healed all allies by half their max health.");
    }
}
