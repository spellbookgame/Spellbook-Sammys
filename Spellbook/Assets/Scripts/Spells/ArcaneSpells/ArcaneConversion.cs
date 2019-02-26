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

            int damage = Random.Range(3, 12);
            enemy.HitEnemy(damage);
            PanelHolder.instance.displayCombat("You cast Arcane Conversion, and it did " + damage + " damage!");
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
