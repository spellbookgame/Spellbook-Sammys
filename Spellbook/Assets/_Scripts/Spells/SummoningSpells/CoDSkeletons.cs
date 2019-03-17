using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// spell for Summoner class
public class CoDSkeletons : Spell
{
    public CoDSkeletons()
    {
        iTier = 3;
        iManaCost = 400;
        iCoolDown = 0;

        sSpellName = "Call of the Dead - Skeletons";
        sSpellClass = "Summoner";
        sSpellInfo = "Summon a skeleton that attacks the enemy for 1-6 damage. It will remain for two turns.";

        requiredGlyphs.Add("Summoning D Glyph", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        Enemy enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        
        // subtract mana and glyph costs
        player.iMana -= iManaCost;

        int damage = Random.Range(1, 6);
        enemy.HitEnemy(damage);
        PanelHolder.instance.displayCombat("You cast " + sSpellName, "The Skeleton did " + damage + " damage!");
    }
}
