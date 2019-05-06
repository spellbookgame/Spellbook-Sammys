using System.Collections.Generic;
using UnityEngine;

// spell for Alchemy class
public class PotionofLuck : Spell
{
    public PotionofLuck()
    {
        iTier = 1;
        iManaCost = 3000;

        combatSpell = false;

        sSpellName = "Brew - Potion of Luck";
        sSpellClass = "Alchemist";
        sSpellInfo = "Give you and an ally an extra D8 next time you roll.";

        requiredRunes.Add("Alchemist A Rune", 1);
        requiredRunes.Add("Alchemist B Rune", 1);
        requiredRunes.Add("Elementalist A Rune", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            PanelHolder.instance.displayNotify(sSpellName, "You and your ally will have an extra D8 next time you roll.", "OK");
            player.tempDice.Add("D8", 1);
        }
        else if (player.iMana < iManaCost)
        {
            PanelHolder.instance.displayNotify("Not enough Mana!", "You do not have enough mana to cast this spell.", "OK");
        }
        else
        {
            // subtract mana
            player.iMana -= iManaCost;

            PanelHolder.instance.displayNotify(sSpellName, "You and your ally will have an extra D8 next time you roll.", "OK");
            player.tempDice.Add("D8", 1);
        }
    }
}
