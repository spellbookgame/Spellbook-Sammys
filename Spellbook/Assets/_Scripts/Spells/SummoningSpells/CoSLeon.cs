using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// spell for Summoner class
public class CoSLeon : Spell
{
    public CoSLeon()
    {
        iTier = 3;
        iManaCost = 1800;
        iCoolDown = 3;

        sSpellName = "Call of the Sun - Leon's Shining";
        sSpellClass = "Summoner";
        sSpellInfo = "You and your allies in the same town as you will be able to cast your next spell for free.";

        requiredGlyphs.Add("Elemental A Glyph", 1);
        requiredGlyphs.Add("Summoning A Glyph", 1);
        requiredGlyphs.Add("Time A Glyph", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // subtract mana and glyph costs
        player.iMana -= iManaCost;
        foreach (KeyValuePair<string, int> kvp in requiredGlyphs)
            player.glyphs[kvp.Key] -= 1;

        PanelHolder.instance.displayCombat("You cast " + sSpellName, "");
        player.activeSpells.Add(this);
    }
}
