using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// example spell for Arcanist class
public class MagicMissiles : Spell
{
    public MagicMissiles()
    {
        sSpellName = "Magic Missiles";
        iTier = 3;
        iManaCost = 100;
        sSpellClass = "Arcanist";

        // adding to hashset
        requiredPieces.Add("Arcane Spell Piece");
        requiredPieces.Add("Alchemy Spell Piece");
        requiredPieces.Add("Elemental Spell Piece");
        requiredPieces.Add("Illusion Spell Piece");

        // adding to list
        requiredPiecesList.Add("Arcane Spell Piece");
        requiredPiecesList.Add("Alchemy Spell Piece");
        requiredPiecesList.Add("Elemental Spell Piece");
        requiredPiecesList.Add("Illusion Spell Piece");
    }

    public override void SpellCast(SpellCaster player)
    {
        HealthManager healthManager = GameObject.Find("ScriptContainer").GetComponent<HealthManager>();
        int damage = Random.Range(3, 12);
        healthManager.HitEnemy(damage);

        Debug.Log(sSpellName + " was cast!");
        player.iMana -= iManaCost;
    }
}
