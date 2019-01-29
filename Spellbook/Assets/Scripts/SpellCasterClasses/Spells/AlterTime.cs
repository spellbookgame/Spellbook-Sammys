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
        iCost = 100;
        sSpellClass = "Chronomancer";

        // adding to hashset
        requiredPieces.Add("Time Spell Piece");
        requiredPieces.Add("Alchemy Spell Piece");
        requiredPieces.Add("Summoning Spell Piece");
        requiredPieces.Add("Illusion Spell Piece");

        // adding to list
        requiredPiecesList.Add("Time Spell Piece");
        requiredPiecesList.Add("Alchemy Spell Piece");
        requiredPiecesList.Add("Summoning Spell Piece");
        requiredPiecesList.Add("Illusion Spell Piece");
    }

    public override void SpellCast()
    {
        throw new System.NotImplementedException();
    }
}
