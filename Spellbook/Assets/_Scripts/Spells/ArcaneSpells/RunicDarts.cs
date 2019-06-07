using Bolt.Samples.Photon.Lobby;
using System.Collections.Generic;
using UnityEngine;

public class RunicDarts : Spell, ICombatSpell
{
    public RunicDarts()
    {
        iTier = 2;
        iCharges = 0;
        iManaCost = 1200;

        combatSpell = true;
        damageSpell = true;

        sSpellName = "Runic Darts";
        sSpellClass = "Arcanist";
        sSpellInfo = "Cast 3 darts forged from the magic of your runes to deal 2-5 damage each.";

        requiredRunes.Add("Arcanist B Rune", 1);
        requiredRunes.Add("Chronomancer B Rune", 1);

        ColorUtility.TryParseHtmlString("#274690", out colorPrimary);
        ColorUtility.TryParseHtmlString("#302B27", out colorSecondary);
        ColorUtility.TryParseHtmlString("#F5F3F5", out colorTertiary);
    }

    public void CombatCast(SpellCaster player, float orbPercentage)
    {
        orbPercentage = orbPercentage * 100f; 
        int damage1, damage2, damage3;
        if(orbPercentage <= 25)
        {
            damage1 = Random.Range(2, 4);
            damage2 = Random.Range(2, 4);
            damage3 = Random.Range(2, 4);
        }
        else if(orbPercentage > 25 && orbPercentage <= 50)
        {
            damage1 = Random.Range(2, 5);
            damage2 = Random.Range(2, 5);
            damage3 = Random.Range(2, 5);
        }
        else if(orbPercentage > 50 && orbPercentage <= 75)
        {
            damage1 = Random.Range(3, 6);
            damage2 = Random.Range(3, 6);
            damage3 = Random.Range(3, 6);
        }
        else
        {
            damage1 = Random.Range(4, 6);
            damage2 = Random.Range(4, 6);
            damage3 = Random.Range(4, 6);
        }
        int totalDamage = damage1 + damage2 + damage3;
        damageDealt = totalDamage;
        NetworkManager.s_Singleton.DealDmgToBoss((float) totalDamage);
    }

    public override void SpellCast(SpellCaster player)
    {
        //Nothing.
    }
    
}
