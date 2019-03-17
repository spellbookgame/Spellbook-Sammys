using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;

    //Inventory inventory;

    InventorySlot[] slots;
    Player localPlayer;
    List<ItemObject> sInventory;
    
    // Start is called before the first frame update
    void Start()
    {
        //inventory = Inventory.instance;
        //inventory.onItemChangedCallback += UpdateUI;
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        sInventory = new List<ItemObject>(localPlayer.spellcaster.inventory);

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI() 
    {
        int i = 0;
        foreach(ItemObject item in sInventory)
        {
            ItemObject tempItem = sInventory[i];
            slots[i].AddItem(tempItem);

            ++i;
        }
        /*for(int i = 0; i < slots.Length; i++)
        {
            if(i < sInventory.Count)
            {
                ////
                Item temp = new Item(sInventory[i]);
                slots[i].AddItem(temp);
            } else
            {
                slots[i].ClearSlot();
            }
        }*/
        Debug.Log("UPDATING UI");
    }
}
