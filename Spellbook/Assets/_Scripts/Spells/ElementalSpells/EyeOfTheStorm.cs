using System.Collections.Generic;
using UnityEngine;

public class EyeOfTheStorm : Spell, ICombatSpell
{
    public EyeOfTheStorm()
    {
        iTier = 2;
        iCharges = 0;
        iManaCost = 1900;

        combatSpell = true;

        sSpellName = "EyeOfTheStorm";
        sSpellClass = "Elementalist";
        sSpellInfo = "Deal 6-18 damage and heal all allies by half the damage dealt.";

        requiredRunes.Add("Elementalist A Rune", 1);
        requiredRunes.Add("Elementalist B Rune", 1);
        requiredRunes.Add("Chronomancer B Rune", 1);

        ColorUtility.TryParseHtmlString("#576CC1", out colorPrimary);
        ColorUtility.TryParseHtmlString("#8C4170", out colorSecondary);
        ColorUtility.TryParseHtmlString("#8370FF", out colorTertiary);

        guideLine = Resources.Load<Sprite>("CombatSwipes/EyeOfTheStorm");
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
