using System.Collections.Generic;
using UnityEngine;

// spell for Chronomancy class
public class DelayTime : Spell
{
    public DelayTime()
    {
        iTier = 1;
        iManaCost = 3200;

        combatSpell = false;

        sSpellName = "Delay Time";
        sSpellClass = "Chronomancer";
        sSpellInfo = "The next crisis will be delayed for 1 round.";

        requiredRunes.Add("Chronomancer A Rune", 1);
        requiredRunes.Add("Chronomancer B Rune", 1);
        requiredRunes.Add("Arcanist A Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            CrisisHandler.instance.roundsUntilCrisis++;
            PanelHolder.instance.displayNotify(sSpellName, "The next crisis will come 1 turn later.", "OK");
        }
        else if(player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("Not enough Mana!", "You don't have enough mana to cast this spell.", "OK");
        }
        else
        {
            // subtract mana
            player.iMana -= iManaCost;

            CrisisHandler.instance.roundsUntilCrisis++;
            PanelHolder.instance.displayNotify(sSpellName, "The next crisis will come 1 turn later.", "OK");
        }
    }
}
