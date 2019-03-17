using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour
{
    public Sprite spriteGlowingMushroom;
    public Sprite spriteInfusedSapphire;
    public Sprite spriteMimeticVellum;
    public Sprite spriteAbyssalOre;

    public List<ItemObject> listOfItems;
    void Awake()
    {
        listOfItems = new List<ItemObject>();
        // Name, Buy Price, Sell Price, Flavor, Mechanics
        // Image commented out but it's between name and buy. Refer to ItemObject Class.
        
        listOfItems.Add(new ItemObject("Glowing Mushroom", spriteGlowingMushroom ,1000, 500, "Oooo Glowy", "Heals 25% damage, can probably be sold to someone at a high price."));
        listOfItems.Add(new ItemObject("Infused Sapphire", spriteInfusedSapphire, 2000, 1000, "This sapphire is embued with pure arcane energy. When shattered, it gives its user a temporary power boost.", "Add +6 to your next damage spell (one time use)."));
        listOfItems.Add(new ItemObject("Mimetic-Vellum", spriteMimeticVellum, 2500, 1250, "These sheafs of calfskin were created by a deranged wizard who sought to duplicate documents without the employ of a scribe. Madness!", "Duplicates a glyph of the your choice (but you must already have the glyph in your possession)."));
        listOfItems.Add(new ItemObject("Abyssal Ore", spriteAbyssalOre, 3000, 1500, "A rare metal brimming with magical energy.", "When used, increase the user's next attack by 50%. If the user is an Alchemist, they transmute the metal into additional X mana."));
    }
}

