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
        
        listOfItems.Add(new ItemObject("Glowing Mushroom", spriteGlowingMushroom, 3, 200, 1800, "Ooo, glowy.", "Can be sold at a high price to shops."));
        listOfItems.Add(new ItemObject("Infused Sapphire", spriteInfusedSapphire, 2, 1400, 420, "This sapphire is embued with pure arcane energy.", 
                                        "When shattered, teleports the user to a random location."));
        listOfItems.Add(new ItemObject("Mimetic-Vellum", spriteMimeticVellum, 1, 2800, 840, "These sheafs of calfskin were created by a deranged wizard who sought to duplicate documents without the employ of a scribe. Madness!", 
                                        "Duplicates a rune of the user's choice. User must have the rune in possession."));
        listOfItems.Add(new ItemObject("Abyssal Ore", spriteAbyssalOre, 2, 1700, 510, "A rare metal brimming with magical energy. Its constant changing form means no one knows what it will turn into, next.",
                                        "When used, this transforms into a random die from D4 to D8. One time use only."));
    }
}