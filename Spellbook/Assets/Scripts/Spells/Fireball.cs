using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// example spell for Elementalist class
public class Fireball : Spell
{
    public Fireball()
    {
        sSpellName = "Fireball";
        iTier = 3;
        iManaCost = 100;
        sSpellClass = "Elementalist";

        requiredPieces.Add("Arcane Spell Piece");
        requiredPieces.Add("Elemental Spell Piece");
        requiredPieces.Add("Summoning Spell Piece");
        requiredPieces.Add("Time Spell Piece");

        requiredPiecesList.Add("Arcane Spell Piece");
        requiredPiecesList.Add("Elemental Spell Piece");
        requiredPiecesList.Add("Summoning Spell Piece");
        requiredPiecesList.Add("Time Spell Piece");
    }

    public override void SpellCast(SpellCaster player)
    {
        HealthManager healthManager = GameObject.Find("ScriptContainer").GetComponent<HealthManager>();
        int damage = Random.Range(2, 12);
        healthManager.HitEnemy(damage);

        Debug.Log(sSpellName + " was cast!");
        player.iMana -= iManaCost;
    }
}
