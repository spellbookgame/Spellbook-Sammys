using System.Collections.Generic;
using UnityEngine;

public class Catastrophe : Spell, ICombatSpell
{
    public Catastrophe()
    {
        iTier = 2;
        iCharges = 0;
        iManaCost = 1500;

        combatSpell = true;

        sSpellName = "Catastrophe";
        sSpellClass = "Illusionist";
        sSpellInfo = "Create an illusionary puppet that will increase an ally's damage output by 20% this round.";

        requiredRunes.Add("Illusionist B Rune", 1);
        requiredRunes.Add("Arcanist B Rune", 1);

        ColorUtility.TryParseHtmlString("#005C69", out colorPrimary);
        ColorUtility.TryParseHtmlString("#950952", out colorSecondary);
        ColorUtility.TryParseHtmlString("#023618", out colorTertiary);

        guideLine = Resources.Load<Sprite>("CombatSwipes/Catastrophe");
    }

    public void CombatCast(SpellCaster player)
    {
        // throw new System.NotImplementedException();
        // for every 20% the orb is filled, increase the multiplier by 5%
        // float multiplier = ((Mathf.Floor(orbPercentage / 20) * 5) + 20) / 100
        // player.totalDamage += player.totalDamge * multiplier;
    }

    public override void SpellCast(SpellCaster player)
    {
        //Nothing.
    }
    
}
