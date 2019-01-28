using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// example spell for Elementalist class
public class IceBarrier : Spell
{
    public IceBarrier()
    {
        sSpellName = "Ice Barrier";
        iTier = 3;
        iCost = 100;
        sSpellClass = "Elementalist";

        requiredPieces.Add("Elemental Spell Piece");
        requiredPieces.Add("Elemental Spell Piece");
        requiredPieces.Add("Elemental Spell Piece");
        requiredPieces.Add("Elemental Spell Piece");
    }

    public override void SpellCast()
    {
        throw new System.NotImplementedException();
    }
}
