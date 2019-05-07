using System.Collections.Generic;
using UnityEngine;

public class MarcellasBlessing : Spell, ICombatSpell
{
    public MarcellasBlessing()
    {
        iTier = 1;

        combatSpell = true;

        sSpellName = "Marcellas Blessing ";
        sSpellClass = "Arcanist";
        sSpellInfo = "Cast this during combat to grant everyone double loot from the fight. This spell cannot be upgraded.";

        requiredRunes.Add("Alchemist A Rune", 1);
        requiredRunes.Add("Arcanist A Rune", 1);
        requiredRunes.Add("Arcanist B Rune", 1);

        ColorUtility.TryParseHtmlString("#4062BB", out colorPrimary);
        ColorUtility.TryParseHtmlString("#59C3C3", out colorSecondary);
        ColorUtility.TryParseHtmlString("#F45B69", out colorTertiary);

        guideLine = Resources.Load<Sprite>("CombatSwipes/MarcellasBlessing");
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
