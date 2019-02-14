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
        PanelManager panelManager = GameObject.Find("ScriptContainer").GetComponent<PanelManager>();

        // if player has enough mana and glyphs, cast the spell
        if (player.glyphs["Arcane D Glyph"] >= 4 && player.iMana >= iManaCost)
        {
            Debug.Log(sSpellName + " was cast!");

            // subtract mana and glyphs
            player.iMana -= iManaCost;
            player.glyphs["Arcane D Glyph"] -= 4;
        }
        else if (player.glyphs["Arcane D Glyph"] < 4)
        {
            panelManager.ShowPanel();
            panelManager.SetPanelText("You don't have enough glyphs to cast this spell.");
        }
        else if (player.iMana < iManaCost)
        {
            panelManager.ShowPanel();
            panelManager.SetPanelText("You don't have enough mana to cast this spell.");
        }
    }
}
