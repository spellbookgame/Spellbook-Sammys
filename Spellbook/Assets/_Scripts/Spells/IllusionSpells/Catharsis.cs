using Bolt.Samples.Photon.Lobby;
using System.Collections.Generic;
using UnityEngine;

public class Catharsis : Spell, ICombatSpell
{
    public Catharsis()
    {
        iTier = 2;
        iCharges = 0;
        iManaCost = 1300;

        combatSpell = true;

        sSpellName = "Catharsis";
        sSpellClass = "Illusionist";
        sSpellInfo = "Heal all allies by 4 - 8 points each.";

        requiredRunes.Add("Illusionist B Rune", 1);
        requiredRunes.Add("Alchemist A Rune", 1);

        ColorUtility.TryParseHtmlString("#9DDBAD", out colorPrimary);
        ColorUtility.TryParseHtmlString("#FFC145", out colorSecondary);
        ColorUtility.TryParseHtmlString("#FFFFFB", out colorTertiary);

        guideLine = Resources.Load<Sprite>("CombatSwipes/Catharsis");
    }

    public void CombatCast(SpellCaster player, float orbPercentage)
    {
        orbPercentage = orbPercentage * 100;
        int healAmount;
        if (orbPercentage <= 25)
            healAmount = Random.Range(4, 7);
        else if (orbPercentage > 25 && orbPercentage <= 50)
            healAmount = Random.Range(5, 7);
        else if (orbPercentage > 50 && orbPercentage <= 75)
            healAmount = Random.Range(6, 8);
        else
            healAmount = Random.Range(6, 9);
        //player.HealDamage(healAmount);
        NetworkManager.s_Singleton.HealAllAlliesByHp(healAmount);
    }

    public override void SpellCast(SpellCaster player)
    {
        //Nothing.
    }
    
}
