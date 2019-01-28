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
        sSpellClass = "Elementalist";

        requiredPieces.Add("Elemental Spell Piece");
        requiredPieces.Add("Elemental Spell Piece");
        requiredPieces.Add("Elemental Spell Piece");
        requiredPieces.Add("Elemental Spell Piece");
    }
}
