using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    [SerializeField] private GameObject inventoryPanel;

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
        
        // if there are 5 or more items in the inventory, increase scroll rect size by 200 for each item > 5
        if (i > 5)
        {
            int expandPanel = (i - 5) * 300;
            inventoryPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(inventoryPanel.GetComponent<RectTransform>().sizeDelta.x,
                (float)inventoryPanel.GetComponent<RectTransform>().sizeDelta.y + expandPanel);
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
