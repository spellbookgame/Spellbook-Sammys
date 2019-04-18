using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// spell for Elementalist class
public class NaturalDisaster : Spell
{
    public NaturalDisaster()
    {
        iTier = 1;
        iManaCost = 3000;
        iCoolDown = 3;

        sSpellName = "Natural Disaster";
        sSpellClass = "Elementalist";
        sSpellInfo = "If the enemy has less than half health left, instantly kill it. However, no loot will be earned from this battle.";

        requiredGlyphs.Add("Elemental A Glyph", 1);
        requiredGlyphs.Add("Elemental B Glyph", 1);
        requiredGlyphs.Add("Alchemy B Glyph", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        Enemy enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        
        // subtract mana and glyph costs
        player.iMana -= iManaCost;
            
        // TODO: destroy enemy without giving player loot
        if(enemy.fCurrentHealth < enemy.fMaxHealth / 2)
        {
            PanelHolder.instance.displayNotify(sSpellName, "You destroyed the enemy!", "OK");
            enemy.EnemyDefeated();
        }
    }
}
