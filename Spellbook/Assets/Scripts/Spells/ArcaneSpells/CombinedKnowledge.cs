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

            PanelHolder.instance.displayNotify("You cast " + sSpellName + "!");
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
