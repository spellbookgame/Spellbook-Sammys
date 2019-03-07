using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// spell for Arcanist class
public class CombinedKnowledge : Spell
{
    public CombinedKnowledge()
    {
        iTier = 2;
        iManaCost = 800;
        iCoolDown = 2;

        sSpellName = "Combined Knowledge";
        sSpellClass = "Arcanist";
        sSpellInfo = "Grant all allies currently in your city (including yourself) a random glyph from their respective class.";

        requiredGlyphs.Add("Arcane B Glyph", 1);
        requiredGlyphs.Add("Summoning B Glyph", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // subtract mana and glyph costs
        player.iMana -= iManaCost;

        PanelHolder.instance.displayNotify("You cast " + sSpellName, "Give random glyphs");
    }
}
