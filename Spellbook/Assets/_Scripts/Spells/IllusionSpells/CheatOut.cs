using Bolt.Samples.Photon.Lobby;
using System.Collections.Generic;
using UnityEngine;

// spell for Illusion class
public class CheatOut : Spell, IAllyCastable
{
    SpellCaster player;
    public CheatOut()
    {
        iTier = 1;
        iManaCost = 3100;

        combatSpell = false;

        sSpellName = "Cheat Out";
        sSpellClass = "Illusionist";
        sSpellInfo = "Replace two of your runes with high tier runes drawn from the deck. Can cast on an ally.";

        requiredRunes.Add("Illusionist A Rune", 1);
        requiredRunes.Add("Chronomancer A Rune", 1);
        requiredRunes.Add("Alchemist B Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        this.player = player;
        PanelHolder.instance.displayChooseSpellcaster(this);
    }

    public void RecieveCastFromAlly(SpellCaster player)
    {
        PanelHolder.instance.displaySpellCastNotif(sSpellName, "Discard two of your runes. Draw 2 from the high tier rune deck.", "MainPlayerScene");
    }

    public void SpellcastPhase2(int sID, SpellCaster player)
    {
        this.player = player;
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            if (sID != player.spellcasterID)
            {
                NetworkManager.s_Singleton.CastOnAlly(player.spellcasterID, sID, sSpellName);
            }
            else
            {
                PanelHolder.instance.displaySpellCastNotif(sSpellName, "Discard two of your runes. Draw 2 from the high tier rune deck.", "MainPlayerScene");
            }

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

            if (sID != player.spellcasterID)
            {
                NetworkManager.s_Singleton.CastOnAlly(player.spellcasterID, sID, sSpellName);
            }
            else
            {
                PanelHolder.instance.displaySpellCastNotif(sSpellName, "Discard two of your runes. Draw 2 from the high tier rune deck.", "MainPlayerScene");
            }

            player.numSpellsCastThisTurn++;
            SpellTracker.instance.lastSpellCasted = this;
        }
    }
}