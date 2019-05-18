using Bolt.Samples.Photon.Lobby;
using System.Collections.Generic;
using UnityEngine;

// spell for Alchemy class
public class PotionofLuck : Spell, IAllyCastable
{
    SpellCaster player;
    public PotionofLuck()
    {
        iTier = 1;
        iManaCost = 3000;

        combatSpell = false;

        sSpellName = "Potion of Luck";
        sSpellClass = "Alchemist";
        sSpellInfo = "Give you and an ally an extra D9 next time you roll.";

        requiredRunes.Add("Alchemist A Rune", 1);
        requiredRunes.Add("Alchemist B Rune", 1);
        requiredRunes.Add("Elementalist A Rune", 1);
    }

    public void RecieveCastFromAlly(SpellCaster player)
    {
        PanelHolder.instance.displayNotify(sSpellName, "You and your ally will have an extra D9 next time you roll.", "MainPlayerScene");
        player.tempDice.Add("D9", 1);
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
            if (player.spellcasterID != sID)
            {
                 PanelHolder.instance.displayNotify(sSpellName, "You and your ally will have an extra D9 next time you roll.", "MainPlayerScene");
            }
           
            player.tempDice.Add("D9", 1);
            NetworkManager.s_Singleton.CastOnAlly(player.spellcasterID, sID, sSpellName);


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

            if (player.spellcasterID != sID)
            {
                 PanelHolder.instance.displayNotify(sSpellName, "You and your ally will have an extra D9 next time you roll.", "MainPlayerScene");
            }
            player.tempDice.Add("D9", 1);
            NetworkManager.s_Singleton.CastOnAlly(player.spellcasterID, sID, sSpellName);

            player.numSpellsCastThisTurn++;
            SpellTracker.instance.lastSpellCasted = this;
        }
    }
}
