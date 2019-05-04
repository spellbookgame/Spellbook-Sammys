using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfusedSapphire : ItemObject
{
    public InfusedSapphire()
    {
        name = "Infused Sapphire";
        sprite = Resources.Load<Sprite>("Art Assets/Items and Currency/InfusedSapphire");
        tier = 2;
        buyPrice = 1400;
        sellPrice = 420;
        flavorDescription = "This sapphire is embued with pure arcane energy.";
        mechanicsDescription = "When shattered, teleports the user to a random location.";
    }

    public override void UseItem(SpellCaster player)
    {
        player.RemoveFromInventory(this);
        PanelHolder.instance.displayNotify("Infused Sapphire", "The Infused Sapphire teleported you to the Forest!", "Inventory");
    }
}
