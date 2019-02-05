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

        requiredPieces.Add("Arcane Spell Piece", 1);
        requiredPieces.Add("Elemental Spell Piece", 1);
        requiredPieces.Add("Summoning Spell Piece", 1);
        requiredPieces.Add("Time Spell Piece", 1);
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
