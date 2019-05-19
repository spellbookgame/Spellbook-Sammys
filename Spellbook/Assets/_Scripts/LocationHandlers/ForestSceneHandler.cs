using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ForestSceneHandler : MonoBehaviour
{
    [SerializeField] private Button lookButton;
    [SerializeField] private Button leaveButton;

    private Player localPlayer;
    private ItemList itemList;

    private void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        itemList = GameObject.Find("ItemList").GetComponent<ItemList>();

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

        if (SpellTracker.instance.SpellIsActive(new Forecast()))
        {
            SpellTracker.instance.DoForecast();
        }
        else
        {
            List<ItemObject> items = itemList.listOfItems;
            ItemObject randItem = items[Random.Range(0, items.Count)];
            localPlayer.Spellcaster.AddToInventory(randItem);
            PanelHolder.instance.displayBoardScan("You found an Item!", "You found a " + randItem.name + "!", randItem.sprite, "MainPlayerScene");

            // if player used wax candle, add another copy of item
            if (localPlayer.Spellcaster.waxCandleUsed)
            {
                localPlayer.Spellcaster.AddToInventory(randItem);
                localPlayer.Spellcaster.waxCandleUsed = false;
            }
        }
    }
}
