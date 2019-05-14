using UnityEngine;

public class RiftTalisman : ItemObject
{
    public RiftTalisman()
    {
        name = "Rift Talisman";
        sprite = Resources.Load<Sprite>("Art Assets/Items and Currency/Rift Talisman");
        tier = 1;
        buyPrice = 3500;
        sellPrice = 1050;
        flavorDescription = "An impure artifact of dark chronomancy. It contains a time rift, folded inwards on itself. Best be careful with this.";
        mechanicsDescription = "Delays the next crisis by one round.";
    }

    public override void UseItem(SpellCaster player)
    {
        player.RemoveFromInventory(this);

        CrisisHandler.instance.roundsUntilCrisis++;
        PanelHolder.instance.displayNotify("Rift Talisman", "The " + CrisisHandler.instance.currentCrisis + " will be delayed by 1 round.", "InventoryScene");
    }
}
