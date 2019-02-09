using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        requiredGlyphs.Add("Arcane1", 4);
    }

    public override void SpellCast(SpellCaster player)
    {
        HealthManager healthManager = GameObject.Find("ScriptContainer").GetComponent<HealthManager>();
        Enemy enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();

        // if player has enough mana and glyphs, cast the spell
        if (player.glyphs["Arcane1"] > 0 && player.iMana >= iManaCost)
        {
            int damage = Random.Range(3, 12);
            enemy.HitEnemy(damage);

            Debug.Log(sSpellName + " was cast!");

            // subtract mana and glyphs
            player.iMana -= iManaCost;
            player.glyphs["Arcane1"] -= 1;

            if (enemy.fCurrentHealth > 0)
            {
                SceneManager.LoadScene("CombatScene");
            }
        }
    }
}
