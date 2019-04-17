using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// example spell for Arcanist class
public class ArcanaHarvest : Spell
{
    public ArcanaHarvest()
    {
        iTier = 3;
        iManaCost = 300;

        sSpellName = "Arcana Harvest";
        sSpellClass = "Arcanist";
        sSpellInfo = "Sacrifice half your mana crystals and move directly to the Mana Crystal Mines. Can cast on an ally.";

        requiredGlyphs.Add("Arcane D Glyph", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // subtract mana and glyph costs
        player.iMana -= iManaCost;
        player.iMana /= 2;
            
        PanelHolder.instance.displayNotify(sSpellName, "Move your piece to the Crystal Mines.", "Vuforia");
    }
}
