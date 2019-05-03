using System.Collections.Generic;
using UnityEngine;

// spell for Illusion class
public class Allegro : Spell
{
    public Allegro()
    {
        iTier = 3;
        iManaCost = 600;

        combatSpell = false;

        sSpellName = "Allegro";
        sSpellClass = "Illusionist";
        sSpellInfo = "Add a D6 to your movement next time you roll. Can cast on an ally.";

        requiredRunes.Add("Illusionist D Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            PanelHolder.instance.displayNotify(sSpellName, "You and your ally will have an additional D6 to your movement next time you roll.", "OK");
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

            PanelHolder.instance.displayNotify(sSpellName, "You and your ally will have an additional D6 to your movement next time you roll.", "OK");
            player.activeSpells.Add(this);
        }
    }
}