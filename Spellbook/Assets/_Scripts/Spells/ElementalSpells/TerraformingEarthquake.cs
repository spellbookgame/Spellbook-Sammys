using System.Collections.Generic;
using UnityEngine;

// spell for Elemental class
public class TerraformingEarthquake : Spell
{
    public TerraformingEarthquake()
    {
        iTier = 2;
        iManaCost = 1300;

        combatSpell = false;

        sSpellName = "Terraforming Earthquake";
        sSpellClass = "Elementalist";
        sSpellInfo = "Move to the closest location to you. If you are already on a location, choose an adjacent one to move to. Can cast on an ally.";

        requiredRunes.Add("Elementalist B Rune", 1);
        requiredRunes.Add("Arcanist B Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            PanelHolder.instance.displayNotify(sSpellName, "Move to the closest location to you. If you are already on a location, choose an adjacent one to move to.", "MainPlayerScene");
        }
        else if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("Not enough Mana!", "You do not have enough mana to cast this spell.", "OK");
        }
        else
        {
            // subtract mana
            player.iMana -= iManaCost;

            PanelHolder.instance.displayNotify(sSpellName, "Move to the closest location to you. If you are already on a location, choose an adjacent one to move to.", "MainPlayerScene");
        }
    }
}
