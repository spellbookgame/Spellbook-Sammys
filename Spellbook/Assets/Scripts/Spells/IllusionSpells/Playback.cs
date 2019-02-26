using System.Collections.Generic;
using UnityEngine;

// spell for Trickster class
public class Playback : Spell
{
    public Playback()
    {
        iTier = 2;
        iManaCost = 700;
        iCoolDown = 2;

        sSpellName = "Playback";
        sSpellClass = "Trickster";
        sSpellInfo = "Retrieve two random glyphs that you've spent this game.";
        
        requiredGlyphs.Add("Illusion B Glyph", 1);
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

            PanelHolder.instance.displayNotify(sSpellName + " was cast.");
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
