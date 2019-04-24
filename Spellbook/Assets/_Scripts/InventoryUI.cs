using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject infoPanel;

    //Inventory inventory;

    InventorySlot[] slots;
    Player localPlayer;
    List<ItemObject> sInventory;

    private bool infoPanelOpen;

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

    void UpdateUI() 
    {
        int i = 0;
        foreach(ItemObject item in sInventory)
        {
            ItemObject tempItem = sInventory[i];
            slots[i].AddItem(tempItem);
            slots[i].button.onClick.AddListener(() => OpenItemPanel(tempItem));

            ++i;
        }
        
        // if there are 5 or more items in the inventory, increase scroll rect size by 200 for each item > 5
        if (i > 4)
        {
            int expandPanel = (i - 4) * 350;
            inventoryPanel.GetComponent<RectTransform>().sizeDelta = new Vector2((float)inventoryPanel.GetComponent<RectTransform>().sizeDelta.x
                                                                            + expandPanel, inventoryPanel.GetComponent<RectTransform>().sizeDelta.y);
        }
        Debug.Log("UPDATING UI");
    }

    // panel that shows item information
    private void OpenItemPanel(ItemObject item)
    {
        if (!infoPanelOpen)
        {
            infoPanel.SetActive(true);
            // setting panel text
            infoPanel.transform.GetChild(0).GetComponent<Text>().text = item.name;
            infoPanel.transform.GetChild(1).GetComponent<Text>().text = item.flavorDescription + "\n\n" + item.mechanicsDescription;
            infoPanelOpen = true;
        }
        else
        {
            infoPanel.SetActive(false);
            infoPanelOpen = false;
        }
        // adding onclick listener to close button
        infoPanel.transform.Find("button_close").GetComponent<Button>().onClick.AddListener(() =>
        {
            if (infoPanelOpen)
            {
                infoPanel.SetActive(false);
                infoPanelOpen = false;
            }
        });
    }
}
