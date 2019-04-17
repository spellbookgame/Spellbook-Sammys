using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// example spell for Arcanist class
public class ArcaneConversion : Spell
{
    public ArcaneConversion()
    {
        iTier = 3;
        iManaCost = 100;
        iCoolDown = 0;

        sSpellName = "Arcane Conversion";
        sSpellClass = "Arcanist";
        sSpellInfo = "Destroy any number of items, and deal 2 damage for each item destroyed";

        requiredGlyphs.Add("Arcane C Glyph", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        Enemy enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();

        // subtract mana and glyph costs
        player.iMana -= iManaCost;

        int damage = Random.Range(3, 12);
        enemy.HitEnemy(damage);
        PanelHolder.instance.displayNotify("You cast " + sSpellName, "It did " + damage + " damage!", "OK");
    }
}
