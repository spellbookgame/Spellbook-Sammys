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

            int damage = Random.Range(1, 6);
            enemy.HitEnemy(damage);
            PanelHolder.instance.displayCombat("You summoned a skeleton and inflicted " + damage + " damage!");
        }
        else if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("You don't have enough mana to cast this spell.");
        }
        else
        {
            PanelHolder.instance.displayNotify("You don't have enough glyphs to cast this spell.");
        }
    }
}
