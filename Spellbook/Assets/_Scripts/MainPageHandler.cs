using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SpellbookExtensions;
using System.Linq;

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
    [SerializeField] private GameObject informationPanel;
    
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
        // update player's mana count
        if (localPlayer != null && manaHasChanged)
        {
            manaCrystalsValue.text = localPlayer.Spellcaster.iMana.ToString();
            manaHasChanged = false;
        }

        // TEST AREA - DELETE LATER
        if(Input.GetKeyDown(KeyCode.T))
        {
            localPlayer.Spellcaster.CollectSpell(new Forecast());
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            localPlayer.Spellcaster.CollectSpell(new DejaVu());
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("MineScene");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            SceneManager.LoadScene("AlchemyTownScene");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("SwampScene");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("ForestScene");
        }
    }

    public void setupMainPage()
    {
        if (GameObject.FindGameObjectWithTag("LocalPlayer") == null) return;
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        classType.text = localPlayer.Spellcaster.classType;
        manaCrystalsValue.text = localPlayer.Spellcaster.iMana.ToString();
        healthValue.text = localPlayer.Spellcaster.fCurrentHealth.ToString() + "/ " + localPlayer.Spellcaster.fMaxHealth.ToString();

        // disable dice button if it's not player's turn
        UICanvasHandler.instance.EnableDiceButton(localPlayer.bIsMyTurn);

        // if it's not first turn of game, then destroy proclamation panel
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
        // set info panel color based on class
        Color panelCol = new Color();
        ColorUtility.TryParseHtmlString(localPlayer.Spellcaster.hexStringPanel, out panelCol);
        informationPanel.GetComponent<SpriteRenderer>().color = panelCol;

        // set onclick listeners for spellbook/inventory button
        spellbookButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.spellbookopen);
            UICanvasHandler.instance.ActivateSpellbookButtons(true);
            SceneManager.LoadScene("SpellbookScene");
        });
        inventoryButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.inventoryOpen);
            SceneManager.LoadScene("InventoryScene");
        });
    }

    // closing the proclamation panel
    public void CloseProclamationPanel()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        localPlayer.Spellcaster.procPanelShown = true;
        PanelHolder.instance.CheckPanelQueue();
    }

    public void DisplayMana(int manaCollected)
    {
        StartCoroutine(ShowManaEarned(manaCollected));
    }

    // coroutine to show mana earned
    private IEnumerator ShowManaEarned(int manaCount)
    {
        manaCrystalsAddition.text = "+" + manaCount.ToString();
        manaHasChanged = true;

        yield return new WaitForSecondsRealtime(2f);

        manaCrystalsAddition.text = "";
    }
}
