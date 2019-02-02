﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// example spell for Summoner class
public class SummonTree : Spell
{
    public SummonTree()
    {
        sSpellName = "Summon Tree";
        iTier = 3;
        iManaCost = 100;
        sSpellClass = "Summoner";

        requiredPieces.Add("Summoning Spell Piece");
        requiredPieces.Add("Summoning Spell Piece");
        requiredPieces.Add("Summoning Spell Piece");
        requiredPieces.Add("Summoning Spell Piece");
    }

    public override void SpellCast(SpellCaster player)
    {
        Debug.Log(sSpellName + " was cast!");
        player.numMana -= iManaCost;
    }
}