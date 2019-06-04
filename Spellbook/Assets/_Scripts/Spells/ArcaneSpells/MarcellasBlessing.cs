using Bolt.Samples.Photon.Lobby;
using System.Collections.Generic;
using UnityEngine;

public class MarcellasBlessing : Spell, ICombatSpell
{
    public MarcellasBlessing()
    {
        iTier = 1;
        iCharges = 0;
        iManaCost = 2400;

        combatSpell = true;

        sSpellName = "Marcella's Blessing ";
        sSpellClass = "Arcanist";
        sSpellInfo = "Double the team's total damage output. This effect cannot be buffed.";

        requiredRunes.Add("Alchemist A Rune", 1);
        requiredRunes.Add("Arcanist A Rune", 1);
        requiredRunes.Add("Arcanist B Rune", 1);

        ColorUtility.TryParseHtmlString("#4062BB", out colorPrimary);
        ColorUtility.TryParseHtmlString("#59C3C3", out colorSecondary);
        ColorUtility.TryParseHtmlString("#F45B69", out colorTertiary);

        guideLine = Resources.Load<Sprite>("CombatSwipes/MarcellasBlessing");
    }

    public void CombatCast(SpellCaster player, float orbPercentage)
    {
        // throw new System.NotImplementedException();
        // int totalDamage = teamDamage * 2;
        
        //Double the teams damage
        NetworkManager.s_Singleton.IncreaseTeamDamageByPercent(1f, sSpellName); 
    }

    public override void SpellCast(SpellCaster player)
    {
        //Nothing.
    }
    
}
