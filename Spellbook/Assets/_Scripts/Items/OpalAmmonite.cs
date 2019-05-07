using UnityEngine;

public class OpalAmmonite : ItemObject
{
    public OpalAmmonite()
    {
        name = "Opal Ammonite";
        sprite = Resources.Load<Sprite>("Art Assets/Items and Currency/Opal Ammonite");
        tier = 3;
        buyPrice = 600;
        sellPrice = 180;
        flavorDescription = "An iridescent fossil of an ancient creature; as mysterious as it is alluring.";
        mechanicsDescription = "Instantly grants a larger amount of mana crystals.";
    }

    public override void UseItem(SpellCaster player)
    {
        player.RemoveFromInventory(this);

        int randMana = Random.Range(800, 2600);
        player.CollectMana(randMana);

        PanelHolder.instance.displayNotify("Opal Ammonite", "The Opal Ammonite granted you " + randMana + " mana!", "InventoryScene");
    }
}
