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
    }

    public override void SpellCast(SpellCaster player)
    {
        Debug.Log(sSpellName + " was cast!");
        player.iMana -= iManaCost;
    }
}
