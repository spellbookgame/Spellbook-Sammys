using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// example spell for Alchemist class
public class Polymorph : Spell
{
    public Polymorph()
    {
        sSpellName = "Polymorph";
        iTier = 3;
        iManaCost = 100;
        sSpellClass = "Alchemist";

        requiredPieces.Add("Alchemy Spell Piece");
        requiredPieces.Add("Alchemy Spell Piece");
        requiredPieces.Add("Alchemy Spell Piece");
        requiredPieces.Add("Alchemy Spell Piece");
    }

    public override void SpellCast(SpellCaster player)
    {
        Debug.Log(sSpellName + " was cast!");
        player.numMana -= iManaCost;
    }
}
