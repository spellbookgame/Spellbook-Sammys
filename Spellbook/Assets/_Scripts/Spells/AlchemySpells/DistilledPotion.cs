using System.Collections.Generic;
using UnityEngine;

// spell for Alchemy class
public class DistilledPotion : Spell, ICombatSpell
{
    public DistilledPotion ()
    {
        iTier = 2;
        iCharges = 0;
        iManaCost = 1500;

        combatSpell = true;

        sSpellName = "Distilled Potion";
        sSpellClass = "Alchemist";
        sSpellInfo = "Heal all allies by 50% of their missing health.";

        requiredRunes.Add("Alchemist C Rune", 1);
        requiredRunes.Add("Alchemist D Rune", 1);

        ColorUtility.TryParseHtmlString("#CCD5FF", out colorPrimary);
        ColorUtility.TryParseHtmlString("#7FE7FF", out colorSecondary);
        ColorUtility.TryParseHtmlString("#D3F8E2", out colorTertiary);
        guideLine = Resources.Load<Sprite>("CombatSwipes/DistilledPotion");
    }

    public void CombatCast(SpellCaster player)
    {
        //throw new System.NotImplementedException();
        int missingHealth = (int)(player.fMaxHealth - player.fCurrentHealth);
        if(missingHealth > 0)
        {
            // for every 20% the orb is filled, increase the heal amount by 5%
            // (int healBuff = Mathf.Floor(orbPercentage(out of 100) / 20))
            // int newHeal = (int)(2 - (healBuff / 100));
            // player.HealDamage(missingHealth / newHeal);
        }
    }

    public override void SpellCast(SpellCaster player)
    {
        //Nothing.
    }
    
}
