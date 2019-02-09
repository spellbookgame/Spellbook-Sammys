using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// example spell for Arcanist class
public class ArcanaHarvest : Spell
{
    public ArcanaHarvest()
    {
        sSpellName = "Arcana Harvest";
        iTier = 3;
        iManaCost = 100;
        sSpellClass = "Arcanist";

        requiredPieces.Add("Arcane Spell Piece", 1);
        requiredPieces.Add("Summoning Spell Piece", 1);
        requiredPieces.Add("Elemental Spell Piece", 1);
        requiredPieces.Add("Illusion Spell Piece", 1);

        requiredGlyphs.Add("Arcane1", 1);
        requiredGlyphs.Add("Summoning1", 1);
        requiredGlyphs.Add("Elemental1", 1);
        requiredGlyphs.Add("Illusion1", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // if player has enough mana and glyphs, cast the spell
        if (player.glyphs["Arcane1"] > 0 && player.glyphs["Summoning1"] > 0 && player.glyphs["Elemental1"] > 0 && player.glyphs["Illusion1"] > 0
            && player.iMana >= iManaCost)
        {
            Debug.Log(sSpellName + " was cast!");

            // subtract mana and glyphs
            player.iMana -= iManaCost;
            player.glyphs["Arcane1"] -= 1;
            player.glyphs["Summoning1"] -= 1;
            player.glyphs["Elemental1"] -= 1;
            player.glyphs["Illusion1"] -= 1;
        }
    }
}
