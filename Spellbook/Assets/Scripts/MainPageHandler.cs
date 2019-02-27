using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPageHandler : MonoBehaviour
{
    [SerializeField] private Text manaCrystalsValue;
    [SerializeField] private Text healthValue;
    [SerializeField] private Text activeSpellsValue;
    [SerializeField] private Text classText;
    [SerializeField] private Enemy enemy;
    [SerializeField] private Image characterImage;

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
        if(localPlayer.Spellcaster.activeSpells.Count > 0)
            UpdateActiveSpells();
    }

    public void setupMainPage()
    {
        if (GameObject.FindGameObjectWithTag("LocalPlayer") == null) return;
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        manaCrystalsValue.text = localPlayer.Spellcaster.iMana.ToString();
        healthValue.text = localPlayer.Spellcaster.fCurrentHealth.ToString();

        classText.text = "You are playing as " + localPlayer.Spellcaster.classType;

        foreach (Spell entry in localPlayer.Spellcaster.activeSpells)
        {
            activeSpellsValue.text = activeSpellsValue.text + entry.sSpellName + "\n";
        }

        // if an enemy does not exist, create one
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            // instantiating enemy with 20 health
            enemy = Instantiate(enemy);
            enemy.Initialize(20f);
            enemy.fCurrentHealth = enemy.fMaxHealth;
        }

        // set player's image based on class
        Debug.Log(localPlayer.Spellcaster.characterSpritePath);
        characterImage.sprite = Resources.Load<Sprite>(localPlayer.Spellcaster.characterSpritePath);

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
    
    // changing the list of active spells
    private void UpdateActiveSpells()
    {
        foreach (Spell entry in localPlayer.Spellcaster.chapter.spellsCollected)
        {
            // if the player has gone the amount of turns that the spell lasts
            if (localPlayer.Spellcaster.NumOfTurnsSoFar - entry.iCastedTurn == entry.iTurnsActive)
            {
                // remove the spell from the active spells list
                localPlayer.Spellcaster.activeSpells.Remove(entry);
                PanelHolder.instance.displayNotify(entry.sSpellName, entry.sSpellName + " wore off...");

                // remove the text from the screen
                activeSpellsValue.text = activeSpellsValue.text.Replace(entry.sSpellName, "");
            }
        }
    }
}
