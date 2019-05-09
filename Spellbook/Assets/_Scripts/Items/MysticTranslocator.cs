using UnityEngine;

public class MysticTranslocator : ItemObject
{
    public MysticTranslocator()
    {
        name = "Mystic Translocator";
        sprite = Resources.Load<Sprite>("Art Assets/Items and Currency/Infused Sapphire");
        tier = 2;
        buyPrice = 2400;
        sellPrice = 720;
        flavorDescription = "A mysterious item with teleportation technology that seems advanced for this day and age. Who knows how it got here?";
        mechanicsDescription = "Allows for the user to swap locations with another wizard.";
    }

    public override void UseItem(SpellCaster player)
    {
        player.RemoveFromInventory(this);
        player.itemsUsedThisTurn++;

        player.locationItemUsed = true;
        PanelHolder.instance.displayNotify("Mystic Translocator", "Swap locations with another wizard (both wizards must be on a location space).", "VuforiaScene");
    }
}
