using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// example spell for Trickster class
public class MirrorImage : Spell
{
    public MirrorImage()
    {
        sSpellName = "Mirror Image";
        iTier = 3;
        iCost = 100;
        sSpellClass = "Trickster";

        requiredPieces.Add("Illusion Spell Piece");
        requiredPieces.Add("Illusion Spell Piece");
        requiredPieces.Add("Illusion Spell Piece");
        requiredPieces.Add("Illusion Spell Piece");
    }

    public override void SpellCast()
    {
        throw new System.NotImplementedException();
    }
}
