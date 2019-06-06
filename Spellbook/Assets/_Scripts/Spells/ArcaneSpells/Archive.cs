using Bolt.Samples.Photon.Lobby;
using System.Collections.Generic;
using UnityEngine;

// spell for Arcane class
public class Archive : Spell, ICombatSpell
{
    public Archive()
    {
        iTier = 2;
        iCharges = 0;
        iManaCost = 2000; 

        combatSpell = true;

        sSpellName = "Archive";
        sSpellClass = "Arcanist";
        sSpellInfo = "Everyone's taps will be increased by 15% this round.";

        requiredRunes.Add("Arcanist A Rune", 1);
        requiredRunes.Add("Illusionist B Rune", 1);

        ColorUtility.TryParseHtmlString("#7F055F", out colorPrimary);
        ColorUtility.TryParseHtmlString("#45062E", out colorSecondary);
        ColorUtility.TryParseHtmlString("#E5A4CB", out colorTertiary);
    }

    public void CombatCast(SpellCaster player, float orbPercentage)
    {
        // for every 20% the orb is filled, add 5% to the multiplier.
        orbPercentage = orbPercentage * 100;
        int tapBuff = (int) Mathf.Floor(orbPercentage / 20) * 5;
        int totalIncrease = (tapBuff + 15) / 100;
        //teamTapTotal += teamTapTotal * totalIncrease;
        NetworkManager.s_Singleton.IncreaseTeamTapPercentage(totalIncrease, sSpellName);
    }

    public override void SpellCast(SpellCaster player)
    {
        //Nothing.
    }
    
}
