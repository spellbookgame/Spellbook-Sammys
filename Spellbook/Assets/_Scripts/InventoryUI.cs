using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Button buttonClose;
    [SerializeField] private Button buttonUse;
    [SerializeField] private GameObject infoPanel;

    //Inventory inventory;

    InventorySlot[] slots;
    Player localPlayer;
    List<ItemObject> sInventory;

    private bool infoPanelOpen;

    void Start()
    {
        //inventory = Inventory.instance;
        //inventory.onItemChangedCallback += UpdateUI;
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        sInventory = new List<ItemObject>(localPlayer.spellcaster.inventory);

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        UpdateUI();
    }

    // populate panel with items from player's inventory
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

            // add onclick to use item
            buttonUse.onClick.AddListener(() => 
            {
                if (localPlayer.Spellcaster.itemsUsedThisTurn >= 2)
                {
                    PanelHolder.instance.displayNotify("Too Many Items", "You've already used 2 items this turn.", "OK");
                }
                else
                    item.UseItem(localPlayer.Spellcaster);
            });

            infoPanelOpen = true;
        }
        else
        {
            // remove onclick from use button
            buttonUse.onClick.RemoveAllListeners();

            infoPanel.SetActive(false);
            infoPanelOpen = false;
        }

        // adding onclick listener to close button
        buttonClose.onClick.AddListener(() =>
        {
            if (infoPanelOpen)
            {
                // remove onclick from use button
                buttonUse.onClick.RemoveAllListeners();

                infoPanel.SetActive(false);
                infoPanelOpen = false;
            }
        });
    }
}
