using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// spell for Arcanist class
public class RuneConversion : Spell
{
    public RuneConversion()
    {
        iTier = 3;
        iManaCost = 500;

        sSpellName = "Rune Conversion";
        sSpellClass = "Arcanist";
        sSpellInfo = "Discard one of your current runes in to draw one directly from the deck. Can cast on an ally.";

        requiredGlyphs.Add("Arcane A Glyph", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            SpellTracker.instance.RemoveFromActiveSpells("Call of the Moon - Umbra's Eclipse");
            PanelHolder.instance.displayNotify(sSpellName, "Discard one of your current runes. Draw a new one from the deck.", "OK");
        }
        else if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("Not enough Mana!", "You do not have enough mana to cast this spell.", "OK");
        }
        else
        {
            // subtract mana and glyph costs
            player.iMana -= iManaCost;

            PanelHolder.instance.displayNotify(sSpellName, "Discard one of your current runes. Draw a new one from the deck.", "OK");
        }
    }
}
