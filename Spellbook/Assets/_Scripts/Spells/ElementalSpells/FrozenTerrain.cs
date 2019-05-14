using Bolt.Samples.Photon.Lobby;
using System.Collections.Generic;
using UnityEngine;

// spell for Elemental class
public class FrozenTerrain : Spell, IAllyCastable
{
    public FrozenTerrain()
    {
        iTier = 2;
        iManaCost = 950;

        combatSpell = false;

        sSpellName = "Frozen Terrain";
        sSpellClass = "Elementalist";
        sSpellInfo = "Everyone moves to the Forest.";

        requiredRunes.Add("Elementalist B Rune", 1);
        requiredRunes.Add("Illusionist A Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            //PanelHolder.instance.displayNotify(sSpellName, "Everyone move their piece to the Forest.", "ForestScene");
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

            //PanelHolder.instance.displayNotify(sSpellName, "Everyone move their piece to the Forest.", "ForestScene");
            NetworkManager.s_Singleton.CastOnAlly(player.spellcasterID, 8, sSpellName);
            player.numSpellsCastThisTurn++;
            SpellTracker.instance.lastSpellCasted = this;
        }
    }

    public void RecieveCastFromAlly(SpellCaster player)
    {
        PanelHolder.instance.displayNotify(sSpellName, "Everyone move their piece to the Forest.", "ForestScene");
    }

    public void SpellcastPhase2(int sID)
    {
        throw new System.NotImplementedException();
    }
}
