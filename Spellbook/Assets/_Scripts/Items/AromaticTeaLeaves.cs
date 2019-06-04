using UnityEngine;

public class AromaticTeaLeaves : ItemObject
{
    public AromaticTeaLeaves()
    {
        name = "Aromatic Tea Leaves";
        sprite = GameObject.Find("ItemContainer").transform.Find(name).GetComponent<SpriteRenderer>().sprite;
        tier = 3;
        buyPrice = 850;
        sellPrice = 255;
        flavorDescription = "A light scent of rosewood, sweet hibiscus, and toad belly--a perfect combination for opening the mind's eye.";
        mechanicsDescription = "User discards their runes and draws new ones from the low tier deck.";
    }

    public override void UseItem(SpellCaster player)
    {
        SoundManager.instance.PlaySingle(SoundManager.aromaticTeaLeaves);
        player.RemoveFromInventory(this);

        PanelHolder.instance.displayNotify("Aromatic Tea Leaves", "Discard your runes and draw 4 new runes from the low tier deck.", "InventoryScene");
    }
}
