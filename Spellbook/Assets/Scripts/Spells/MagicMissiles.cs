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

        requiredPieces.Add("Arcane A Spell Piece", 1);

        requiredGlyphs.Add("Arcane A Glyph", 4);
    }

    public override void SpellCast(SpellCaster player)
    {
        Enemy enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();

        // if player has enough mana and glyphs, cast the spell
        if (player.glyphs["Arcane A Glyph"] >= 4 && player.iMana >= iManaCost)
        {
            int damage = Random.Range(3, 12);
            enemy.HitEnemy(damage);

            // subtract mana and glyphs
            player.iMana -= iManaCost;
            player.glyphs["Arcane A Glyph"] -= 4;

            PanelHolder.instance.displayCombat("You cast Magic Missles, and they did " + damage + " damage!");
        }
        // TODO: button not working
        else if (player.glyphs["Arcane A Glyph"] < 4)
        {
            PanelHolder.instance.displayNotify("You don't have enough glyphs to cast this spell.");
        }
        else if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("You don't have enough mana to cast this spell.");
        }
    }
}
