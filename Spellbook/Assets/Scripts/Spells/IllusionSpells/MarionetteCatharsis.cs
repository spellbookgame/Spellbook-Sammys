using System.Collections.Generic;
using UnityEngine;

// spell for Trickster class
public class MarionetteCatharsis : Spell
{
    public MarionetteCatharsis()
    {
        iTier = 3;
        iManaCost = 200;
        iCoolDown = 0;

        sSpellName = "Marionette - Catharsis";
        sSpellClass = "Trickster";
        sSpellInfo = "Possess a puppet that attacks the enemy for 1-4 damage. It will remain for the duration of the fight.";

        requiredGlyphs.Add("Illusion C Glyph", 1);
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

            PanelHolder.instance.displayNotify(sSpellName + " was cast.");
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
