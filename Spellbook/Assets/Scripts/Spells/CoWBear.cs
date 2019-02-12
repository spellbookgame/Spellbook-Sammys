using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// spell for Summoner class
public class CoWBear : Spell
{
    public CoWBear()
    {
        sSpellName = "Call of the Wild - Sign of the Bear";
        iTier = 2;
        iManaCost = 400;
        sSpellClass = "Summoner";

        requiredPieces.Add("Summoning B Spell Piece", 1);
        requiredPieces.Add("Summoning C Spell Piece", 1);
        requiredPieces.Add("Time D Spell Piece", 1);

        requiredGlyphs.Add("Summoning1", 4);
    }

    public override void SpellCast(SpellCaster player)
    {
        // if player has enough mana and glyphs, cast the spell
        if (player.glyphs["Summoning1"] >= 4 && player.iMana >= iManaCost)
        {
            Debug.Log(sSpellName + " was cast!");

            // subtract mana and glyphs
            player.iMana -= iManaCost;
            player.glyphs["Summoning1"] -= 4;
        }
    }
}
