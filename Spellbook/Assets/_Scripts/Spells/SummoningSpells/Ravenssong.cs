using Bolt.Samples.Photon.Lobby;
using System.Collections.Generic;
using UnityEngine;

public class Ravenssong : Spell, ICombatSpell
{
    public Ravenssong()
    {
        iTier = 1;
        iCharges = 0;
        iManaCost = 2750;

        combatSpell = true;

        sSpellName = "Raven's Song";
        sSpellClass = "Summoner";
        sSpellInfo = "Heal all allies by 20% of their current health, and deal 10% damage to the enemy.";

        requiredRunes.Add("Summoner A Rune", 1);
        requiredRunes.Add("Summoner B Rune", 1);
        requiredRunes.Add("Alchemist A Rune", 1);

        ColorUtility.TryParseHtmlString("#6E26A5", out colorPrimary);
        ColorUtility.TryParseHtmlString("#BFACC8", out colorSecondary);
        ColorUtility.TryParseHtmlString("#4A4063", out colorTertiary);
    }

    public void CombatCast(SpellCaster player, float orbPercentage)
    {
        orbPercentage = orbPercentage * 100f;
        float multiplier = ((Mathf.Floor(orbPercentage / 20f) * 5f) + 20f) / 100f;
        //int healAmount = (int) player.fCurrentHealth * multiplier;
        //player.HealDamage(healAmount);
        //enemy.DealDamage(healAmount / 2);
        // NetworkManager.s_Singleton.HealAllAlliesByPercent(multiplier, sSpellName);
        // NetworkManager.s_Singleton.DealPercentDmgToBoss(0.1f);
        NetworkManager.s_Singleton.Ravensong(multiplier, 0.1f);
    }

    public override void SpellCast(SpellCaster player)
    {
        //Nothing.
    }
    
}
