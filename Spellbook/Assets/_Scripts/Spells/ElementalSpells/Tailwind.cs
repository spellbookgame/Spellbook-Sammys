//using Bolt.Samples.Photon.Lobby;
using Bolt.Samples.Photon.Lobby;
using System.Collections.Generic;
using UnityEngine;

// spell for Elemental class
public class Tailwind : Spell, IAllyCastable
{
    SpellCaster player;
    public Tailwind()
    {
        iTier = 1;
        iManaCost = 2500;

        combatSpell = false;

        sSpellName = "Tailwind";
        sSpellClass = "Elementalist";
        sSpellInfo = "Everyone will receive an extra D6 to their movement next turn.";

        requiredRunes.Add("Elementalist A Rune", 1);
        requiredRunes.Add("Elementalist B Rune", 1);
        requiredRunes.Add("Elementalist C Rune", 1);
    }

    //Edit 5-13-2019: Commented out the displayNotify and activeSpells.Add lines
    //Moved them to RecieveCastFromAlly() so the spellcaster who casted it doesn't add the spell twice
    //TODO: Delete old code after testing.
    public override void SpellCast(SpellCaster player)
    {
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            //PanelHolder.instance.displayNotify(sSpellName, "Everyone will receive a D6 to their movement next time they roll.", "MainPlayerScene");
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

            //PanelHolder.instance.displayNotify(sSpellName, "Everyone will receive a D6 to their movement next time they roll.", "MainPlayerScene");
            //player.activeSpells.Add(this);
            NetworkManager.s_Singleton.CastOnAlly(player.spellcasterID, 8, sSpellName);
            player.numSpellsCastThisTurn++;
            SpellTracker.instance.lastSpellCasted = this;
        }
        
    }

    public void RecieveCastFromAlly(SpellCaster player)
    {
        PanelHolder.instance.displayNotify(sSpellName, "Everyone will receive a D6 to their movement next time they roll.", "MainPlayerScene");
        player.activeSpells.Add(this);
    }

    public void SpellcastPhase2(int sID)
    {
       //Will not implemented cause this spell targets everyone.
    }
}
