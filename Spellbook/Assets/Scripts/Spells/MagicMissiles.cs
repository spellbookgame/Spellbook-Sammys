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

        requiredPieces.Add("Arcane Spell Piece", 1);
        /*requiredPieces.Add("Alchemy Spell Piece", 1);
        requiredPieces.Add("Elemental Spell Piece", 1);
        requiredPieces.Add("Illusion Spell Piece", 1);*/
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
