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
        iManaCost = 100;
        sSpellClass = "Elementalist";

        requiredPieces.Add("Elemental Spell Piece");
        requiredPieces.Add("Elemental Spell Piece");
        requiredPieces.Add("Elemental Spell Piece");
        requiredPieces.Add("Elemental Spell Piece");
    }

    public override void SpellCast(SpellCaster player)
    {
        Debug.Log(sSpellName + " was cast!");
        player.numMana -= iManaCost;
    }
}
