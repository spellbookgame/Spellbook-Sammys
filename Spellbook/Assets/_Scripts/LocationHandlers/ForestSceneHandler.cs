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

        if (SpellTracker.instance.SpellIsActive("Forecast"))
        {
            SpellTracker.instance.DoForecast();
        }
        else
        {
            int r = Random.Range(0, 100);
            Debug.Log("random number: " + r);
            if(r <= 7)
            {
                string[] items = new string[] { "Mimetic Vellum", "Rift Talisman", "Crystal Mirror" };
                ItemObject item = itemList.GetItemFromName(items[Random.Range(0, 3)]);
                PanelHolder.instance.displayBoardScan("You found an Item!", "You found a " + item.name + "!", item.sprite, "MainPlayerScene");
                localPlayer.Spellcaster.AddToInventory(item);
            }
            else if(r > 7 && r < 40)
            {
                string[] items = new string[] { "Infused Sapphire", "Abyssal Ore", "Hollow Cabochon", "Mystic Translocator" };
                ItemObject item = itemList.GetItemFromName(items[Random.Range(0, 4)]);
                PanelHolder.instance.displayBoardScan("You found an Item!", "You found a " + item.name + "!", item.sprite, "MainPlayerScene");
                localPlayer.Spellcaster.AddToInventory(item);
            }
            else
            {
                string[] items = new string[] { "Glowing Mushroom", "Wax Candle", "Aromatic Tea Leaves", "Opal Ammonite" };
                ItemObject item = itemList.GetItemFromName(items[Random.Range(0, 4)]);
                PanelHolder.instance.displayBoardScan("You found an Item!", "You found a " + item.name + "!", item.sprite, "MainPlayerScene");
                localPlayer.Spellcaster.AddToInventory(item);
            }
        }
    }
}
