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

            PanelHolder.instance.displayNotify("You cast " + sSpellName, "You healed all allies by half their max health.");
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
