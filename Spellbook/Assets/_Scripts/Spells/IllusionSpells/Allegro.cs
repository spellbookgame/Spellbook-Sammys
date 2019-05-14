using Bolt.Samples.Photon.Lobby;
using System.Collections.Generic;
using UnityEngine;

// spell for Illusion class
public class Allegro : Spell, IAllyCastable
{
    SpellCaster player;
    public Allegro()
    {
        iTier = 3;
        iManaCost = 600;

        combatSpell = false;

        sSpellName = "Allegro";
        sSpellClass = "Illusionist";
        sSpellInfo = "Add a D6 to your movement next time you roll. Can cast on an ally.";

        requiredRunes.Add("Illusionist D Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        this.player = player;
        PanelHolder.instance.displayChooseSpellcaster(this);
    }

    public void RecieveCastFromAlly(SpellCaster player)
    {
        PanelHolder.instance.displayNotify(sSpellName, "You and your ally will have an additional D6 to your movement next time you roll.", "MainPlayerScene");
        player.activeSpells.Add(this);
    }

    public void SpellcastPhase2(int sID)
    {
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            if (player.spellcasterID != sID)
            {
                //Make sure to add it only once to this player's activespells
                PanelHolder.instance.displayNotify(sSpellName, "You and your ally will have an additional D6 to your movement next time you roll.", "MainPlayerScene");
                player.activeSpells.Add(this);
            }
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
                //Make sure to add it only once to this player's activespells
                PanelHolder.instance.displayNotify(sSpellName, "You and your ally will have an additional D6 to your movement next time you roll.", "MainPlayerScene");
                player.activeSpells.Add(this);
            }
            NetworkManager.s_Singleton.CastOnAlly(player.spellcasterID, sID, sSpellName);

            player.numSpellsCastThisTurn++;
            SpellTracker.instance.lastSpellCasted = this;
        }
    }
}