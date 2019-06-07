using Bolt.Samples.Photon.Lobby;
using System.Collections.Generic;
using UnityEngine;

public class EyeOfTheStorm : Spell, ICombatSpell
{
    public EyeOfTheStorm()
    {
        iTier = 2;
        iCharges = 0;
        iManaCost = 1900;

        combatSpell = true;
        damageSpell = true;

        sSpellName = "Eye of the Storm";
        sSpellClass = "Elementalist";
        sSpellInfo = "Deal 7-15 damage and heal all allies by half the damage dealt.";

        requiredRunes.Add("Elementalist A Rune", 1);
        requiredRunes.Add("Elementalist B Rune", 1);
        requiredRunes.Add("Chronomancer B Rune", 1);

        ColorUtility.TryParseHtmlString("#576CC1", out colorPrimary);
        ColorUtility.TryParseHtmlString("#8C4170", out colorSecondary);
        ColorUtility.TryParseHtmlString("#8370FF", out colorTertiary);
    }

    public void CombatCast(SpellCaster player, float orbPercentage)
    {
        orbPercentage = orbPercentage * 100;
        int damage, healAmount;
        if (orbPercentage <= 25)
        {
            damage = Random.Range(7, 10);
        }
        else if (orbPercentage > 25 && orbPercentage <= 50)
        {
            damage = Random.Range(7, 12);
        }
        else if (orbPercentage > 50 && orbPercentage <= 75)
        {
            damage = Random.Range(9, 12);
        }
        else
        {
            damage = Random.Range(9, 16);
        }
        healAmount = damage / 2;
        damageDealt = damage;
        //player.HealDamage(healAmount);
        // NetworkManager.s_Singleton.DealDmgToBoss(damage);
        // NetworkManager.s_Singleton.HealAllAlliesByHp(healAmount, sSpellName);
        NetworkManager.s_Singleton.EyeOfTheStorm(healAmount, damage);
    }

    public override void SpellCast(SpellCaster player)
    {
        //Nothing.
    }
    
}
