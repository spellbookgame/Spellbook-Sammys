using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// example spell for Arcanist class
public class ArcaneBlast : Spell
{
    public ArcaneBlast()
    {
        sSpellName = "Arcane Blast";
        iTier = 3;
        sSpellClass = "Arcanist";

        requiredPieces.Add("Arcane Spell Piece");
        requiredPieces.Add("Arcane Spell Piece");
        requiredPieces.Add("Arcane Spell Piece");
        requiredPieces.Add("Arcane Spell Piece");
    }
}
