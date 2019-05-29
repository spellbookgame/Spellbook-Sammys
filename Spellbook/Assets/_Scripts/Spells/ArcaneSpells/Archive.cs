using System.Collections.Generic;
using UnityEngine;

// spell for Arcane class
public class Archive : Spell, ICombatSpell
{
    public Archive()
    {
        iTier = 2;
        iCharges = 0;
        iManaCost = 2000; 

        combatSpell = true;

        sSpellName = "Archive";
        sSpellClass = "Arcanist";
        sSpellInfo = "Everyone's taps will be increased by 10%.";

        requiredRunes.Add("Arcanist A Rune", 1);
        requiredRunes.Add("Illusionist B Rune", 1);

        ColorUtility.TryParseHtmlString("#7F055F", out colorPrimary);
        ColorUtility.TryParseHtmlString("#45062E", out colorSecondary);
        ColorUtility.TryParseHtmlString("#E5A4CB", out colorTertiary);

        guideLine = Resources.Load<Sprite>("CombatSwipes/Archive");
    }

    public void CombatCast()
    {
        throw new System.NotImplementedException();
    }

    public override void SpellCast(SpellCaster player)
    {
        //Nothing.
    }
    
}
