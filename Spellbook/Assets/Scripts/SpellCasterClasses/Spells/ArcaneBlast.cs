using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// example spell for Arcanist class
public class ArcaneBlast : Spell
{
    public ArcaneBlast()
    {
        sSpellName = "Arcane Blast";
        iTier = 1;
        spellClass = "Arcanist";

        requiredPieces.Add("Arcane Spell Piece");
        requiredPieces.Add("Arcane Spell Piece");
        requiredPieces.Add("Arcane Spell Piece");
        requiredPieces.Add("Arcane Spell Piece");
    }

    public override void collectSpell()
    {
        Debug.Log("spell is collected!");
    }
}
