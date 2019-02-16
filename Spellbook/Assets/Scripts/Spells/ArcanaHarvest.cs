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

        requiredPieces.Add("Arcane D Spell Piece", 1);

        requiredGlyphs.Add("Arcane D Glyph", 4);
    }

    public override void SpellCast(SpellCaster player)
    {
        // if player has enough mana and glyphs, cast the spell
        if (player.glyphs["Arcane D Glyph"] >= 4 && player.iMana >= iManaCost)
        {
            Debug.Log(sSpellName + " was cast!");

            // subtract mana and glyphs
            player.iMana -= iManaCost;
            player.glyphs["Arcane D Glyph"] -= 4;

            player.activeSpells.Add(sSpellName);
            PanelHolder.instance.displayNotify("You cast Arcana Harvest. You will receive double mana/glyphs on the next space you land on.");
        }
        else if (player.glyphs["Arcane D Glyph"] < 4)
        {
            PanelHolder.instance.displayNotify("You don't have enough glyphs to cast this spell.");
        }
        else if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("You don't have enough mana to cast this spell.");
        }
    }
}
