﻿using System.Collections;
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
    [SerializeField] private Text manaCrystalsAddition;
    [SerializeField] private Text healthValue;
    [SerializeField] private Enemy enemy;

    [SerializeField] private SpriteRenderer characterImage;
    [SerializeField] private GameObject warpBackground1;
    [SerializeField] private GameObject warpBackground2;
    [SerializeField] private SpriteRenderer symbolImage;
    
    [SerializeField] private Button diceButton;
    [SerializeField] private Button spellbookButton;
    
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

        // disable dice button if it's not player's turn
        if (localPlayer != null && !localPlayer.bIsMyTurn)
            diceButton.interactable = false;

        // update player's list of active quests
        if (localPlayer != null && localPlayer.Spellcaster.activeQuests.Count > 0)
            UpdateActiveQuests();
    }

    public void setupMainPage()
    {
        if (GameObject.FindGameObjectWithTag("LocalPlayer") == null) return;
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        classType.text = localPlayer.Spellcaster.classType;
        manaCrystalsValue.text = localPlayer.Spellcaster.iMana.ToString();
        healthValue.text = localPlayer.Spellcaster.fCurrentHealth.ToString() + "/ " + localPlayer.Spellcaster.fMaxHealth.ToString();

        Debug.Log("main page turn just ended: " + localPlayer.Spellcaster.turnJustEnded);
        // set text for earned mana briefly 
        if(localPlayer.Spellcaster.turnJustEnded == true)
        {
            int endOfTurnMana = localPlayer.Spellcaster.CollectManaEndTurn();
            StartCoroutine(ShowManaEarned(endOfTurnMana));
        }

        // if it's not first turn of game, then destroy proclamation panel each time scene starts
        if (localPlayer.Spellcaster.procPanelShown)
        {
            Destroy(proclamationPanel.gameObject);

            // in case a panel didn't display during scan scene, display them in main scene
            PanelHolder.instance.CheckPanelQueue();
            Debug.Log("queue checked in main scene");
        }

        // create instances of QuestTracker/SpellTracker prefabs
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

        // set onclick listeners for spellbook button
        spellbookButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.spellbookopen);
            SceneManager.LoadScene("SpellbookScene");
        });
    }

    // updating the list of active quests
    private void UpdateActiveQuests()
    {
        foreach (Quest q in localPlayer.Spellcaster.activeQuests.ToArray())
        {
            // if the player's turns from starting the quest exceeded the turn limit
            if (localPlayer.Spellcaster.NumOfTurnsSoFar - q.startTurn > q.turnLimit)
            {
                // remove the quest from the active quests list and notify player
                localPlayer.Spellcaster.activeQuests.Remove(q);
                SoundManager.instance.PlaySingle(SoundManager.questfailed);
                PanelHolder.instance.displayNotify(q.questName + " Failed...", "You failed to complete the quest in your given time.", "OK");
            }
        }
    }

    // FOR TESTING ONLY - DELETE LATER
    public void CollectSpell()
    {
        localPlayer.Spellcaster.CollectSpell(localPlayer.Spellcaster.chapter.spellsAllowed[0]);
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
        localPlayer.Spellcaster.turnJustEnded = false;
    }
}
