using System.Collections.Generic;
using UnityEngine;

public class Manipulate : Spell, ICombatSpell
{
    public Manipulate()
    {
        iTier = 2;

        combatSpell = true;

        sSpellName = "Manipulate";
        sSpellClass = "Chronomancer";
        sSpellInfo = "Passive: increase the tap time in combat by 2 seconds.";

        requiredRunes.Add("Chronomancer B Rune", 1);
        requiredRunes.Add("Arcanist B Rune", 1);

        ColorUtility.TryParseHtmlString("#44BBA4", out colorPrimary);
        ColorUtility.TryParseHtmlString("#E7BB41", out colorSecondary);
        ColorUtility.TryParseHtmlString("#E7E5DF", out colorTertiary);

        guideLine = Resources.Load<Sprite>("CombatSwipes/Manipulate");
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
