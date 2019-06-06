using Bolt.Samples.Photon.Lobby;
using System.Collections.Generic;
using UnityEngine;

public class Tragedy : Spell, ICombatSpell
{
    public Tragedy()
    {
        iTier = 3;
        iCharges = 0;
        iManaCost = 800;

        combatSpell = true;

        sSpellName = "Tragedy";
        sSpellClass = "Illusionist";
        sSpellInfo = "Create an illusionary puppet that deals 5 - 9 damage to the enemy.";

        requiredRunes.Add("Illusionist C Rune", 1);

        ColorUtility.TryParseHtmlString("#9759C6", out colorPrimary);
        ColorUtility.TryParseHtmlString("#CC3F3F", out colorSecondary);
        ColorUtility.TryParseHtmlString("#222A68", out colorTertiary);
    }

    public void CombatCast(SpellCaster player, float orbPercentage)
    {
        orbPercentage = orbPercentage * 100;
        int damage;
        if (orbPercentage <= 25)
            damage = Random.Range(5, 7);
        else if (orbPercentage > 25 && orbPercentage <= 50)
            damage = Random.Range(5, 8);
        else if (orbPercentage > 50 && orbPercentage <= 75)
            damage = Random.Range(6, 9);
        else
            damage = Random.Range(7, 10);
        NetworkManager.s_Singleton.DealDmgToBoss(damage);
    }

    public override void SpellCast(SpellCaster player)
    {
        //Nothing.
    }
    
}
