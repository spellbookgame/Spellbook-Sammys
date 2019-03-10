using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// example spell for Arcanist class
public class ArcanaHarvest : Spell
{
    public ArcanaHarvest()
    {
        iTier = 3;
        iManaCost = 400;
        iCoolDown = 0;
        iTurnsActive = 2;

        sSpellName = "Arcana Harvest";
        sSpellClass = "Arcanist";
        sSpellInfo = "Until your next turn, earn double resources (mana, glyphs). Can cast on an ally.";

        requiredGlyphs.Add("Arcane D Glyph", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // subtract mana and glyph costs
        player.iMana -= iManaCost;
            
        PanelHolder.instance.displayNotify("You cast " + sSpellName, "You will receive double mana/glyphs until your next turn.");
        player.activeSpells.Add(this);
    }
}
