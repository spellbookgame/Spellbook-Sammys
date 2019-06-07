using UnityEngine;
using System;

public class ItemObject : IComparable<ItemObject>
{
    public string name = "New Item";
    public Sprite sprite;
    public int tier;
    public int buyPrice;
    public int sellPrice;
    public string flavorDescription;
    public string mechanicsDescription;

    public int CompareTo(ItemObject other) 
    {
        if(other == null)
        {
            return 1;
        }

        return 0;
    }

    public virtual void UseItem(SpellCaster player)
    {
        // do something
    }
}
