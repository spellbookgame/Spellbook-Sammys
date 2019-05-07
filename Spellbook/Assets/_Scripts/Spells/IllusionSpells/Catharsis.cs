using System.Collections.Generic;
using UnityEngine;

public class Catharsis : Spell, ICombatSpell
{
    public Catharsis()
    {
        iTier = 2;

        combatSpell = true;

        sSpellName = "Catharsis";
        sSpellClass = "Illusionist";
        sSpellInfo = "Heal all allies by 5 points each.";

        requiredRunes.Add("Illusionist B Rune", 1);
        requiredRunes.Add("Alchemist A Rune", 1);

        ColorUtility.TryParseHtmlString("#9DDBAD", out colorPrimary);
        ColorUtility.TryParseHtmlString("#FFC145", out colorSecondary);
        ColorUtility.TryParseHtmlString("#FFFFFB", out colorTertiary);

        guideLine = Resources.Load<Sprite>("CombatSwipes/Catharsis");
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
