using System.Collections.Generic;
using UnityEngine;

// spell for Alchemy class
public class CharmingNegotiator : Spell
{
    public CharmingNegotiator()
    {
        iTier = 2;
        iManaCost = 600;
        iCoolDown = 0;

        sSpellName = "Brew - Charming Negotiator";
        sSpellClass = "Alchemist";
        sSpellInfo = "The next time you meet a shop keeper, you'll get 30% discount on all items. Can cast on an ally.";

        requiredGlyphs.Add("Alchemy D Glyph", 1);
        requiredGlyphs.Add("Summoning B Glyph", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // subtract mana and glyph costs
        player.iMana -= iManaCost;

        PanelHolder.instance.displayNotify("You cast " + sSpellName, "The next time you meet a shop keeper, you'll get 30% discount on all items.");
        player.activeSpells.Add(this);
    }
}
