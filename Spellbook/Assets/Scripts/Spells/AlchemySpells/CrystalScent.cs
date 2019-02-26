using System.Collections.Generic;
using UnityEngine;

// spell for Alchemy class
public class CrystalScent : Spell
{
    public CrystalScent()
    {
        iTier = 3;
        iManaCost = 100;
        iCoolDown = 0;
        iTurnsActive = 3;

        sSpellName = "Brew - Crystal Scent";
        sSpellClass = "Alchemist";
        sSpellInfo = "You will have a 20% increase in mana crystals whenever you collect mana for the next 3 turns. Can cast on an ally.";

        requiredGlyphs.Add("Alchemy D Glyph", 1);
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
        // cast the spell
        if (canCast && player.iMana > iManaCost)
        {
            // subtract mana and glyph costs
            player.iMana -= iManaCost;
            foreach (KeyValuePair<string, int> kvp in requiredGlyphs)
                player.glyphs[kvp.Key] -= 1;

            PanelHolder.instance.displayNotify("You cast " + sSpellName + ". For the next 3 turns, you will gain 20% more mana crystals.");

            // iCastedTurn is the number turn that player casted the spell.
            iCastedTurn = player.NumOfTurnsSoFar;
            player.activeSpells.Add(this);
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
