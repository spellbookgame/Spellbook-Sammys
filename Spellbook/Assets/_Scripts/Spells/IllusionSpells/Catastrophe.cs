using Bolt.Samples.Photon.Lobby;
using System.Collections.Generic;
using UnityEngine;

public class Catastrophe : Spell, ICombatSpell, IAllyCastable
{
    float OrbPercent;
    SpellCaster player;

    public Catastrophe()
    {
        iTier = 2;
        iCharges = 0;
        iManaCost = 1500;

        combatSpell = true;

        sSpellName = "Catastrophe";
        sSpellClass = "Illusionist";
        sSpellInfo = "Create an illusionary puppet that will increase an ally's damage output by 20% this round.";

        requiredRunes.Add("Illusionist B Rune", 1);
        requiredRunes.Add("Arcanist B Rune", 1);

        ColorUtility.TryParseHtmlString("#005C69", out colorPrimary);
        ColorUtility.TryParseHtmlString("#950952", out colorSecondary);
        ColorUtility.TryParseHtmlString("#023618", out colorTertiary);

        guideLine = Resources.Load<Sprite>("CombatSwipes/Catastrophe");
    }

    public void CombatCast(SpellCaster player, float orbPercentage)
    {
        this.player = player;
        this.OrbPercent = orbPercentage;
        PanelHolder.instance.displayChooseSpellcaster(this);
    }

    public void RecieveCastFromAlly(SpellCaster player)
    {
        PanelHolder.instance.displaySpellCastNotif(sSpellName, "Your damage output is increased by 20% this round", "OK");
    }


    public override void SpellCast(SpellCaster player)
    {
        //Nothing.
    }

    public void SpellcastPhase2(int sID, SpellCaster player)
    {
        OrbPercent = OrbPercent * 100;
        // for every 20% the orb is filled, increase the multiplier by 5%
        float multiplier = ((Mathf.Floor(OrbPercent/ 20) * 5) + 20) / 100;
        // player.totalDamage += player.totalDamge * multiplier;
        NetworkManager.s_Singleton.CastOnAlly(player.spellcasterID, sID, sSpellName); 
        NetworkManager.s_Singleton.IncreaseAllyDamageByPercent(sID, multiplier);
    }
}
