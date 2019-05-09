using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// spell for all clases
public class SpacialWarp : Spell
{
    public SpacialWarp()
    {
        iTier = 1;
        iManaCost = 800;

        combatSpell = false;
        blackMagicSpell = true;

        sSpellName = "Spacial Warp";
        sSpellClass = "";
        sSpellInfo = "Teleport to any location on the map.";
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

            // allows them to scan location without having to move
            player.locationItemUsed = true;
            PanelHolder.instance.displayNotify("Spacial Warp", "Teleport to any location on the map. " +
                                                "Spacial Warp disappeared from your memory without a trace...", "VuforiaScene");

            // remove this spell from castable spells once it's cast
            player.chapter.spellsCollected.Remove(this);
            player.numSpellsCastThisTurn++;
        }
    }
}
