﻿using Bolt.Samples.Photon.Lobby;
using System.Collections.Generic;
using UnityEngine;

public class Manipulate : Spell, ICombatSpell
{
    public Manipulate()
    {
        iTier = 2;
        iCharges = 0;
        iManaCost = 2400;

        combatSpell = true;

        sSpellName = "Manipulate";
        sSpellClass = "Chronomancer";
        sSpellInfo = "Passive: if this spell is selected, increase the tap time by 2 seconds for this round. This effect cannot be buffed.";

        requiredRunes.Add("Chronomancer B Rune", 1);
        requiredRunes.Add("Arcanist B Rune", 1);

        ColorUtility.TryParseHtmlString("#44BBA4", out colorPrimary);
        ColorUtility.TryParseHtmlString("#E7BB41", out colorSecondary);
        ColorUtility.TryParseHtmlString("#E7E5DF", out colorTertiary);
    }

    public void CombatCast(SpellCaster player, float orbPercentage)
    {
        // throw new System.NotImplementedException();
        // player.TapTime += 2f;
        NetworkManager.s_Singleton.IncreaseTapTimeBy2Sec(sSpellName);
    }

    public override void SpellCast(SpellCaster player)
    {
        //Nothing.
    }
    
}
