using Bolt.Samples.Photon.Lobby;
using System.Collections.Generic;
using UnityEngine;

// spell for Elemental class
public class Fireball : Spell, ICombatSpell
{
    public Fireball()
    {
        iTier = 3;
        iCharges = 0;
        iManaCost = 600;

        combatSpell = true;
        damageSpell = true;

        sSpellName = "Fireball";
        sSpellClass = "Elementalist";
        sSpellInfo = "Cast 2 fireballs that deal 1-6 damage each.";

        requiredRunes.Add("Elementalist D Rune", 1);

        ColorUtility.TryParseHtmlString("#F74A4A", out colorPrimary);
        ColorUtility.TryParseHtmlString("#FC923C", out colorSecondary);
        ColorUtility.TryParseHtmlString("#FFE43A", out colorTertiary);
    }

    public void CombatCast(SpellCaster player, float orbPercentage)
    {
        orbPercentage = orbPercentage * 100;
        int damage1, damage2;
        if (orbPercentage <= 25)
        {
            damage1 = Random.Range(1, 4);
            damage2 = Random.Range(1, 4);
        }
        else if (orbPercentage > 25 && orbPercentage <= 50)
        {
            damage1 = Random.Range(2, 5);
            damage2 = Random.Range(2, 5);
        }
        else if (orbPercentage > 50 && orbPercentage <= 75)
        {
            damage1 = Random.Range(3, 6);
            damage2 = Random.Range(3, 6);
        }
        else
        {
            damage1 = Random.Range(4, 7);
            damage2 = Random.Range(4, 7);
        }
        int totalDamage = damage1 + damage2;
        damageDealt = totalDamage;
        NetworkManager.s_Singleton.DealDmgToBoss(totalDamage);
    }

    public override void SpellCast(SpellCaster player)
    {
        // nothing
    }
}
