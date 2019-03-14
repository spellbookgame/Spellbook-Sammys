using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    public Button button_item0;
    public Image image_item0;

    public Button button_item1;
    public Image image_item1;

    public Button button_item2;
    public Image image_item2;

    public Button button_item3;
    public Image image_item3;

    public Button button_backButton;
    public Button button_buyButton;

    public Text text_myMana;
    public Text text_itemName;
    public Text text_itemDesc;
    public Text text_itemPrice;

    public List<ItemObject> allItems;
    ItemObject item0;
    ItemObject item1;
    ItemObject item2;
    ItemObject item3;

    public Button BuyButton;
    public GameObject manaCrystalImage;

    ItemObject currentSelected;

    SpellCaster spellcaster;

    void Start()
    {
        allItems = GetComponent<ItemList>().listOfItems;
        spellcaster = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>().spellcaster;
        
        float size = allItems.Count;

        //Choose 4 random items from item pool to put for sale.
        item0 = allItems[(int)UnityEngine.Random.Range(0f, size - 1)];
        if(size > 1)
        {
            item1 = allItems[(int)UnityEngine.Random.Range(0f, size - 1)];
            while (item1.name.Equals(item0.name) )
            {
                item1 = allItems[(int)UnityEngine.Random.Range(0f, size - 1)];
            }
        }
        
        if(size > 2)
        {
            item2 = allItems[(int)UnityEngine.Random.Range(0f, size - 1)];
            while (item2.name.Equals(item0.name) || item2.name.Equals(item1.name) )
            {
                item2 = allItems[(int)UnityEngine.Random.Range(0f, size - 1)];
            }
        }
        
        if(size > 3)
        {
            item3 = allItems[(int)UnityEngine.Random.Range(0f, size - 1)];
            while (item3.name.Equals(item0.name) || item3.name.Equals(item1.name) || item3.name.Equals(item2.name) )
            {
                item3 = allItems[(int)UnityEngine.Random.Range(0f, size - 1)];
            }
        }
        image_item0.sprite = item0.sprite;
        image_item1.sprite = item1.sprite;
        image_item2.sprite = item2.sprite;
        image_item3.sprite = item3.sprite;

        text_myMana.text = spellcaster.iMana + "";
        


        /*
        button_backButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);

            //TODO: Change scene later.
            SceneManager.LoadScene("MainPlayerScene");
        });*/

        button_buyButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            if(spellcaster.iMana >= currentSelected.buyPrice)
            {
                spellcaster.LoseMana((int)currentSelected.buyPrice);
                text_myMana.text = spellcaster.iMana + "";
                
                spellcaster.AddToInventory(currentSelected);


                text_itemDesc.text = "He he he thank you..";
            }
            else
            {
                text_itemDesc.text = "Not enough mana stranger!";
            }
        });


        button_item0.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            PopulateSaleUI(item0);
            BuyButton.gameObject.SetActive(true);
        });
        button_item1.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            PopulateSaleUI(item1);
            BuyButton.gameObject.SetActive(true);
        });
        button_item2.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            PopulateSaleUI(item2);
            BuyButton.gameObject.SetActive(true);
        });
        button_item3.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            PopulateSaleUI(item3);
            BuyButton.gameObject.SetActive(true);
        });
    }

    private void PopulateSaleUI(ItemObject item)
    {
        currentSelected = item;
        text_itemName.text = item.name;
        text_itemPrice.text = item.buyPrice + "";
        text_itemDesc.text = item.mechanicsDescription+ "\n" +item.flavorDescription;
    }
}



