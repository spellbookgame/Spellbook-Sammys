using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// spell for all clases
public class DarkRevelation : Spell
{
    public DarkRevelation()
    {
        iTier = 1;
        iManaCost = 3400;

        combatSpell = false;

        sSpellName = "Dark Revelation";
        sSpellClass = "";
        sSpellInfo = "Create a random spell you do not own.";
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

            Random rnd = new Random();
            List<Spell> spells = new List<Spell>();
            foreach(Spell s in player.chapter.spellsAllowed)
            {
                // if spell isn't in player's collect spells list, add it to list of spells 
                if(!player.chapter.spellsCollected.Any(x => x.sSpellName.Equals(s.sSpellName)))
                {
                    spells.Add(s);
                }
            }

            player.CollectSpell(spells[Random.Range(0, spells.Count)]);

            player.numSpellsCastThisTurn++;
        }
    }
}
