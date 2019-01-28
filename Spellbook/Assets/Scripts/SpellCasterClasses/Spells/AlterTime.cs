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

        requiredPieces.Add("Time Spell Piece");
        requiredPieces.Add("Time Spell Piece");
        requiredPieces.Add("Time Spell Piece");
        requiredPieces.Add("Time Spell Piece");
    }

    public override void SpellCast()
    {
        throw new System.NotImplementedException();
    }
}
