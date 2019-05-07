using System.Collections.Generic;
using UnityEngine;

// spell for Alchemy class
public class CollectorsDrink : Spell
{
    public CollectorsDrink()
    {
        iTier = 2;
        iManaCost = 1200;

        combatSpell = false;

        sSpellName = "Brew - Collector's Drink";
        sSpellClass = "Alchemist";
        sSpellInfo = "The next time you receive an item, receive an additional copy of it.";

        requiredRunes.Add("Alchemist C Rune", 1);
        requiredRunes.Add("Illusionist B Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            PanelHolder.instance.displayNotify("You cast " + sSpellName, "Next time you receive an item, you will gain another copy of it.", "MainPlayerScene");
        }
        else if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("Not enough Mana!", "You do not have enough mana to cast this spell.", "OK");
        }
        else
        {
            // subtract mana and glyph costs
            player.iMana -= iManaCost;

            PanelHolder.instance.displayNotify("You cast " + sSpellName, "Next time you receive an item, you will gain another copy of it.", "MainPlayerScene");
            player.activeSpells.Add(this);
        }
    }
}
