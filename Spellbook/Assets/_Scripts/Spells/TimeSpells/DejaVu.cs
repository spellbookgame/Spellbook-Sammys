using Bolt.Samples.Photon.Lobby;
using System.Collections.Generic;
using UnityEngine;

// spell for Chronomancy class
public class DejaVu : Spell, IAllyCastable
{
    SpellCaster player;
    public DejaVu()
    {
        iTier = 1;
        iManaCost = 2800;

        combatSpell = false;

        sSpellName = "Deja Vu";
        sSpellClass = "Chronomancer";
        sSpellInfo = "Repeat the spell you last cast. Can cast on an ally.";

        requiredRunes.Add("Chronomancer A Rune", 1);
        requiredRunes.Add("Arcanist A Rune", 1);
        requiredRunes.Add("Illusionist B Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        this.player = player;
        PanelHolder.instance.displayChooseSpellcaster(this);
    }

    public void RecieveCastFromAlly(SpellCaster player)
    {
        Spell newSpell = SpellTracker.instance.lastSpellCasted;
        player.CollectMana(newSpell.iManaCost);
        newSpell.SpellCast(player);
        player.numSpellsCastThisTurn++;
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
                Spell newSpell = SpellTracker.instance.lastSpellCasted;
                player.CollectMana(newSpell.iManaCost);
                newSpell.SpellCast(player);
            }

            player.numSpellsCastThisTurn++;
        }
        else if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("Not enough Mana!", "You don't have enough mana to cast this spell.", "OK");
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
                Spell newSpell = SpellTracker.instance.lastSpellCasted;
                player.CollectMana(newSpell.iManaCost);
                newSpell.SpellCast(player);
            }

            player.numSpellsCastThisTurn++;
        }
    }
}
