using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// spell for all clases
public class Fortune : Spell
{
    public Fortune()
    {
        iTier = 1;
        iManaCost = 2850;

        combatSpell = false;
        blackMagicSpell = true;

        sSpellName = "Fortune";
        sSpellClass = "";
        sSpellInfo = "Create a permanent die ranging from D5 - D9.";
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

            List<string> dice = new List<string>()
            {
                "D5",
                "D6",
                "D7",
                "D8",
                "D9"
            };

            string die = dice[Random.Range(0, dice.Count)];
            player.dice[die]++;
            PanelHolder.instance.displayNotify("Fortune", "You created a " + die + "! Fortune disappeared from your memory without a trace...", "MainPlayerScene");

            // remove this spell from castable spells once it's cast
            player.chapter.spellsCollected.Remove(this);
            player.numSpellsCastThisTurn++;
        }
    }
}
