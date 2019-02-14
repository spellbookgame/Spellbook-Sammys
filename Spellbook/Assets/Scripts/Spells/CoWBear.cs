using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// spell for Summoner class
public class CoWBear : Spell
{
    public CoWBear()
    {
        sSpellName = "Call of the Wild - Sign of the Bear";
        iTier = 2;
        iManaCost = 400;
        sSpellClass = "Summoner";

        requiredPieces.Add("Summoning B Spell Piece", 1);
        requiredPieces.Add("Summoning C Spell Piece", 1);
        requiredPieces.Add("Time D Spell Piece", 1);

        requiredGlyphs.Add("Summoning B Glyph", 4);
    }

    public override void SpellCast(SpellCaster player)
    {
        Enemy enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();

        // if player has enough mana and glyphs, cast the spell
        if (player.glyphs["Summoning B Glyph"] >= 4 && player.iMana >= iManaCost)
        {
            int damage = Random.Range(3, 18);
            enemy.HitEnemy(damage);

            // subtract mana and glyphs
            player.iMana -= iManaCost;
            player.glyphs["Summoning B Glyph"] -= 4;

            if (enemy.fCurrentHealth > 0)
            {
                SceneManager.LoadScene("CombatScene");
            }
        }
    }
}
