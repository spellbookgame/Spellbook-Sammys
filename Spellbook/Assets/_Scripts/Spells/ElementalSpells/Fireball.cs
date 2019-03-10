using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// spell for Elementalist class
public class Fireball : Spell
{
    public Fireball()
    {
        iTier = 3;
        iManaCost = 300;
        iCoolDown = 0;

        sSpellName = "Fireball";
        sSpellClass = "Elementalist";
        sSpellInfo = "Cast 2 fireballs that deal 1-6 damage each.";

        requiredGlyphs.Add("Elemental D Glyph", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        Enemy enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        
        // subtract mana and glyph costs
        player.iMana -= iManaCost;
            

        int damage = Random.Range(2, 12);
        enemy.HitEnemy(damage);
        PanelHolder.instance.displayCombat("You cast " + sSpellName, "It did " + damage + " damage!");
    }
}
