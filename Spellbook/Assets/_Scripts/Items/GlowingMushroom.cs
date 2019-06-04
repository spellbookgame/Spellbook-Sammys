using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingMushroom : ItemObject
{
    public GlowingMushroom()
    {
        name = "Glowing Mushroom";
        sprite = GameObject.Find("ItemContainer").transform.Find(name).GetComponent<SpriteRenderer>().sprite;
        tier = 3;
        buyPrice = 300;
        sellPrice = 1800;
        flavorDescription = "Ooo, glowy.";
        mechanicsDescription = "Heals you for 2 points.";
    }

    public override void UseItem(SpellCaster player)
    {
        SoundManager.instance.PlaySingle(SoundManager.glowingMushroom);
        player.RemoveFromInventory(this);

        player.HealDamage(2);
        PanelHolder.instance.displayNotify("Glowing Mushroom", "You ate the mushroom and healed 2 health. You feel a little funny, though...", "InventoryScene");
    }
}
