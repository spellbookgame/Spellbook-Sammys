using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ForestSceneHandler : MonoBehaviour
{
    [SerializeField] private Button lookButton;
    [SerializeField] private Button leaveButton;

    private bool collectedItem;

    private Player localPlayer;
    private List<ItemObject> itemList;

    private void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        itemList = GameObject.Find("ItemList").GetComponent<ItemList>().listOfItems;

        lookButton.onClick.AddListener(LookForItem);
        leaveButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("MainPlayerScene");
        });

        QuestTracker.instance.TrackLocationQuest("location_forest");
        QuestTracker.instance.TrackErrandQuest("location_forest");
    }

    private void LookForItem()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        
        if(!collectedItem)
        {
            ItemObject item = itemList[Random.Range(0, itemList.Count)];
            PanelHolder.instance.displayBoardScan("You found an Item!", "You found a " + item.name + "!", item.sprite, "OK");
            localPlayer.Spellcaster.AddToInventory(item);
            collectedItem = true;
        }
        else
        {
            PanelHolder.instance.displayNotify("Don't be greedy!", "You can only take once per visit. Now leave!", "MainPlayerScene");
        }
    }
}
