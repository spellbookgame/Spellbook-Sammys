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
        // subtract mana and glyph costs
        player.iMana -= iManaCost;
            

        PanelHolder.instance.displayNotify("You cast " + sSpellName, "", "OK");
    }
}
