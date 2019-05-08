using System.Collections.Generic;
using UnityEngine;

// spell for Illusion class
public class CheatOut : Spell
{
    public CheatOut()
    {
        iTier = 1;
        iManaCost = 3100;

        combatSpell = false;

        sSpellName = "Cheat Out";
        sSpellClass = "Illusionist";
        sSpellInfo = "Replace two of your runes with high tier runes drawn from the deck. Can cast on an ally.";

        requiredRunes.Add("Illusionist A Rune", 1);
        requiredRunes.Add("Chronomancer A Rune", 1);
        requiredRunes.Add("Alchemist B Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            PanelHolder.instance.displayNotify(sSpellName, "Discard two of your runes. Draw 2 from the high tier rune deck.", "MainPlayerScene");

            player.numSpellsCastThisTurn++;
            SpellTracker.instance.lastSpellCasted = this;
        }
        else if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("Not enough Mana!", "You do not have enough mana to cast this spell.", "OK");
        }
        else
        {
            // subtract mana
            player.iMana -= iManaCost;

            PanelHolder.instance.displayNotify(sSpellName, "Discard two of your runes. Draw 2 from the high tier rune deck.", "MainPlayerScene");

            player.numSpellsCastThisTurn++;
            SpellTracker.instance.lastSpellCasted = this;
        }
    }
}