using Bolt.Samples.Photon.Lobby;
using System.Collections.Generic;
using UnityEngine;

// spell for Chronomancy class
public class DelayTime : Spell, IAllyCastable
{
    public DelayTime()
    {
        iTier = 1;
        iManaCost = 3800;

        combatSpell = false;

        sSpellName = "Delay Time";
        sSpellClass = "Chronomancer";
        sSpellInfo = "The next crisis will be delayed for 1 round.";

        requiredRunes.Add("Chronomancer A Rune", 1);
        requiredRunes.Add("Chronomancer B Rune", 1);
        requiredRunes.Add("Arcanist A Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            //CrisisHandler.instance.roundsUntilCrisis++;
            //PanelHolder.instance.displayNotify(sSpellName, "The next crisis will come 1 turn later.", "MainPlayerScene");
            NetworkManager.s_Singleton.CastOnAlly(player.spellcasterID, 8, sSpellName);
            player.numSpellsCastThisTurn++;
            SpellTracker.instance.lastSpellCasted = this;
        }
        else if(player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("Not enough Mana!", "You don't have enough mana to cast this spell.", "OK");
        }
        else
        {
            // subtract mana
            player.iMana -= iManaCost;

            //CrisisHandler.instance.roundsUntilCrisis++;
            //PanelHolder.instance.displayNotify(sSpellName, "The next crisis will come 1 turn later.", "MainPlayerScene");
            NetworkManager.s_Singleton.CastOnAlly(player.spellcasterID, 8, sSpellName);
            player.numSpellsCastThisTurn++;
            SpellTracker.instance.lastSpellCasted = this;
        }
    }

    public void RecieveCastFromAlly(SpellCaster player)
    {
        CrisisHandler.instance.roundsUntilCrisis++;
        // NetworkManager.s_Singleton.ModifyRoundsUntilNextCrisis(1);
        PanelHolder.instance.displayNotify(sSpellName, "The next crisis will come 1 turn later.", "MainPlayerScene");
    }

    public void SpellcastPhase2(int sID, SpellCaster player)
    {
        throw new System.NotImplementedException();
    }
}
