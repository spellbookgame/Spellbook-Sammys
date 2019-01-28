using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// example spell for Summoner class
public class SummonTree : Spell
{
    public SummonTree()
    {
        sSpellName = "Summon Tree";
        iTier = 3;
        sSpellClass = "Summoner";

        requiredPieces.Add("Summoning Spell Piece");
        requiredPieces.Add("Summoning Spell Piece");
        requiredPieces.Add("Summoning Spell Piece");
        requiredPieces.Add("Summoning Spell Piece");
    }
}