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

    [SerializeField] private Text roundsUntilCrisis;
    [SerializeField] private Text classType;
    [SerializeField] private Text manaCrystalsValue;
    [SerializeField] private Text manaCrystalsAddition;
    [SerializeField] private Text healthValue;

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

    [SerializeField] private Animator anim;
    #endregion

    private bool diceTrayOpen;

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
        // update mana/health values
        if(localPlayer != null)
        {
            manaCrystalsValue.text = localPlayer.Spellcaster.iMana.ToString();
            healthValue.text = localPlayer.Spellcaster.fCurrentHealth.ToString() + "/ " + localPlayer.Spellcaster.fMaxHealth.ToString();
        }

        // mute player's bgm if not their turn
        if (localPlayer != null && !localPlayer.bIsMyTurn)
            SoundManager.instance.musicSource.volume = 0;

        // update rounds until crisis
        roundsUntilCrisis.text = "Rounds Until Crisis: " + NetworkGameState.instance.RoundsUntilCrisisActivates().ToString();
    }

    public void setupMainPage()
    {
        if (GameObject.FindGameObjectWithTag("LocalPlayer") == null) return;
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        SetClassAttributes();

        classType.text = localPlayer.Spellcaster.classType;
        manaCrystalsValue.text = localPlayer.Spellcaster.iMana.ToString();
        healthValue.text = localPlayer.Spellcaster.fCurrentHealth.ToString() + "/ " + localPlayer.Spellcaster.fMaxHealth.ToString();

        // disable dice button if it's not player's turn, activate end turn button accordingly
        UICanvasHandler.instance.EnableDiceButton(localPlayer.bIsMyTurn);
        UICanvasHandler.instance.ActivateEndTurnButton(localPlayer.Spellcaster.hasRolled);

        // create instances of QuestTracker/SpellTracker prefabs
        Instantiate(questTracker);
        Instantiate(spellTracker);

        if (!UICanvasHandler.instance.chronomancerGone)
        {
            StartCoroutine("FadeIn");
            UICanvasHandler.instance.chronomancerGone = true;
        }

        // in case a panel didn't display during scan scene, display them in main scene
        PanelHolder.instance.CheckPanelQueue();

        CrisisHandler.instance.player = localPlayer;
    }

    private void SetClassAttributes()
    {
        // set character image/icons to associated sprites
        // set background music (only in at the start based on player's starting location)
        switch (localPlayer.Spellcaster.classType)
        {
            case "Alchemist":
                symbolImage.sprite = alchemistIcon;
                characterImage.sprite = alchemistSprite;
                if (!UICanvasHandler.instance.initialSoundsSet)
                {
                    SoundManager.instance.PlayGameBCM(SoundManager.regulusBGM);
                    UICanvasHandler.instance.initialSoundsSet = true;
                }
                break;
            case "Arcanist":
                symbolImage.sprite = arcanistIcon;
                characterImage.sprite = arcanistSprite;
                if (!UICanvasHandler.instance.initialSoundsSet)
                {
                    SoundManager.instance.PlayGameBCM(SoundManager.zandriaBGM);
                    UICanvasHandler.instance.initialSoundsSet = true;
                }
                break;
            case "Chronomancer":
                symbolImage.sprite = chronomancerIcon;
                characterImage.sprite = chronomancerSprite;
                if (!UICanvasHandler.instance.initialSoundsSet)
                {
                    SoundManager.instance.PlayGameBCM(SoundManager.merideaBGM);
                    UICanvasHandler.instance.initialSoundsSet = true;
                }
                break;
            case "Elementalist":
                symbolImage.sprite = elementalistIcon;
                characterImage.sprite = elementalistSprite;
                if (!UICanvasHandler.instance.initialSoundsSet)
                {
                    SoundManager.instance.PlayGameBCM(SoundManager.sarissaBGM);
                    UICanvasHandler.instance.initialSoundsSet = true;
                }
                break;
            case "Illusionist":
                symbolImage.sprite = illusionistIcon;
                characterImage.sprite = illusionistSprite;
                if (!UICanvasHandler.instance.initialSoundsSet)
                {
                    SoundManager.instance.PlayGameBCM(SoundManager.paradosBGM);
                    UICanvasHandler.instance.initialSoundsSet = true;
                }
                break;
            case "Summoner":
                symbolImage.sprite = summonerIcon;
                characterImage.sprite = summonerSprite;
                if (!UICanvasHandler.instance.initialSoundsSet)
                {
                    SoundManager.instance.PlayGameBCM(SoundManager.andromedaBGM);
                    UICanvasHandler.instance.initialSoundsSet = true;
                }
                break;
        }

        // set background color based on class
        Color lightCol = new Color();
        ColorUtility.TryParseHtmlString(localPlayer.Spellcaster.hexStringLight, out lightCol);
        lightCol = lightCol.SetSaturation(0.35f);
        warpController.color = lightCol;
        // set info panel color based on class
        Color panelCol = new Color();
        ColorUtility.TryParseHtmlString(localPlayer.Spellcaster.hexStringPanel, out panelCol);
        informationPanel.GetComponent<SpriteRenderer>().color = panelCol;

        LoadHandler.instance.setupComplete = true;
    }

    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("FadeIn", true);
        yield return new WaitUntil(() => anim.gameObject.GetComponent<Image>().color.a == 0);
        anim.gameObject.SetActive(false);
    }

    public void DisplayMana(int manaCollected)
    {
        StartCoroutine(ShowManaEarned(manaCollected));
    }

    // coroutine to show mana earned
    private IEnumerator ShowManaEarned(int manaCount)
    {
        manaCrystalsAddition.text = "+" + manaCount.ToString();

        yield return new WaitForSecondsRealtime(2f);

        manaCrystalsAddition.text = "";
    }

    // DELETE LATER - TESTING ONLY
    public void CollectCombatSpells()
    {
        foreach(Spell s in localPlayer.Spellcaster.chapter.spellsAllowed)
        {
            if(s.combatSpell)
                localPlayer.Spellcaster.CollectSpell(s);
        }
    }

    public void CollectSpells()
    {
        foreach(Spell s in localPlayer.Spellcaster.chapter.spellsAllowed)
        {
            if (!s.combatSpell)
                localPlayer.Spellcaster.CollectSpell(s);
        }
    }

    public void GoToCombat()
    {
        SceneManager.LoadScene("CombatSceneV2");
    }
}
