﻿using Bolt.Samples.Photon.Lobby;
using System.Collections.Generic;
using UnityEngine;

public class Bearsfury : Spell, ICombatSpell
{
    public Bearsfury()
    {
        iTier = 2;
        iCharges = 0;
        iManaCost = 1800;

        combatSpell = true;
        damageSpell = true;

        sSpellName = "Bear's Fury";
        sSpellClass = "Summoner";
        sSpellInfo = "Summon a bear that unleashes a flurry of swipes dealing 8-14 damage.";

        requiredRunes.Add("Summoner C Rune", 1);
        requiredRunes.Add("Illusionist B Rune", 1);

        ColorUtility.TryParseHtmlString("#FCA17D", out colorPrimary);
        ColorUtility.TryParseHtmlString("#F9DBBD", out colorSecondary);
        ColorUtility.TryParseHtmlString("#B55151", out colorTertiary);
    }

    public void CombatCast(SpellCaster player, float orbPercentage)
    {
        orbPercentage = orbPercentage * 100f;
        int damage;
        if (orbPercentage <= 25)
            damage = Random.Range(8, 11);
        else if (orbPercentage > 25 && orbPercentage <= 50)
            damage = Random.Range(9, 12);
        else if (orbPercentage > 50 && orbPercentage <= 75)
            damage = Random.Range(9, 14);
        else
            damage = Random.Range(10, 15);

        damageDealt = damage;
        NetworkManager.s_Singleton.DealDmgToBoss(damage);
    }

    public override void SpellCast(SpellCaster player)
    {
        //Nothing.
    }
    
}
