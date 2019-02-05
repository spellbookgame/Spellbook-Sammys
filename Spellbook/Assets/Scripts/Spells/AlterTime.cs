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
        iManaCost = 100;
        sSpellClass = "Chronomancer";

        requiredPieces.Add("Time Spell Piece", 1);
        requiredPieces.Add("Alchemy Spell Piece", 1);
        requiredPieces.Add("Summoning Spell Piece", 1);
        requiredPieces.Add("Illusion Spell Piece", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        Debug.Log(sSpellName + " was cast!");
        player.iMana -= iManaCost;
    }
}
