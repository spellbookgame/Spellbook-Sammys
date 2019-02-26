using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// spell for Elementalist class
public class Fireball : Spell
{
    public Fireball()
    {
        iTier = 3;
        iManaCost = 100;
        iCoolDown = 0;

        sSpellName = "Fireball";
        sSpellClass = "Elementalist";
        sSpellInfo = "Cast 2 fireballs that deal 1-6 damage each.";

        requiredGlyphs.Add("Elemental D Glyph", 1);
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

            int damage = Random.Range(2, 12);
            enemy.HitEnemy(damage);
            PanelHolder.instance.displayCombat("You cast Fireball and it did " + damage + " damage!");
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
