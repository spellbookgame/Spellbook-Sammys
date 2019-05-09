using System.Collections.Generic;
using UnityEngine;

// spell for Illusion class
public class Playback : Spell
{
    public Playback()
    {
        iTier = 2;
        iManaCost = 1050;

        combatSpell = false;

        sSpellName = "Playback";
        sSpellClass = "Illusionist";
        sSpellInfo = "Swap a rune from your hand with one from any city. Can cast on an ally.";

        requiredRunes.Add("Illusionist B Rune", 1);
        requiredRunes.Add("Summoner B Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            PanelHolder.instance.displayNotify(sSpellName, "Swap a rune from your hand with one from any city.", "MainPlayerScene");

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

            PanelHolder.instance.displayNotify(sSpellName, "Swap a rune from your hand with one from any city.", "MainPlayerScene");

            player.numSpellsCastThisTurn++;
            SpellTracker.instance.lastSpellCasted = this;
        }
    }
}