using System.Collections.Generic;
using UnityEngine;

// spell for Elemental class
public class Tailwind : Spell
{
    public Tailwind()
    {
        iTier = 1;
        iManaCost = 2500;

        combatSpell = false;

        sSpellName = "Tailwind";
        sSpellClass = "Elementalist";
        sSpellInfo = "Everyone will receive an extra D6 to their movement next turn.";

        requiredRunes.Add("Elementalist A Rune", 1);
        requiredRunes.Add("Elementalist B Rune", 1);
        requiredRunes.Add("Elementalist C Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            SpellTracker.instance.RemoveFromActiveSpells("Call of the Moon - Umbra's Eclipse");
            PanelHolder.instance.displayNotify(sSpellName, "Everyone will receive a D6 to their movement next time they roll.", "OK");
            player.activeSpells.Add(this);
        }
        else if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("Not enough Mana!", "You do not have enough mana to cast this spell.", "OK");
        }
        else
        {
            // subtract mana
            player.iMana -= iManaCost;

            PanelHolder.instance.displayNotify(sSpellName, "Everyone will receive a D6 to their movement next time they roll.", "OK");
            player.activeSpells.Add(this);
        }
    }
}
