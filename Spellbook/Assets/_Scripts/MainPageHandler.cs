using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPageHandler : MonoBehaviour
{
    [SerializeField] private GameObject questTracker;
    [SerializeField] private GameObject spellTracker;

    [SerializeField] private Text classType;
    [SerializeField] private Text manaCrystalsValue;
    [SerializeField] private Text healthValue;
    [SerializeField] private Enemy enemy;

    [SerializeField] private Image characterImage;
    [SerializeField] private GameObject warpBackground1;
    [SerializeField] private GameObject warpBackground2;
    [SerializeField] private Image symbolImage;
    
    [SerializeField] private Button rollButton;
    [SerializeField] private Button combatButton;
    [SerializeField] private Button spellbookButton;
    
    [SerializeField] private GameObject diceRollPanel;

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
        // update player's list of active spells
        if (localPlayer != null && localPlayer.Spellcaster.activeSpells.Count > 0)
            UpdateActiveSpells();
        // update player's list of active quests
        if (localPlayer != null && localPlayer.Spellcaster.activeQuests.Count > 0)
            UpdateActiveQuests();

        // disable roll button if it's not player's turn
        if (localPlayer != null && !localPlayer.bIsMyTurn)
            rollButton.enabled = false;
        else
            rollButton.enabled = true;
    }

    public void setupMainPage()
    {
        if (GameObject.FindGameObjectWithTag("LocalPlayer") == null) return;
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        classType.text = localPlayer.Spellcaster.classType;
        manaCrystalsValue.text = localPlayer.Spellcaster.iMana.ToString();
        healthValue.text = localPlayer.Spellcaster.fCurrentHealth.ToString() + "/ " + localPlayer.Spellcaster.fMaxHealth.ToString();

        // if an enemy does not exist, create one
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            // instantiating enemy with 20 health
            enemy = Instantiate(enemy);
            enemy.Initialize(20f);
            enemy.fCurrentHealth = enemy.fMaxHealth;
        }

        // create instance of QuestTracker prefab
        GameObject q = Instantiate(questTracker);
        GameObject s = Instantiate(spellTracker);
            
        // set character image based on class
        characterImage.sprite = Resources.Load<Sprite>(localPlayer.Spellcaster.characterSpritePath);
        // set class symbol image based on class
        symbolImage.sprite = Resources.Load<Sprite>(localPlayer.Spellcaster.characterIconPath);
        // set background color based on class
        Color lightCol = new Color();
        ColorUtility.TryParseHtmlString(localPlayer.Spellcaster.hexStringLight, out lightCol);
        Color darkCol = new Color();
        ColorUtility.TryParseHtmlString(localPlayer.Spellcaster.hexStringDark, out darkCol);
        warpBackground1.GetComponent<SpriteRenderer>().color = darkCol;
        warpBackground2.GetComponent<SpriteRenderer>().color = lightCol;

        // set onclick listeners for buttons
        rollButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            diceRollPanel.SetActive(true);
        });
        combatButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("CombatScene");
        });
        spellbookButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("SpellbookScene");
        });
    }
    
    // updating the list of active spells
    private void UpdateActiveSpells()
    {
        foreach (Spell entry in localPlayer.Spellcaster.chapter.spellsCollected)
        {
            // if the player has gone the amount of turns that the spell lasts
            if (localPlayer.Spellcaster.NumOfTurnsSoFar - entry.iCastedTurn == entry.iTurnsActive)
            {
                // remove the spell from the active spells list and notify player
                localPlayer.Spellcaster.activeSpells.Remove(entry);
                PanelHolder.instance.displayNotify(entry.sSpellName, entry.sSpellName + " wore off...");
            }
        }
    }

    // updating the list of active quests
    private void UpdateActiveQuests()
    {
        foreach (Quest q in localPlayer.Spellcaster.activeQuests)
        {
            // if the player's turns from starting the quest exceeded the turn limit
            if (localPlayer.Spellcaster.NumOfTurnsSoFar - q.startTurn > q.turnLimit)
            {
                // remove the quest from the active quests list and notify player
                localPlayer.Spellcaster.activeQuests.Remove(q);
                PanelHolder.instance.displayNotify(q.questName + " Failed...", "You failed to complete the quest in your given time.");
            }
        }
    }
}
