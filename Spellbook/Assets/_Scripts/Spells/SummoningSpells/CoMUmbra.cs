using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// example spell for Summoning class
public class CoMUmbra : Spell
{
    public CoMUmbra()
    {
        iTier = 2;
        iManaCost = 1000;

        combatSpell = false;

        sSpellName = "Call of the Moon - Umbra's Eclipse";
        sSpellClass = "Summoner";
        sSpellInfo = "Your next spell you cast (except this one) will be free. Can cast on an ally.";

        requiredRunes.Add("Summoner B Rune", 1);
        requiredRunes.Add("Arcanist B Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("Not enough Mana!", "You do not have enough mana to cast this spell.", "OK");
        }
        else
        {
            // subtract mana
            player.iMana -= iManaCost;

            PanelHolder.instance.displayNotify(sSpellName, "Your next spell you cast will be free.", "MainPlayerScene");
            player.activeSpells.Add(this);
        }
    }
}
