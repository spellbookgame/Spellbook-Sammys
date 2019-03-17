using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// spell for Arcanist class
public class Transcribe : Spell
{
    public Transcribe()
    {
        iTier = 1;
        iManaCost = 1500;
        iCoolDown = 3;

        sSpellName = "Transcribe";
        sSpellClass = "Arcanist";
        sSpellInfo = "Upgrade a glyph into its next highest tier. Can cast on an ally.";

        requiredGlyphs.Add("Alchemy B Glyph", 1);
        requiredGlyphs.Add("Arcane A Glyph", 1);
        requiredGlyphs.Add("Illusion B Glyph", 1);
    }

    // SpellCast is called from SpellCastHandler.cs
    public override void SpellCast(SpellCaster player)
    {
        // subtract mana 
        player.iMana -= iManaCost;

        PanelHolder.instance.displayNotify("You cast " + sSpellName, "");
    }
}
