using System.Collections.Generic;
using UnityEngine;

public class Tragedy : Spell, ICombatSpell
{
    public Tragedy()
    {
        iTier = 3;

        combatSpell = true;

        sSpellName = "Tragedy";
        sSpellClass = "Illusionist";
        sSpellInfo = "Create an illusionary puppet that deals 8 damage to the enemy.";

        requiredRunes.Add("Illusionist C Rune", 1);

        ColorUtility.TryParseHtmlString("#9759C6", out colorPrimary);
        ColorUtility.TryParseHtmlString("#CC3F3F", out colorSecondary);
        ColorUtility.TryParseHtmlString("#222A68", out colorTertiary);

        guideLine = Resources.Load<Sprite>("CombatSwipes/Tragedy");
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
