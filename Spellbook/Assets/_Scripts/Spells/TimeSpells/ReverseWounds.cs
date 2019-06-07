using Bolt.Samples.Photon.Lobby;
using System.Collections.Generic;
using UnityEngine;

public class ReverseWounds : Spell, ICombatSpell
{
    public ReverseWounds()
    {
        iTier = 2;
        iCharges = 0;
        iManaCost = 1250;

        combatSpell = true;

        sSpellName = "Reverse Wounds";
        sSpellClass = "Chronomancer";
        sSpellInfo = "Heal all allies by 5% of their max health.";

        requiredRunes.Add("Chronomancer B Rune", 1);
        requiredRunes.Add("Alchemist B Rune", 1);

        ColorUtility.TryParseHtmlString("#CE4257", out colorPrimary);
        ColorUtility.TryParseHtmlString("#FF7F51", out colorSecondary);
        ColorUtility.TryParseHtmlString("#720026", out colorTertiary);
    }

    public void CombatCast(SpellCaster player, float orbPercentage)
    {
        orbPercentage = orbPercentage * 100f;
        float multiplier = ((Mathf.Floor(orbPercentage / 20f) * 5f) + 5f) / 100f;
        //int healAmount = (int) player.fMaxHealth * multiplier;
        //player.HealDamage(healAmount);
        NetworkManager.s_Singleton.HealAllAlliesByPercent(multiplier, sSpellName);
    }

    public override void SpellCast(SpellCaster player)
    {
        //Nothing.
    }
}
