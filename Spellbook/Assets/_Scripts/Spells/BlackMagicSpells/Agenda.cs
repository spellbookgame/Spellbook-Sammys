using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// spell for all clases
public class Agenda : Spell
{
    public Agenda()
    {
        iTier = 1;
        iManaCost = 1250;

        combatSpell = false;
        blackMagicSpell = true;

        sSpellName = "Agenda";
        sSpellClass = "";
        sSpellInfo = "Can cast an unlimited amount of spells this turn.";
    }

    public override void SpellCast(SpellCaster player)
    {
        if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("Not enough Mana!", "You do not have enough mana to cast this spell.", "OK");
        }
        else
        {
            // subtract mana and glyph costs
            player.iMana -= iManaCost;

            SpellTracker.instance.agendaActive = true;  // this is reset in EndTurnClick.cs
            PanelHolder.instance.displayNotify("Agenda", "You will be able to cast an unlimited amount of spells this turn. " +
                                                "Agenda disappeared from your memory without a trace...", "MainPlayerScene");

            // remove this spell from castable spells once it's cast
            player.chapter.spellsCollected.Remove(this);
            player.numSpellsCastThisTurn++;
        }
    }
}
