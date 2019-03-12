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
        // subtract mana and glyph costs
        player.iMana -= iManaCost;

        PanelHolder.instance.displayNotify("You cast " + sSpellName, "");
    }
}
