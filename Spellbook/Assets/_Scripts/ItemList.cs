using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{

    public List<ItemObject> listOfItems;
    void Awake()
    {
        listOfItems = new List<ItemObject>
        {
            new InfusedSapphire(),
            new AbyssalOre(),
            new GlowingMushroom(),
            new MimeticVellum(),
            new CrystalMirror()
        };
    }
}