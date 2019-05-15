using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlimmeringCabochon : ItemObject
{
    public GlimmeringCabochon()
    {
        name = "Glimmering Cabochon";
        sprite = Resources.Load<Sprite>("Art Assets/Items and Currency/Glimmering Cabochon");
        tier = 2;
        buyPrice = 0;
        sellPrice = 0;
        flavorDescription = "Magical fire burns just beneath the surface of this peculiar jewel.";
        mechanicsDescription = "Casts the spell infused into the cabochon for free.";
    }

    public override void UseItem(SpellCaster player)
    {
        player.RemoveFromInventory(this);

        List<Spell> spells = new List<Spell>();
        // only include spell if it's non-combat
        foreach(Spell s in player.chapter.spellsCollected)
        {
            if (!s.combatSpell)
                spells.Add(s);
        }

        Spell spell = spells[Random.Range(0, spells.Count)];
        IAllyCastable spellToCast = (IAllyCastable) spell;

        // give player mana to cast that spell
        player.CollectMana(spell.iManaCost);

        // cast the spell
        spellToCast.SpellcastPhase2(player.spellcasterID, player);
    }
}