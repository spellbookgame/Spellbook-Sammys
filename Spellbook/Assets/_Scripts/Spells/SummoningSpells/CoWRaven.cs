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
        // subtract mana and glyph costs
        player.iMana -= iManaCost;
            
        PanelHolder.instance.displayCombat("You cast " + sSpellName, "");
    }
}
