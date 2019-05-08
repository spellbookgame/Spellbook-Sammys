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

        combatSpell = false;

        sSpellName = "Rune Conversion";
        sSpellClass = "Arcanist";
        sSpellInfo = "Discard one of your current runes in to draw one directly from the deck. Can cast on an ally.";

        requiredRunes.Add("Arcanist A Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            PanelHolder.instance.displayNotify(sSpellName, "Discard one of your current runes. Draw a new one from the deck.", "MainPlayerScene");

            player.numSpellsCastThisTurn++;
            SpellTracker.instance.lastSpellCasted = this;
        }
        else if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("Not enough Mana!", "You do not have enough mana to cast this spell.", "OK");
        }
        else
        {
            // subtract mana and glyph costs
            player.iMana -= iManaCost;

            PanelHolder.instance.displayNotify(sSpellName, "Discard one of your current runes. Draw a new one from the deck.", "MainPlayerScene");

            player.numSpellsCastThisTurn++;
            SpellTracker.instance.lastSpellCasted = this;
        }
    }
}
