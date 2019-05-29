using System.Collections.Generic;
using UnityEngine;

public class Ravenssong : Spell, ICombatSpell
{
    public Ravenssong()
    {
        iTier = 1;
        iCharges = 0;
        iManaCost = 2750;

        combatSpell = true;

        sSpellName = "Raven's Song";
        sSpellClass = "Summoner";
        sSpellInfo = "Heal all allies by 20%, and deal half of that total to the enemy.";

        requiredRunes.Add("Summoner A Rune", 1);
        requiredRunes.Add("Summoner B Rune", 1);
        requiredRunes.Add("Alchemist A Rune", 1);

        ColorUtility.TryParseHtmlString("#6E26A5", out colorPrimary);
        ColorUtility.TryParseHtmlString("#BFACC8", out colorSecondary);
        ColorUtility.TryParseHtmlString("#4A4063", out colorTertiary);

        guideLine = Resources.Load<Sprite>("CombatSwipes/Ravenssong");
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
