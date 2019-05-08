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
        player.itemsUsedThisTurn++;

        Spell spell = player.chapter.spellsCollected[Random.Range(0, player.chapter.spellsCollected.Count)];

        // give player mana to cast that spell
        player.CollectMana(spell.iManaCost);

        // cast the spell
        spell.SpellCast(player);
    }
}