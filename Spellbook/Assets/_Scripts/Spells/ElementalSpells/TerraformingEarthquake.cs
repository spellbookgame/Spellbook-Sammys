using Bolt.Samples.Photon.Lobby;
using System.Collections.Generic;
using UnityEngine;

// spell for Elemental class
public class TerraformingEarthquake : Spell, IAllyCastable
{
    SpellCaster player;
    public TerraformingEarthquake()
    {
        iTier = 2;
        iManaCost = 1300;

        combatSpell = false;

        sSpellName = "Terraforming Earthquake";
        sSpellClass = "Elementalist";
        sSpellInfo = "Move to the closest location to you. If you are already on a location, choose an adjacent one to move to. Can cast on an ally.";

        requiredRunes.Add("Elementalist B Rune", 1);
        requiredRunes.Add("Arcanist B Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        this.player = player;
        PanelHolder.instance.displayChooseSpellcaster(this);
    }

    public void RecieveCastFromAlly(SpellCaster player)
    {
        PanelHolder.instance.displayNotify(sSpellName, "Move to the closest location to you. If you are already on a location, choose an adjacent one to move to.", "VuforiaScene");
        player.locationItemUsed = true;     // allows player to scan location without rolling
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
                PanelHolder.instance.displayNotify(sSpellName, "Move to the closest location to you. If you are already on a location, choose an adjacent one to move to.", "VuforiaScene");
                player.locationItemUsed = true;
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
                PanelHolder.instance.displayNotify(sSpellName, "Move to the closest location to you. If you are already on a location, choose an adjacent one to move to.", "VuforiaScene");
                player.locationItemUsed = true;
            }

            player.numSpellsCastThisTurn++;
            SpellTracker.instance.lastSpellCasted = this;
        }
    }
}
