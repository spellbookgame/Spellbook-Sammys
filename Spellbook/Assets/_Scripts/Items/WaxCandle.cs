using UnityEngine;

public class WaxCandle : ItemObject
{
    public WaxCandle()
    {
        name = "Wax Candle";
        sprite = Resources.Load<Sprite>("Art Assets/Items and Currency/Wax Candle");
        tier = 3;
        buyPrice = 1200;
        sellPrice = 360;
        flavorDescription = "A small candle that glows with a warm light.";
        mechanicsDescription = "Next time the user goes to the forest, they will gain an additional copy of the item they find.";
    }

    public override void UseItem(SpellCaster player)
    {
        player.RemoveFromInventory(this);
        player.itemsUsedThisTurn++;

        player.waxCandleUsed = true;
        PanelHolder.instance.displayNotify("Wax Candle", "The candle is lit. Next time you're in the forest, you will find duplicate items.", "InventoryScene");
    }
}
