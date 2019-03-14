using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    public Button button_item0;
    public Button button_item1;
    public Button button_item2;
    public Button button_item3;
    public Button button_backButton;
    public Button button_buyButton;

    public Text text_myMana;
    public Text text_itemName;
    public Text text_itemDesc;
    public Text text_itemPrice;

    List<Item> allItems;
    Item item0;
    Item item1;
    Item item2;
    Item item3;

    Item currentSelected;

    SpellCaster spellcaster;

    void Start()
    {
        spellcaster = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>().spellcaster;
        float size = allItems.Count;

        //Choose 4 random items from item pool to put for sale.
        item0 = allItems[(int)UnityEngine.Random.Range(0f, size - 1)];

        while (item1.name != item0.name && item1 != null)
        {
            item1 = allItems[(int)UnityEngine.Random.Range(0f, size - 1)];
        }

        while (item2.name != item0.name && item2.name != item1.name && item2 != null)
        {
            item2 = allItems[(int)UnityEngine.Random.Range(0f, size - 1)];
        }

        while (item3.name != item0.name && item3.name != item1.name && item3.name != item2.name && item3 != null)
        {
            item3 = allItems[(int)UnityEngine.Random.Range(0f, size - 1)];
        }

        text_myMana.text = spellcaster.iMana + "";
        button_item0.image = item0.image;
        button_item1.image = item1.image;
        button_item2.image = item2.image;
        button_item3.image = item3.image;



        button_backButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);

            //TODO: Change scene later.
            SceneManager.LoadScene("MainPlayerScene");
        });

        button_buyButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            spellcaster.LoseMana((int)currentSelected.buyPrice);
            text_myMana.text = spellcaster.iMana + "";

            //TODO: Uncomment when AddToInventory is implemented
            //spellcaster.AddToInventory(currentSelected);


            text_itemDesc.text = "He he he thank you..";
        });


        button_item0.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            PopulateSaleUI(item0);
        });
        button_item1.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            PopulateSaleUI(item1);
        });
        button_item2.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            PopulateSaleUI(item2);
        });
        button_item3.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            PopulateSaleUI(item3);
        });
    }

    private void PopulateSaleUI(Item item)
    {
        currentSelected = item;
        text_itemName.text = item.name;
        text_itemPrice.text = item.buyPrice + "";
        text_itemDesc.text = item.description;
    }

    class Item
    {
        public Image image;
        public float buyPrice;
        public float sellPrice;
        public string name;
        public string description;
    }
}



