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
        sSpellInfo = "Instantly kill the enemy, exit at a random town, and cannot cast spells for the nex 2 turns.";

        requiredGlyphs.Add("Elemental A Glyph", 1);
        requiredGlyphs.Add("Elemental B Glyph", 1);
        requiredGlyphs.Add("Alchemy B Glyph", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        Enemy enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();

        bool canCast = false;
        // checking if player can actually cast the spell
        foreach (KeyValuePair<string, int> kvp in requiredGlyphs)
        {
            if (player.glyphs[kvp.Key] >= 1)
                canCast = true;
        }
        if (canCast && player.iMana > iManaCost)
        {
            // subtract mana and glyph costs
            player.iMana -= iManaCost;
            foreach (KeyValuePair<string, int> kvp in requiredGlyphs)
                player.glyphs[kvp.Key] -= 1;
         
            PanelHolder.instance.displayCombat("You cast " + sSpellName, "You destroyed the enemy!");
            enemy.EnemyDefeated();
        }
        else if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("Not enough mana!", "You don't have enough mana to cast this spell.");
        }
        else
        {
            PanelHolder.instance.displayNotify("Not enough glyphs!", "You don't have enough glyphs to cast this spell.");
        }
    }
}
