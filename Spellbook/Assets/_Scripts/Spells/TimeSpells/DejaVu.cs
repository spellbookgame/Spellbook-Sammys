using System.Collections.Generic;
using UnityEngine;

// spell for Chronomancy class
public class DejaVu : Spell, IAllyCastable
{
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
        // cast spell for free if Umbra's Eclipse is active
        if (SpellTracker.instance.CheckUmbra())
        {
            Spell newSpell = SpellTracker.instance.lastSpellCasted;
            player.CollectMana(newSpell.iManaCost);
            newSpell.SpellCast(player);

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

            Spell newSpell = SpellTracker.instance.lastSpellCasted;
            player.CollectMana(newSpell.iManaCost);
            newSpell.SpellCast(player);

            player.numSpellsCastThisTurn++;
        }
    }

    public void RecieveCastFromAlly(SpellCaster player)
    {
        Spell newSpell = SpellTracker.instance.lastSpellCasted;
        player.CollectMana(newSpell.iManaCost);
        newSpell.SpellCast(player);
        player.numSpellsCastThisTurn++;
    }
}
