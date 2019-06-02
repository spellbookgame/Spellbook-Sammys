using System.Collections.Generic;
using UnityEngine;

// spell for Alchemy class
public class ToxicPotion : Spell, ICombatSpell
{
    public ToxicPotion()
    {
        iTier = 3;
        iCharges = 0;
        iManaCost = 900;

        combatSpell = true;

        sSpellName = "Toxic Potion";
        sSpellClass = "Alchemist";
        sSpellInfo = "Brew a toxic potion that will increase the team's damage output by 5% this round.";

        requiredRunes.Add("Alchemist C Rune", 1);

        ColorUtility.TryParseHtmlString("#AF47D8", out colorPrimary);
        ColorUtility.TryParseHtmlString("#521945", out colorSecondary);
        ColorUtility.TryParseHtmlString("#880044", out colorTertiary);

        guideLine = Resources.Load<Sprite>("CombatSwipes/ToxicPotion");
    }

    public void CombatCast(SpellCaster player)
    {
        // throw new System.NotImplementedException();
        // for every 20% the orb is filled, add 5% to the damage multiplier
        // int multiplier = 5 + (Mathf.Floor(orbPercentage(out of 100) / 20) * 5);
        // teamOutput += teamOutput * (multiplier / 100);
    }

    public override void SpellCast(SpellCaster player)
    {
        //Nothing.
    }
    
}
