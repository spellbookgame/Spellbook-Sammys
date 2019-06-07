using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlimmeringCabochon : ItemObject
{
    public GlimmeringCabochon()
    {
        name = "Glimmering Cabochon";
        sprite = GameObject.Find("ItemContainer").transform.Find(name).GetComponent<SpriteRenderer>().sprite;
        tier = 2;
        buyPrice = 0;
        sellPrice = 0;
        flavorDescription = "Magical fire burns just beneath the surface of this peculiar jewel.";
        mechanicsDescription = "Casts the spell infused into the cabochon for free (excluding allies).";
    }

    public override void UseItem(SpellCaster player)
    {
        SoundManager.instance.PlaySingle(SoundManager.glimmeringCabochon);
        player.RemoveFromInventory(this);

        List<Spell> spells = new List<Spell>();
        // only include spell if it's non-combat
        foreach(Spell s in player.chapter.spellsCollected)
        {
            if (!s.combatSpell)
                if(!s.sSpellName.Equals("Deja Vu"))     // don't allow cabochon to cast Deja vu (too complicated)
                    spells.Add(s);
        }

        Spell spell = spells[Random.Range(0, spells.Count)];

        if(spell is IAllyCastable)
        {
            IAllyCastable spellToCast = (IAllyCastable)spell;

            // cast the spell
            spellToCast.RecieveCastFromAlly(player);
        }
        else
        {
            player.CollectMana(spell.iManaCost);
            spell.SpellCast(player);
        }
        
    }
}