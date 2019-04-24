using System.Collections.Generic;
using UnityEngine;

// spell for Alchemy class
public class CrystalScent : Spell
{
    public CrystalScent()
    {
        iTier = 3;
        iManaCost = 600;

        combatSpell = false;

        sSpellName = "Brew - Crystal Scent";
        sSpellClass = "Alchemist";
        sSpellInfo = "Teleport to the Marketplace. Can cast on an ally.";

        requiredRunes.Add("Alchemist D Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {   
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            SpellTracker.instance.RemoveFromActiveSpells("Call of the Moon - Umbra's Eclipse");
            PanelHolder.instance.displayNotify("You cast " + sSpellName, "Move your piece to the Marketplace.", "Shop");
        }
        else if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("Not enough Mana!", "You do not have enough mana to cast this spell.", "OK");
        }
        else
        {
            // subtract mana and glyph costs
            player.iMana -= iManaCost;

            PanelHolder.instance.displayNotify("You cast " + sSpellName, "Move your piece to the Marketplace.", "Shop");
        }
    }
}
