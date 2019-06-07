using Bolt.Samples.Photon.Lobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour
{
    NetworkGameState gamestate;
    public InventoryUI inventory;
    public GameObject itemBeingDragged;
    public Image itemForGrabsImage;
    public Button itemSlotButton;
    string itemForGrabsStr;
    ItemObject itemObjNetwork;
    Player localPlayer;
    bool itemFromNetwork = false;


    public Text textStatus;
    string noItemHere = "No item here, leave an item for a friend?";
    string yesItemHere = "There is an item up for grabs!";

    public static ItemBox instance = null;

    // Start is called before the first frame update
    void Start()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
        {
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);
            BoltConsole.Write("ItemBox copy destroyed");
        }


        gamestate = GameObject.Find("GameState(Clone)").GetComponent<NetworkGameState>();
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        itemForGrabsStr = gamestate.ItemForGrabs();
        if(itemForGrabsStr != null && !itemForGrabsStr.Equals("")){
            textStatus.text = yesItemHere;
            itemObjNetwork = ItemList.instance.GetItemFromName(itemForGrabsStr);
            itemSlotButton.onClick.AddListener(() => inventory.ShowThirdPartyItemInfo(itemObjNetwork));
            itemForGrabsImage.sprite = itemObjNetwork.sprite;
            itemForGrabsImage.enabled = true;
        }
        else
        {
            textStatus.text = noItemHere; 
        }
    }

    public void DropItemToNetwork()
    {
        textStatus.text = yesItemHere;
        itemForGrabsStr = itemBeingDragged.GetComponent<Image>().sprite.name;
        BoltConsole.Write("Item being dropped!: "+itemForGrabsStr);
        //gamestate.ItemDropOff(itemForGrabsStr);
        NetworkManager.s_Singleton.DropItem(itemForGrabsStr);
        itemObjNetwork = ItemList.instance.GetItemFromName(itemForGrabsStr);
        BoltConsole.Write("Old Item!: " +itemObjNetwork.name);
        BoltConsole.Write("Localplayer is " + (localPlayer != null));
        BoltConsole.Write("Spellcaster: " +localPlayer.Spellcaster.spellcasterID);
        foreach(ItemObject item in localPlayer.Spellcaster.inventory)
        {
            if(item.name == itemForGrabsStr)
            {
                localPlayer.Spellcaster.RemoveFromInventory(item);
                break;
            }
        }

    }

    public void CollectItemFromNetwork()
    {
        textStatus.text = noItemHere;

        ItemObject newItem = ItemList.instance.GetItemFromName(gamestate.ItemForGrabs());
        localPlayer.Spellcaster.AddToInventory(newItem);
        NetworkManager.s_Singleton.PickUpItem();
    }

    public void CheckIfFromNetwork()
    {
        if(itemFromNetwork)
        {
            CollectItemFromNetwork();
        }
    }

    public void ItemFromNetwork(bool isFromNetwork)
    {
        itemFromNetwork = isFromNetwork;
    }
}
