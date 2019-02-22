using System.Collections.Generic;
using UnityEngine;

// example spell for Trickster class
public class Playwright : Spell
{
    public Playwright()
    {
        sSpellName = "Playwright";
        iTier = 2;
        iManaCost = 400;
        sSpellClass = "Trickster";

        requiredGlyphs.Add("Illusion B Glyph", 1);
        requiredGlyphs.Add("Illusion C Glyph", 1);
        requiredGlyphs.Add("Time C Glyph", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        bool canCast = false;
        // checking if player can actually cast the spell
        foreach(KeyValuePair<string, int> kvp in requiredGlyphs)
        {
            if(player.glyphs[kvp.Key] >= 1)
                canCast = true;
        }
        if(canCast)
        {
            // subtract mana and glyph costs
            player.iMana -= iManaCost;
            foreach(KeyValuePair<string, int> kvp in requiredGlyphs)
                player.glyphs[kvp.Key] -= 1;

            PanelHolder.instance.displayNotify(sSpellName + " was cast. You may control your next roll to be a 1, 2, 3, 4, 5, or 6.");

            player.activeSpells.Add(sSpellName);
        }
        else if(player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("You don't have enough mana to cast this spell.");
        }
        else
        {
            PanelHolder.instance.displayNotify("You don't have enough glyphs to cast this spell.");
        }
    }
}
