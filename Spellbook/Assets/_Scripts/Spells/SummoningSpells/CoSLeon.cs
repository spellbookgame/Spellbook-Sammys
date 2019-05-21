using Bolt.Samples.Photon.Lobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// spell for Summoner class
public class CoSLeon : Spell, IAllyCastable
{
    public CoSLeon()
    {
        iTier = 1;
        iManaCost = 2200;

        combatSpell = false;

        sSpellName = "Leon's Shining";
        sSpellClass = "Summoner";
        sSpellInfo = "All allies gain a charge on each of their combat spells.";

        requiredRunes.Add("Elementalist A Rune", 1);
        requiredRunes.Add("Summoner A Rune", 1);
        requiredRunes.Add("Chronomancer A Rune", 1);
    }

    //Cast on everyone, even yourself.
    public override void SpellCast(SpellCaster player)
    {
        if (SpellTracker.instance.CheckUmbra())
        {
            //foreach(Spell s in player.chapter.spellsCollected)
            //{
            //    if (s.combatSpell)
            //        ++s.iCharges;
            //}

            //PanelHolder.instance.displayNotify("Leon's Shining", "You gained a charge in all your combat spells!", "MainPlayerScene");
            NetworkManager.s_Singleton.CastOnAlly(player.spellcasterID, 8, sSpellName);
            player.numSpellsCastThisTurn++;
            SpellTracker.instance.lastSpellCasted = this;
        }
        else if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("Not enough Mana!", "You do not have enough mana to cast this spell.", "OK");
        }
        else
        {
            // subtract mana
            player.iMana -= iManaCost;

            //foreach (Spell s in player.chapter.spellsCollected)
            //{
            //    if (s.combatSpell)
            //        ++s.iCharges;
            //}

            //PanelHolder.instance.displayNotify("Leon's Shining", "You gained a charge in all your combat spells!", "MainPlayerScene");
            NetworkManager.s_Singleton.CastOnAlly(player.spellcasterID, 8, sSpellName);
            player.numSpellsCastThisTurn++;
            SpellTracker.instance.lastSpellCasted = this;
        }
    }

    public void RecieveCastFromAlly(SpellCaster player)
    {
        foreach (Spell s in player.chapter.spellsCollected)
        {
            if (s.combatSpell)
                ++s.iCharges;
        }

        PanelHolder.instance.displayNotify("Leon's Shining", "You gained a charge in all your combat spells!", "MainPlayerScene");
    }

    public void SpellcastPhase2(int sID, SpellCaster player)
    {
        throw new System.NotImplementedException();
    }
}
