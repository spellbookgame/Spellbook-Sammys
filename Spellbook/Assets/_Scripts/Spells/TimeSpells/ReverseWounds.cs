using System.Collections.Generic;
using UnityEngine;

public class ReverseWounds : Spell, ICombatSpell, IAllyCastable
{
    public ReverseWounds()
    {
        iTier = 2;
        iCharges = 0;
        iManaCost = 1250;

        combatSpell = true;

        sSpellName = "Reverse Wounds";
        sSpellClass = "Chronomancer";
        sSpellInfo = "Heal your health equal to half of health missing. Can cast on an ally.";

        requiredRunes.Add("Chronomancer B Rune", 1);
        requiredRunes.Add("Alchemist B Rune", 1);

        ColorUtility.TryParseHtmlString("#CE4257", out colorPrimary);
        ColorUtility.TryParseHtmlString("#FF7F51", out colorSecondary);
        ColorUtility.TryParseHtmlString("#720026", out colorTertiary);

        guideLine = Resources.Load<Sprite>("CombatSwipes/ReverseWounds");
    }

    public void CombatCast()
    {
        throw new System.NotImplementedException();
    }

    public void RecieveCastFromAlly(SpellCaster player)
    {
        throw new System.NotImplementedException();
    }

    public override void SpellCast(SpellCaster player)
    {
        //Nothing.
    }

    public void SpellcastPhase2(int sID)
    {
        throw new System.NotImplementedException();
    }
}
