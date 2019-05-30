using System.Collections.Generic;
using UnityEngine;

public class NaturalDisaster : Spell, ICombatSpell
{
    public NaturalDisaster()
    {
        iTier = 1;
        iCharges = 0;
        iManaCost = 3300;

        combatSpell = true;

        sSpellName = "Natural Disaster";
        sSpellClass = "Elementalist";
        sSpellInfo = "Remove 50% of the enemy's current health. This effect cannot be buffed.";

        requiredRunes.Add("Elementalist A Rune", 1);
        requiredRunes.Add("Elementalist B Rune", 1);
        requiredRunes.Add("Alchemist B Rune", 1);

        ColorUtility.TryParseHtmlString("#D19C1D", out colorPrimary);
        ColorUtility.TryParseHtmlString("#D7F75B", out colorSecondary);
        ColorUtility.TryParseHtmlString("#9BE564", out colorTertiary);

        guideLine = Resources.Load<Sprite>("CombatSwipes/NaturalDisaster");
    }

    public void CombatCast(SpellCaster player)
    {
        throw new System.NotImplementedException();
    }

    public override void SpellCast(SpellCaster player)
    {
        //Nothing.
    }
    
}
