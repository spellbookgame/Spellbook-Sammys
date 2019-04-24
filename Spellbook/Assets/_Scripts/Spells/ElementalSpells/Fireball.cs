using System.Collections.Generic;
using UnityEngine;

// spell for Elemental class
public class Fireball : Spell
{
    public Fireball()
    {
        iTier = 3;
        iManaCost = 600;

        combatSpell = true;

        sSpellName = "Fireball";
        sSpellClass = "Elementalist";
        sSpellInfo = "Cast 2 fireballs that deal 1-6 damage each.";

        requiredRunes.Add("Elementalist D Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // nothing
    }
}
