using System.Collections.Generic;
using UnityEngine;

public class RunicDarts : Spell, ICombatSpell
{
    public RunicDarts()
    {
        iTier = 2;
        iCharges = 0;
        iManaCost = 1200;

        combatSpell = true;

        sSpellName = "Runic Darts";
        sSpellClass = "Arcanist";
        sSpellInfo = "Cast 3 darts forged from the magic of your runes to deal 2-5 damage each.";

        requiredRunes.Add("Arcanist B Rune", 1);
        requiredRunes.Add("Chronomancer B Rune", 1);

        ColorUtility.TryParseHtmlString("#274690", out colorPrimary);
        ColorUtility.TryParseHtmlString("#302B27", out colorSecondary);
        ColorUtility.TryParseHtmlString("#F5F3F5", out colorTertiary);

        guideLine = Resources.Load<Sprite>("CombatSwipes/RunicDarts");
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
