using System.Collections.Generic;
using UnityEngine;

public class Catastrophe : Spell, ICombatSpell
{
    public Catastrophe()
    {
        iTier = 2;

        combatSpell = true;

        sSpellName = "Catastrophe";
        sSpellClass = "Illusionist";
        sSpellInfo = "Create an illusionary puppet that will increase your team's total tap count by 50% for 2 rounds.";

        requiredRunes.Add("Illusionist B Rune", 1);
        requiredRunes.Add("Arcanist B Rune", 1);

        ColorUtility.TryParseHtmlString("#005C69", out colorPrimary);
        ColorUtility.TryParseHtmlString("#950952", out colorSecondary);
        ColorUtility.TryParseHtmlString("#023618", out colorTertiary);

        guideLine = Resources.Load<Sprite>("CombatSwipes/Catastrophe");
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
