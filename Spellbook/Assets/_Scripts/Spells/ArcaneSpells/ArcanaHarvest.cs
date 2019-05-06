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

        combatSpell = false;

        sSpellName = "Arcana Harvest";
        sSpellClass = "Arcanist";
        sSpellInfo = "Sacrifice half your mana crystals and move directly to the Mana Crystal Mines. Can cast on an ally.";

        requiredRunes.Add("Arcanist D Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            PanelHolder.instance.displayNotify(sSpellName, "Move your piece to the Crystal Mines.", "VuforiaScene");
        }
        else if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("Not enough Mana!", "You do not have enough mana to cast this spell.", "OK");
        }
        else
        {
            // subtract mana and glyph costs
            player.iMana -= iManaCost;
            player.iMana /= 2;

            PanelHolder.instance.displayNotify(sSpellName, "Move your piece to the Crystal Mines.", "VuforiaScene");
            UICanvasHandler.instance.ActivateSpellbookButtons(false);
        }
    }
}
