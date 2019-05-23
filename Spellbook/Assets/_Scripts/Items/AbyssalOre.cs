using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbyssalOre : ItemObject
{
    public AbyssalOre()
    {
        name = "Abyssal Ore";
        sprite = Resources.Load<Sprite>("Art Assets/Items and Currency/Abyssal Ore");
        tier = 2;
        buyPrice = 1700;
        sellPrice = 510;
        flavorDescription = "A rare metal brimming with magical energy. Its constant changing form means no one knows what it will turn into, next.";
        mechanicsDescription = "When used, this transforms into a random die from D4 to D8. One time use only.";
    }

    public override void UseItem(SpellCaster player)
    {
        SoundManager.instance.PlaySingle(SoundManager.abyssalOre);
        player.RemoveFromInventory(this);

        // give player a random dice
        int randSides = Random.Range(4, 8);

        if (player.tempDice.ContainsKey("D" + randSides.ToString()))
            player.tempDice["D" + randSides.ToString()] += 1;
        else
            player.tempDice.Add("D" + randSides.ToString(), 1);

        PanelHolder.instance.displayNotify("Abyssal Ore", "The Abyssal Ore gave you a D" + randSides.ToString() + "!", "InventoryScene");
    }
}
