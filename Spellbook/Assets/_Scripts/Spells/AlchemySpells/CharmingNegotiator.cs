using System.Collections.Generic;
using UnityEngine;

// spell for Alchemy class
public class CharmingNegotiator : Spell
{
    public CharmingNegotiator()
    {
        iTier = 2;
        iManaCost = 1400;

        combatSpell = false;

        sSpellName = "Brew - Charming Negotiator";
        sSpellClass = "Alchemist";
        sSpellInfo = "The shopkeeper will give a 30% discount to everyone next time they visit.";

        requiredRunes.Add("Alchemist D Rune", 1);
        requiredRunes.Add("Summoner B Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            PanelHolder.instance.displayNotify("You cast " + sSpellName, "Next time you visit the shop, you will receive 30% discount.", "OK");
        }
        else if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("Not enough Mana!", "You do not have enough mana to cast this spell.", "OK");
        }
        else
        {
            // subtract mana and glyph costs
            player.iMana -= iManaCost;

            PanelHolder.instance.displayNotify("You cast " + sSpellName, "Next time you visit the shop, you will receive 30% discount.", "OK");
            player.activeSpells.Add(this);
        }
    }
}
