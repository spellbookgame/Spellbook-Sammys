using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// example spell for Elementalist class
public class Fireball : Spell
{
    public Fireball()
    {
        sSpellName = "Fireball";
        iTier = 3;
        iManaCost = 100;
        sSpellClass = "Elementalist";

        /*requiredPieces.Add("Arcane Spell Piece", 1);
        requiredPieces.Add("Elemental Spell Piece", 1);
        requiredPieces.Add("Summoning Spell Piece", 1);
        requiredPieces.Add("Time Spell Piece", 1);

        requiredGlyphs.Add("Arcane1", 1);
        requiredGlyphs.Add("Elemental1", 1);
        requiredGlyphs.Add("Summoning1", 1);
        requiredGlyphs.Add("Time1", 1);*/
    }

    public override void SpellCast(SpellCaster player)
    {
        /*Enemy enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();

        // if player has enough mana and glyphs, cast the spell
        if (player.glyphs["Arcane1"] > 0 && player.glyphs["Elemental1"] > 0 && player.glyphs["Summoning1"] > 0 && player.glyphs["Time1"] > 0
            && player.iMana >= iManaCost)
        {
            int damage = Random.Range(2, 12);
            enemy.HitEnemy(damage);

            Debug.Log(sSpellName + " was cast!");

            // subtract mana and glyphs
            player.iMana -= iManaCost;
            player.glyphs["Arcane1"] -= 1;
            player.glyphs["Elemental1"] -= 1;
            player.glyphs["Summoning1"] -= 1;
            player.glyphs["Time1"] -= 1;

            if (enemy.fCurrentHealth > 0)
            {
                SceneManager.LoadScene("CombatScene");
            }
        } */
    }
}
