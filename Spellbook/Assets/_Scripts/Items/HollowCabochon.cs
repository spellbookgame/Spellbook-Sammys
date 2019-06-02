using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollowCabochon : ItemObject
{
    public HollowCabochon()
    {
        name = "Hollow Cabochon";
        sprite = GameObject.Find("ItemContainer").transform.Find(name).GetComponent<SpriteRenderer>().sprite;
        tier = 2;
        buyPrice = 2100;
        sellPrice = 630;
        flavorDescription = "While not truly hollow in the physical sense, this glassy jewel seems to be infused with an essence of want.";
        mechanicsDescription = "A random spell (excluding combat spells) from the user's collection will be stored in the Hollow Cabochon, turning it into a Glimmering Cabochon infused with that spell.";
    }

    public override void UseItem(SpellCaster player)
    {
        if(player.chapter.spellsCollected.Count <= 0)
        {
            PanelHolder.instance.displayNotify("No Spells Collected", "You do not have any spells that can be stored in the cabochon.", "OK");
        }
        else
        {
            // check to see if they have at least 1 non combat spell to put in
            bool hasNonCombatSpell = false;
            foreach (Spell spell in player.chapter.spellsCollected)
            {
                if (!spell.combatSpell)
                {
                    hasNonCombatSpell = true;
                    break;
                }
            }

            if (!hasNonCombatSpell)
                PanelHolder.instance.displayNotify("No Spells Collected", "You do not have any spells that can be stored in the cabochon.", "OK");
            else
            {
                SoundManager.instance.PlaySingle(SoundManager.hollowCabochon);
                player.RemoveFromInventory(this);

                player.AddToInventory(new GlimmeringCabochon());
                PanelHolder.instance.displayNotify("Hollow Cabochon", "Your Hollow Cabochon has turned into a Glimmering Cabochon!", "InventoryScene");
            }
        }
    }
}