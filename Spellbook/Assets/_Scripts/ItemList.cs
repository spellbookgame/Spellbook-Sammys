using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public static ItemList instance = null;

    public List<ItemObject> listOfItems;
    public List<ItemObject> tier1Items;
    public List<ItemObject> tier2Items;
    public List<ItemObject> tier3Items;

    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        listOfItems = new List<ItemObject>
        {
            new InfusedSapphire(),
            new AbyssalOre(),
            new GlowingMushroom(),
            new MimeticVellum(),
            new CrystalMirror(),
            new MysticTranslocator(),
            new AromaticTeaLeaves(),
            new OpalAmmonite(),
            new WaxCandle(),
            new HollowCabochon(),
            new RiftTalisman()
        };

        tier1Items = new List<ItemObject>()
        {
            new CrystalMirror(),
            new RiftTalisman(),
            new MimeticVellum()
        };

        tier2Items = new List<ItemObject>()
        {
            new InfusedSapphire(),
            new AbyssalOre(),
            new HollowCabochon(),
            new MysticTranslocator()
        };

        tier3Items = new List<ItemObject>()
        {
            new GlowingMushroom(),
            new WaxCandle(),
            new AromaticTeaLeaves(),
            new OpalAmmonite()
        };
    }

    public ItemObject GetItemFromName(string itemName)
    {
        ItemObject item = new ItemObject();
        switch(itemName)
        {
            case "Infused Sapphire":
                item = new InfusedSapphire();
                break;
            case "Abyssal Ore":
                item = new AbyssalOre();
                break;
            case "Glowing Mushroom":
                item = new GlowingMushroom();
                break;
            case "Mimetic Vellum":
                item = new MimeticVellum();
                break;
            case "Crystal Mirror":
                item = new CrystalMirror();
                break;
            case "Mystic Translocator":
                item = new MysticTranslocator();
                break;
            case "Aromatic Tea Leaves":
                item = new AromaticTeaLeaves();
                break;
            case "Opal Ammonite":
                item = new OpalAmmonite();
                break;
            case "Wax Candle":
                item = new WaxCandle();
                break;
            case "Hollow Cabochon":
                item = new HollowCabochon();
                break;
            case "Rift Talisman":
                item = new RiftTalisman();
                break;
        }
        return item;
    }
}