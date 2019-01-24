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

        requiredPieces.Add("piece1");
        requiredPieces.Add("piece2");
        requiredPieces.Add("piece3");
        requiredPieces.Add("piece4");
    }

    public override void collectSpell()
    {
        Debug.Log("spell is collected!");
    }
}
