using System.Collections.Generic;
using UnityEngine;

public class Chronoblast : Spell, ICombatSpell
{
    public Chronoblast()
    {
        iTier = 3;
        iCharges = 0;
        iManaCost = 700;

        combatSpell = true;

        sSpellName = "Chronoblast";
        sSpellClass = "Chronomancer";
        sSpellInfo = "Deal 4-8 damage to the enemy.";

        requiredRunes.Add("Chronomancer D Rune", 1);

        ColorUtility.TryParseHtmlString("#F9DF36", out colorPrimary);
        ColorUtility.TryParseHtmlString("#E06D06", out colorSecondary);
        ColorUtility.TryParseHtmlString("#FAFF81", out colorTertiary);

        guideLine = Resources.Load<Sprite>("CombatSwipes/Chronoblast");
    }

    public void CombatCast(SpellCaster player)
    {
        // throw new System.NotImplementedException();
        /*int damage;
        if (orbPercentage <= 25)
            damage = Random.Range(4, 6);
        else if (orbPercentage > 25 && orbPercentage <= 50)
            damage = Random.Range(4, 7);
        else if (orbPercentage > 50 && orbPercentage <= 75)
            damage = Random.Range(5, 8);
        else
            damage = Random.Range(5, 9);*/
    }

    public override void SpellCast(SpellCaster player)
    {
        //Nothing.
    }
    
}
