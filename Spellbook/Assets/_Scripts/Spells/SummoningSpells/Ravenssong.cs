using System.Collections.Generic;
using UnityEngine;

public class Ravenssong : Spell, ICombatSpell
{
    public Ravenssong()
    {
        iTier = 1;

        combatSpell = true;

        sSpellName = "Call of the Wild - Raven's Song";
        sSpellClass = "Summoner";
        sSpellInfo = "Destroy half of the enemy's current HP. This attack cannot be buffed.";

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
