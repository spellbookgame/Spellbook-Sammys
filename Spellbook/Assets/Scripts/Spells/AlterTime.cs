using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// example spell for Chronomancer class
public class AlterTime : Spell
{
    public AlterTime()
    {
        sSpellName = "Alter Time";
        iTier = 3;
        iManaCost = 100;
        sSpellClass = "Chronomancer";

        requiredPieces.Add("Time Spell Piece", 1);
        requiredPieces.Add("Alchemy Spell Piece", 1);
        requiredPieces.Add("Summoning Spell Piece", 1);
        requiredPieces.Add("Illusion Spell Piece", 1);

        requiredGlyphs.Add("Time1", 1);
        requiredGlyphs.Add("Alchemy1", 1);
        requiredGlyphs.Add("Summoning1", 1);
        requiredGlyphs.Add("Illusion1", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // if player has enough mana and glyphs, cast the spell
        if (player.glyphs["Time1"] > 0 && player.glyphs["Alchemy1"] > 0 && player.glyphs["Summoning1"] > 0 && player.glyphs["Illusion1"] > 0
            && player.iMana >= iManaCost)
        {
            Debug.Log(sSpellName + " was cast!");

            // subtract mana and glyphs
            player.iMana -= iManaCost;
            player.glyphs["Alchemy1"] -= 1;
            player.glyphs["Illusion1"] -= 1;
            player.glyphs["Summoning1"] -= 1;
            player.glyphs["Time1"] -= 1;
        }
    }
}
