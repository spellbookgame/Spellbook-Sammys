using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingMushroom : ItemObject
{
    public GlowingMushroom()
    {
        name = "Glowing Mushroom";
        sprite = Resources.Load<Sprite>("Art Assets/Items and Currency/GlowingMushroom");
        tier = 3;
        buyPrice = 300;
        sellPrice = 1800;
        flavorDescription = "Ooo, glowy.";
        mechanicsDescription = "Can be sold at a high price to shops.";
    }

    public override void UseItem(SpellCaster player)
    {
        PanelHolder.instance.displayNotify("Glowing Mushroom", "This item cannot be used.", "OK");
    }
}
