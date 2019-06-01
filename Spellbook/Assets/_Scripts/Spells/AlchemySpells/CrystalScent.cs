using Bolt.Samples.Photon.Lobby;
using System.Collections.Generic;
using UnityEngine;

// spell for Alchemy class
public class CrystalScent : Spell, IAllyCastable
{
    SpellCaster player;
    public CrystalScent()
    {
        iTier = 3;
        iManaCost = 600;

        combatSpell = false;

        sSpellName = "Crystal Scent Potion";
        sSpellClass = "Alchemist";
        sSpellInfo = "Teleport to the Marketplace. Can cast on an ally.";

        requiredRunes.Add("Alchemist D Rune", 1);
    }

    public void RecieveCastFromAlly(SpellCaster player)
    {
        PanelHolder.instance.displaySpellCastNotif(sSpellName, "Move your piece to the Marketplace.", "ShopScene");
    }

    public override void SpellCast(SpellCaster player)
    {
        this.player = player;
        PanelHolder.instance.displayChooseSpellcaster(this);
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
                PanelHolder.instance.displaySpellCastNotif(sSpellName, "Move your piece to the Marketplace.", "ShopScene");
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
            // subtract mana and glyph costs
            player.iMana -= iManaCost;

            if (sID != player.spellcasterID)
            {
                NetworkManager.s_Singleton.CastOnAlly(player.spellcasterID, sID, sSpellName);
            }
            else
            {
                PanelHolder.instance.displaySpellCastNotif(sSpellName, "Move your piece to the Marketplace.", "ShopScene");
            }

            player.numSpellsCastThisTurn++;
            SpellTracker.instance.lastSpellCasted = this;
        }
    }
}
