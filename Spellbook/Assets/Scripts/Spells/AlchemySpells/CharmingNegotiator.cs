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
        bool canCast = false;
        // checking if player can actually cast the spell
        foreach (KeyValuePair<string, int> kvp in requiredGlyphs)
        {
            if (player.glyphs[kvp.Key] >= 1)
                canCast = true;
        }
        if (canCast && player.iMana > iManaCost)
        {
            // subtract mana and glyph costs
            player.iMana -= iManaCost;
            foreach (KeyValuePair<string, int> kvp in requiredGlyphs)
                player.glyphs[kvp.Key] -= 1;

            PanelHolder.instance.displayNotify("You cast " + sSpellName + "!");
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
