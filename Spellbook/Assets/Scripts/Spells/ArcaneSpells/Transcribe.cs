using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// spell for Arcanist class
public class Transcribe : Spell
{
    public Transcribe()
    {
        iTier = 1;
        iManaCost = 1500;
        iCoolDown = 3;

        sSpellName = "Transcribe";
        sSpellClass = "Arcanist";
        sSpellInfo = "Upgrade a glyph into its next highest tier. Can cast on an ally.";

        requiredGlyphs.Add("Alchemy B Glyph", 1);
        requiredGlyphs.Add("Arcane A Glyph", 1);
        requiredGlyphs.Add("Illusion B Glyph", 1);
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
