using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SpellbookExtensions;
using UnityEngine.Events;

public class MainPageHandler : MonoBehaviour
{
    #region private_fields
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

    [SerializeField] private Sprite alchemistIcon;
    [SerializeField] private Sprite arcanistIcon;
    [SerializeField] private Sprite chronomancerIcon;
    [SerializeField] private Sprite elementalistIcon;
    [SerializeField] private Sprite illusionistIcon;
    [SerializeField] private Sprite summonerIcon;

    [SerializeField] private Sprite alchemistSprite;
    [SerializeField] private Sprite arcanistSprite;
    [SerializeField] private Sprite chronomancerSprite;
    [SerializeField] private Sprite elementalistSprite;
    [SerializeField] private Sprite illusionistSprite;
    [SerializeField] private Sprite summonerSprite;
    
    [SerializeField] private Button spellbookButton;
    [SerializeField] private Button inventoryButton;
    #endregion

    private bool diceTrayOpen;
    private bool manaHasChanged;

    Player localPlayer;
    public static MainPageHandler instance = null;

    public UnityEvent loadSceneEvent;

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

        // ------------ TEST AREA - DELETE LATER ----------
        if (Input.GetKeyDown(KeyCode.G))
        {
            localPlayer.Spellcaster.CollectSpell(new Allegro());
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("MineScene");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("ShopScene");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("ForestScene");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            SceneManager.LoadScene("IllusionTownScene");
        }
        // -------------------------------------------------
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

        // create instances of QuestTracker/SpellTracker prefabs
        Instantiate(questTracker);
        Instantiate(spellTracker);
        // CHANGE CRISISHANDLER TO BE INSTANTIATED IN LOBBY SCENE
        Instantiate(crisisHandler);

        SetClassAttributes();

        // in case a panel didn't display during scan scene, display them in main scene
        PanelHolder.instance.CheckPanelQueue();
    }
    
    private void SetClassAttributes()
    {
        // set character image/icons to associated sprites
        switch(localPlayer.Spellcaster.classType)
        {
            case "Alchemist":
                symbolImage.sprite = alchemistIcon;
                characterImage.sprite = alchemistSprite;
                break;
            case "Arcanist":
                symbolImage.sprite = arcanistIcon;
                characterImage.sprite = arcanistSprite;
                break;
            case "Chronomancer":
                symbolImage.sprite = chronomancerIcon;
                characterImage.sprite = chronomancerSprite;
                break;
            case "Elementalist":
                symbolImage.sprite = elementalistIcon;
                characterImage.sprite = elementalistSprite;
                break;
            case "Illusionist":
                symbolImage.sprite = illusionistIcon;
                characterImage.sprite = illusionistSprite;
                break;
            case "Summoner":
                symbolImage.sprite = summonerIcon;
                characterImage.sprite = summonerSprite;
                break;
        }

        Color lightCol = new Color();
        ColorUtility.TryParseHtmlString(localPlayer.Spellcaster.hexStringLight, out lightCol);
        lightCol = lightCol.SetSaturation(0.35f);
        warpController.color = lightCol;
        // set info panel color based on class
        Color panelCol = new Color();
        ColorUtility.TryParseHtmlString(localPlayer.Spellcaster.hexStringPanel, out panelCol);
        informationPanel.GetComponent<SpriteRenderer>().color = panelCol;
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
