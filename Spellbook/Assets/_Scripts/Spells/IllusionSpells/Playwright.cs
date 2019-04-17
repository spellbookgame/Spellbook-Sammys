using System.Collections.Generic;
using UnityEngine;

// spell for Trickster class
public class Playwright : Spell
{
    public Playwright()
    {
        iTier = 1;
        iManaCost = 1600;
        iCoolDown = 3;

        sSpellName = "Playwright";
        sSpellClass = "Trickster";
        sSpellInfo = "Destroy one of your puppets and upgrade two glyphs into their next highest tier. Can cast on an ally."
                        + "Cannot cast if you do not have any active puppets.";

        requiredGlyphs.Add("Illusion A Glyph", 1);
        requiredGlyphs.Add("Illusion B Glyph", 1);
        requiredGlyphs.Add("Arcane A Glyph", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // subtract mana and glyph costs
        player.iMana -= iManaCost;
           
        PanelHolder.instance.displayNotify("You cast " + sSpellName, "", "OK");
    }
}
