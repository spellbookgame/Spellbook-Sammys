using System.Collections.Generic;
using UnityEngine;

// spell for Alchemy class
public class ToxicPotion : Spell
{
    public ToxicPotion()
    {
        iTier = 3;
        iManaCost = 300;
        iCoolDown = 0;

        sSpellName = "Brew - Toxic Potion";
        sSpellClass = "Alchemist";
        sSpellInfo = "Brew a toxic potion that will grant an additional 3 " +
                        "damage to your attacks for the duration of the fight. Can cast on an ally.";

        requiredGlyphs.Add("Alchemy C Glyph", 1);
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
            
            PanelHolder.instance.displayNotify("You cast " + sSpellName + ". You have +3 to your attacks for the duration of this fight.");
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
