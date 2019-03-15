using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;

    Inventory inventory;

    InventorySlot[] slots;
    
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI() 
    {
        Player localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        List<ItemObject> sInventory = new List<ItemObject>(localPlayer.spellcaster.inventory);
        for(int i = 0; i < slots.Length; i++)
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
        }
        Debug.Log("UPDATING UI");
    }
}
