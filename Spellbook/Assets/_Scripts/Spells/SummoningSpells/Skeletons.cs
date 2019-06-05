using Bolt.Samples.Photon.Lobby;
using System.Collections.Generic;
using UnityEngine;

public class Skeletons : Spell, ICombatSpell, IAllyCastable
{
    float OrbPercent;
    SpellCaster player;

    public Skeletons()
    {
        iTier = 3;
        iCharges = 0;
        iManaCost = 900;

        combatSpell = true;

        sSpellName = "Skeletons";
        sSpellClass = "Summoner";
        sSpellInfo = "Summon a skeleton that increases an ally's damage output by 10% this round.";

        requiredRunes.Add("Summoner D Rune", 1);

        ColorUtility.TryParseHtmlString("#F2F5EA", out colorPrimary);
        ColorUtility.TryParseHtmlString("#2C363F", out colorSecondary);
        ColorUtility.TryParseHtmlString("#E75A7C", out colorTertiary);
    }

    public void CombatCast(SpellCaster player, float orbPercentage)
    {
        this.player = player;
        this.OrbPercent = orbPercentage;
        PanelHolder.instance.displayChooseSpellcaster(this);
    }


    public void RecieveCastFromAlly(SpellCaster player)
    {
       PanelHolder.instance.displaySpellCastNotif(sSpellName, "Your damage output is increased by 10% this round", "OK");
    }

    public override void SpellCast(SpellCaster player)
    {
        //Nothing.
    }

    public void SpellcastPhase2(int sID, SpellCaster player)
    {
        OrbPercent = OrbPercent * 100;
        //for every 20% the orb is filled, add 5% to the multiplier
        float multiplier = ((Mathf.Floor(OrbPercent / 20) * 5) + 10) / 100;
        // player.totalDamage += player.totalDamage * multiplier;
        NetworkManager.s_Singleton.CastOnAlly(player.spellcasterID, sID, sSpellName); 
        NetworkManager.s_Singleton.IncreaseAllyDamageByPercent(sID, multiplier);
    }
}
