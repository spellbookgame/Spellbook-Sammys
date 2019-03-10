using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    private void Awake() 
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 15;

    public List<Item> items = new List<Item>();
    public Item dummyItem;  //For demo purposes.

    public void onClickAddItem()
    {
        Debug.Log("on click");
        Add(dummyItem);
    }

    public bool Add(Item item)
    {
        if(!item.isDefaultItem) 
        {
            if(items.Count >= space)
            {
                Debug.Log("Inventory is full.");
                return false;
            }
            items.Add(item);

            if(onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
            
        }
        return true;
    }

    // Update is called once per frame
    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}