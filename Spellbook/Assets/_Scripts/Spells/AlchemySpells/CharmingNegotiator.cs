using Bolt.Samples.Photon.Lobby;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// spell for Alchemy class
public class CharmingNegotiator : Spell, IAllyCastable
{
    public CharmingNegotiator()
    {
        iTier = 2;
        iManaCost = 1400;

        combatSpell = false;

        sSpellName = "Potion of Charm";
        sSpellClass = "Alchemist";
        sSpellInfo = "The shopkeeper will give a 50% discount to everyone next time they visit.";

        requiredRunes.Add("Alchemist D Rune", 1);
        requiredRunes.Add("Summoner B Rune", 1);
    }

    public void RecieveCastFromAlly(SpellCaster player)
    {
        PanelHolder.instance.displaySpellCastNotif(sSpellName, "Next time you visit the shop, you will receive 50% discount.", "MainPlayerScene");
        player.activeSpells.Add(this);
    }

    public override void SpellCast(SpellCaster player)
    {
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
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
            // subtract mana and glyph costs
            player.iMana -= iManaCost;

            NetworkManager.s_Singleton.CastOnAlly(player.spellcasterID, 8, sSpellName);

            player.numSpellsCastThisTurn++;
            SpellTracker.instance.lastSpellCasted = this;
        }
    }

    public void SpellcastPhase2(int sID, SpellCaster player)
    {
        //Will not implement.
    }
}
