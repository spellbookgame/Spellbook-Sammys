using Bolt.Samples.Photon.Lobby;
using System.Collections.Generic;
using UnityEngine;

// spell for Elemental class
public class Growth : Spell, IAllyCastable
{
    public Growth()
    {
        iTier = 3;
        iManaCost = 700;

        combatSpell = false;

        sSpellName = "Growth";
        sSpellClass = "Elementalist";
        sSpellInfo = "Everyone will receive an extra D7 to their mana next turn.";

        requiredRunes.Add("Elementalist C Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            //PanelHolder.instance.displayNotify(sSpellName, "Everyone will receive a D7 to their mana next time they roll.", "MainPlayerScene");
            //player.activeSpells.Add(this);
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

            //PanelHolder.instance.displayNotify(sSpellName, "Everyone will receive a D7 to their mana next time they roll.", "MainPlayerScene");
            //player.activeSpells.Add(this);
            NetworkManager.s_Singleton.CastOnAlly(player.spellcasterID, 8, sSpellName);
            player.numSpellsCastThisTurn++;
            SpellTracker.instance.lastSpellCasted = this;
        }
    }

    public void RecieveCastFromAlly(SpellCaster player)
    {
        PanelHolder.instance.displaySpellCastNotif(sSpellName, "Everyone will receive a D7 to their mana next time they roll.", "MainPlayerScene");
        player.activeSpells.Add(this);
    }

    public void SpellcastPhase2(int sID, SpellCaster player)
    {
        //Will not implement.
    }
}
