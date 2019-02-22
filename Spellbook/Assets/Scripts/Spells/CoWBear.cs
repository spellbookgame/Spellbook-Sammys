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

        requiredGlyphs.Add("Summoning B Glyph", 1);
        requiredGlyphs.Add("Summoning C Glyph", 1);
        requiredGlyphs.Add("Time D Glyph", 1);
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
        if (canCast)
        {
            // subtract mana and glyph costs
            player.iMana -= iManaCost;
            foreach (KeyValuePair<string, int> kvp in requiredGlyphs)
                player.glyphs[kvp.Key] -= 1;

            int damage = Random.Range(3, 18);
            enemy.HitEnemy(damage);
            PanelHolder.instance.displayCombat("You summoned a great bear and inflicted " + damage + " damage!");
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
