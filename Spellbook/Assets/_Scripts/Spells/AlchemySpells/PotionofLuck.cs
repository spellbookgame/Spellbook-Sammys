using System.Collections.Generic;
using UnityEngine;

// spell for Alchemy class
public class PotionofLuck : Spell
{
    public PotionofLuck()
    {
        iTier = 1;
        iManaCost = 3000;

        sSpellName = "Brew - Potion of Luck";
        sSpellClass = "Alchemist";
        sSpellInfo = "Give you and an ally an extra D8 next time you roll.";

        requiredGlyphs.Add("Alchemy A Glyph", 1);
        requiredGlyphs.Add("Alchemy B Glyph", 1);
        requiredGlyphs.Add("Elemental A Glyph", 1);
    }

    public override void SpellCast(SpellCaster player)
    {
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            SpellTracker.instance.RemoveFromActiveSpells("Call of the Moon - Umbra's Eclipse");

            PanelHolder.instance.displayNotify(sSpellName, "You and your ally will have an extra D8 next time you roll.", "OK");
            player.dice["D8"] += 1;
            player.activeSpells.Add(this);
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
            player.dice["D8"] += 1;
            player.activeSpells.Add(this);
        }
    }
}
