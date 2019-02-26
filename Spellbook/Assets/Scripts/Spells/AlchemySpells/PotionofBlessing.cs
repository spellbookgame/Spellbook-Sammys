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
        sSpellInfo = "Give all allies currently in your city double movement on their move rolls next turn.";

        requiredGlyphs.Add("Alchemy A Glyph", 1);
        requiredGlyphs.Add("Summoning B Glyph", 1);
        requiredGlyphs.Add("Time A Glyph", 1);
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
