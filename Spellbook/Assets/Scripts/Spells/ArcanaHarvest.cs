using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// example spell for Arcanist class
public class ArcanaHarvest : Spell
{
    public ArcanaHarvest()
    {
        sSpellName = "Arcana Harvest";
        iTier = 3;
        iManaCost = 100;
        sSpellClass = "Arcanist";

        requiredGlyphs.Add("Arcane D Glyph", 1);
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
        if (canCast)
        {
            // subtract mana and glyph costs
            player.iMana -= iManaCost;
            foreach (KeyValuePair<string, int> kvp in requiredGlyphs)
                player.glyphs[kvp.Key] -= 1;
            
            PanelHolder.instance.displayNotify("You cast Arcana Harvest. You will receive double mana/glyphs on the next space you land on.");
            player.activeSpells.Add(sSpellName);
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
