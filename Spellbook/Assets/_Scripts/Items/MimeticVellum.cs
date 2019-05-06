using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimeticVellum : ItemObject
{
    public MimeticVellum()
    {
        name = "Mimetic Vellum";
        sprite = Resources.Load<Sprite>("Art Assets/Items and Currency/MimeticVellum");
        tier = 1;
        buyPrice = 2800;
        sellPrice = 840;
        flavorDescription = "These sheafs of calfskin were created by a deranged wizard who sought to duplicate documents without the employ of a scribe. Madness!";
        mechanicsDescription = "Duplicates a rune of the user's choice. User must have the rune in possession.";
    }

    public override void UseItem(SpellCaster player)
    {
        player.RemoveFromInventory(this);
        PanelHolder.instance.displayNotify("Mimetic Vellum", "Choose a rune from your hand and duplicate it. Discard a rune to keep the duplicate.", "InventoryScene");
    }
}
