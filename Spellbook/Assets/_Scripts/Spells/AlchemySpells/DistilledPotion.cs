using System.Collections.Generic;
using UnityEngine;

// spell for Alchemy class
public class DistilledPotion : Spell, ICombatSpell
{
    public DistilledPotion ()
    {
        iTier = 2;
        iCharges = 0;
        iManaCost = 1500;

        combatSpell = true;

        sSpellName = "Distilled Potion";
        sSpellClass = "Alchemist";
        sSpellInfo = "Brew a potion that will heal your health equal to half of health missing. Can cast on an ally.";

        requiredRunes.Add("Alchemist C Rune", 1);
        requiredRunes.Add("Alchemist D Rune", 1);
        ColorUtility.TryParseHtmlString("#CCD5FF", out colorPrimary);
        ColorUtility.TryParseHtmlString("#7FE7FF", out colorSecondary);
        ColorUtility.TryParseHtmlString("#D3F8E2", out colorTertiary);
        guideLine = Resources.Load<Sprite>("CombatSwipes/DistilledPotion");
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
