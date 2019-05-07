using System.Collections.Generic;
using UnityEngine;

public class Bearsfury : Spell, ICombatSpell
{
    public Bearsfury()
    {
        iTier = 2;

        combatSpell = true;

        sSpellName = "Call of the Wild - Bear's Fury";
        sSpellClass = "Summoner";
        sSpellInfo = "Summon a bear that unleashes a flurry of swipes dealing 7-18 damage.";

        requiredRunes.Add("Summoner C Rune", 1);
        requiredRunes.Add("Illusionist B Rune", 1);

        ColorUtility.TryParseHtmlString("#FCA17D", out colorPrimary);
        ColorUtility.TryParseHtmlString("#F9DBBD", out colorSecondary);
        ColorUtility.TryParseHtmlString("#B55151", out colorTertiary);

        guideLine = Resources.Load<Sprite>("CombatSwipes/Bearsfury");
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
