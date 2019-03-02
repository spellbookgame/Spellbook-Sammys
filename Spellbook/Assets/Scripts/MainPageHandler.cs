using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPageHandler : MonoBehaviour
{
    [SerializeField] private Text classType;
    [SerializeField] private Text manaCrystalsValue;
    [SerializeField] private Text healthValue;
    [SerializeField] private Enemy enemy;

    [SerializeField] private Image characterImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image symbolImage;

    [SerializeField] private Button scanButton;
    [SerializeField] private Button combatButton;
    [SerializeField] private Button spellbookButton;

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
        if (localPlayer.Spellcaster.activeSpells.Count > 0)
            UpdateActiveSpells();
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

        // set character image based on class
        characterImage.sprite = Resources.Load<Sprite>(localPlayer.Spellcaster.characterSpritePath);

        // set background image based on class
        backgroundImage.sprite = Resources.Load<Sprite>(localPlayer.Spellcaster.characterBackgroundPath);

        // set class symbol image based on class
        symbolImage.sprite = Resources.Load<Sprite>(localPlayer.Spellcaster.characterIconPath);

        // set onclick listeners for buttons
        scanButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("VuforiaScene");
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
}
