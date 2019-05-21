using Bolt.Samples.Photon.Lobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// spell for Arcanist class
public class RuneConversion : Spell, IAllyCastable
{
    SpellCaster player;
    public RuneConversion()
    {
        iTier = 3;
        iManaCost = 500;

        combatSpell = false;

        sSpellName = "Rune Conversion";
        sSpellClass = "Arcanist";
        sSpellInfo = "Discard one of your current runes to draw one directly from the low-tier deck. Can cast on an ally.";

        requiredRunes.Add("Arcanist A Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        this.player = player;
        PanelHolder.instance.displayChooseSpellcaster(this);
    }
    
    public void RecieveCastFromAlly(SpellCaster player)
    {
         PanelHolder.instance.displayNotify(sSpellName, "Discard one of your current runes. Draw a new one from the low-tier deck.", "MainPlayerScene");
    }

    public void SpellcastPhase2(int sID, SpellCaster player)
    {
        this.player = player;

        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            if(sID != player.spellcasterID)
            {
                NetworkManager.s_Singleton.CastOnAlly(player.spellcasterID, sID, sSpellName);
            }
            else { 
                PanelHolder.instance.displayNotify(sSpellName, "Discard one of your current runes. Draw a new one from the low-tier deck.", "MainPlayerScene");
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

            if(sID != player.spellcasterID)
            {
                NetworkManager.s_Singleton.CastOnAlly(player.spellcasterID, sID, sSpellName);
            }
            else { 
                PanelHolder.instance.displayNotify(sSpellName, "Discard one of your current runes. Draw a new one from the low-tier deck.", "MainPlayerScene");
            }
            player.numSpellsCastThisTurn++;
            SpellTracker.instance.lastSpellCasted = this;
        }
    }
}
