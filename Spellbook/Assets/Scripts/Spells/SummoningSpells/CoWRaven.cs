using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// spell for Summoner class
public class CoWRaven : Spell
{
    public CoWRaven()
    {
        iTier = 2;
        iManaCost = 800;
        iCoolDown = 2;

        sSpellName = "Call of the Wild - Sign of the Raven";
        sSpellClass = "Summoner";
        sSpellInfo = "Summon a raven that will bring you a random item.";

        requiredGlyphs.Add("Summoning B Glyph", 1);
        requiredGlyphs.Add("Summoning C Glyph", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
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
            
            PanelHolder.instance.displayCombat("You cast " + sSpellName + "!");
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
