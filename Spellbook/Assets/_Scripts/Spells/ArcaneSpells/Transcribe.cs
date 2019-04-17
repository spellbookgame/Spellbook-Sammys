using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// spell for Arcanist class
public class Transcribe : Spell
{
    public Transcribe()
    {
        iTier = 1;
        iManaCost = 2800;

        sSpellName = "Transcribe";
        sSpellClass = "Arcanist";
        sSpellInfo = "Discard your rune hand and draw new ones from the top tier deck.";

        requiredGlyphs.Add("Arcane A Glyph", 1);
        requiredGlyphs.Add("Illusion A Glyph", 1);
        requiredGlyphs.Add("Illusion B Glyph", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // subtract mana 
        player.iMana -= iManaCost;

        PanelHolder.instance.displayNotify(sSpellName, "Discard your rune hand and draw new ones from the top tier deck.", "OK");
    }
}
