using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SpellbookExtensions;

public class MainPageHandler : MonoBehaviour
{
    [SerializeField] private GameObject questTracker;
    [SerializeField] private GameObject spellTracker;
    [SerializeField] private GameObject crisisHandler;

    [SerializeField] private Text classType;
    [SerializeField] private Text manaCrystalsValue;
    [SerializeField] private Text manaCrystalsAddition;
    [SerializeField] private Text healthValue;
    [SerializeField] private Enemy enemy;

    [SerializeField] private SpriteRenderer characterImage;
    [SerializeField] private UIWarpController warpController;
    [SerializeField] private SpriteRenderer symbolImage;
    
    [SerializeField] private Button spellbookButton;
    [SerializeField] private Button inventoryButton;
    
    [SerializeField] private GameObject proclamationPanel;

    private bool diceTrayOpen;
    private bool manaHasChanged;

    Player localPlayer;
    public static MainPageHandler instance = null;

    void Awake()
    {
        //Check if there is already an instance of MainPageHandler
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of MainPageHandler.
            Destroy(gameObject);
    }

    private void Start()
    {
        setupMainPage();
    }

    private void Update()
    {
        // earn mana at end of turn (besides first turn of game)
        if (localPlayer != null && !(localPlayer.Spellcaster.numOfTurnsSoFar <= 1))
        {
            if (localPlayer.Spellcaster.endTurnManaCollected == false)
            {
                int endOfTurnMana = localPlayer.Spellcaster.CollectManaEndTurn();
                StartCoroutine(ShowManaEarned(endOfTurnMana));
            }
        }

        // update player's mana count
        if (localPlayer != null && manaHasChanged)
        {
            manaCrystalsValue.text = localPlayer.Spellcaster.iMana.ToString();
            manaHasChanged = false;
        }

        // TESTING AREA
        if(Input.GetKeyDown(KeyCode.D))
        {
            localPlayer.Spellcaster.dice["D6"] += 1;
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            CrisisHandler.instance.CallTsunami();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            localPlayer.Spellcaster.CollectSpell(new Fireball());
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("ShopScene");
        }
    }

    public void setupMainPage()
    {
        if (GameObject.FindGameObjectWithTag("LocalPlayer") == null) return;
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        classType.text = localPlayer.Spellcaster.classType;
        manaCrystalsValue.text = localPlayer.Spellcaster.iMana.ToString();
        healthValue.text = localPlayer.Spellcaster.fCurrentHealth.ToString() + "/ " + localPlayer.Spellcaster.fMaxHealth.ToString();

        // if it's not first turn of game, then destroy proclamation panel each time scene starts
        if (localPlayer.Spellcaster.procPanelShown)
        {
            Destroy(proclamationPanel.gameObject);

            // in case a panel didn't display during scan scene, display them in main scene
            PanelHolder.instance.CheckPanelQueue();
        }

        // create instances of QuestTracker/SpellTracker prefabs
        GameObject q = Instantiate(questTracker);
        GameObject s = Instantiate(spellTracker);
        // CHANGE CRISISHANDLER TO BE INSTANTIATED IN LOBBY SCENE
        GameObject c = Instantiate(crisisHandler);
            
        // set character image based on class
        characterImage.sprite = Resources.Load<Sprite>(localPlayer.Spellcaster.characterSpritePath);
        // set class symbol image based on class
        symbolImage.sprite = Resources.Load<Sprite>(localPlayer.Spellcaster.characterIconPath);
        // set background color based on class
        Color lightCol = new Color();
        ColorUtility.TryParseHtmlString(localPlayer.Spellcaster.hexStringLight, out lightCol);
        lightCol = lightCol.SetSaturation(0.35f);
        warpController.color = lightCol;

        // set onclick listeners for spellbook/inventory button
        spellbookButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.spellbookopen);
            SceneManager.LoadScene("SpellbookScene");
        });
        inventoryButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.inventoryOpen);
            SceneManager.LoadScene("InventoryScene");
        });
    }

    // FOR TESTING ONLY - DELETE LATER
    public void CollectMana()
    {
        localPlayer.Spellcaster.CollectMana(500);
        manaHasChanged = true;
    }
    public void CollectRandomItem()
    {
        SoundManager.instance.PlaySingle(SoundManager.itemfound);
        List<ItemObject> itemList = GameObject.Find("ItemList").GetComponent<ItemList>().listOfItems;
        ItemObject item = itemList[Random.Range(0, itemList.Count - 1)];
        PanelHolder.instance.displayNotify("You found an Item!", "You got found a " + item.name + "!", "OK");
        localPlayer.Spellcaster.AddToInventory(item);
    }

    // closing the proclamation panel
    public void CloseProclamationPanel()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        localPlayer.Spellcaster.procPanelShown = true;
        PanelHolder.instance.CheckPanelQueue();
    }

    // coroutine to show mana earned
    IEnumerator ShowManaEarned(int manaCount)
    {
        manaCrystalsAddition.text = "+" + manaCount.ToString();
        manaHasChanged = true;

        yield return new WaitForSeconds(2f);

        manaCrystalsAddition.text = "";
    }
}
