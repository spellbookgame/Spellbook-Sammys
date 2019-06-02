using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// example spell for Arcanist class
public class CombinedKnowledge : Spell
{
    public CombinedKnowledge()
    {
        iTier = 2;
        iManaCost = 1000;

        combatSpell = false;

        sSpellName = "Combined Knowledge";
        sSpellClass = "Arcanist";
        sSpellInfo = "Each player takes a rune from their respective class towns.";

        requiredRunes.Add("Arcanist B Rune", 1);
        requiredRunes.Add("Summoner B Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            PanelHolder.instance.displaySpellCastNotif(sSpellName, "Everyone take the rune from their respective towns. Swap or discard a rune from hand if needed.", "MainPlayerScene");

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

            PanelHolder.instance.displaySpellCastNotif(sSpellName, "Everyone take the rune from their respective towns. Swap or discard a rune from hand if needed.", "MainPlayerScene");

            player.numSpellsCastThisTurn++;
            SpellTracker.instance.lastSpellCasted = this;
        }
    }
}
