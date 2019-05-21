using System.Collections.Generic;
using UnityEngine;

public class Skeletons : Spell, ICombatSpell
{
    public Skeletons()
    {
        iTier = 3;
        iCharges = 0;
        iManaCost = 900;

        combatSpell = true;

        sSpellName = "Skeletons";
        sSpellClass = "Summoner";
        sSpellInfo = "Summon a skeleton that attacks the enemy for 5-10 damage.";

        requiredRunes.Add("Summoner D Rune", 1);

        ColorUtility.TryParseHtmlString("#F2F5EA", out colorPrimary);
        ColorUtility.TryParseHtmlString("#2C363F", out colorSecondary);
        ColorUtility.TryParseHtmlString("#E75A7C", out colorTertiary);

        guideLine = Resources.Load<Sprite>("CombatSwipes/Skeletons");
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
