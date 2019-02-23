using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/*
 * Written by Moises Martinez moi.compsci@gmail.com
 * 
 Generates random events when the player lands on an Event board space.
 Just call executeEventSpace(+1 overload) to apply effects.
     */
public class EventSpaceManager : MonoBehaviour
{
    SpellCaster spellCaster;

    //Generic events
    List<Action> genericFunctions;

    //Town Specific Events
    List<Action> alchemistTownFunctions;
    List<Action> arcanistTownFunctions;
    List<Action> elementalTownFunctions;

    public static EventSpaceManager instance = null;

    private void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);
    }

    private void Start()
    {
        spellCaster = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>().Spellcaster;
        /*
         TODO: The lists below (of all events) is going to get big soon,
         so we might need to 
         Possible options:
         1. Instantiate them here only ONCE and make this gamobject 
         DontDestroyOnLoad(this);
         2. Instantiate them only when needed in order to avoid
         instantiating all of them each time we Start() the Vuforia scene. 
         This can be done within the sub-regions below.*/
        genericFunctions = new List<Action>();
        genericFunctions.Add(eventAddMana);
        genericFunctions.Add(eventDeleteMana);
        genericFunctions.Add(eventLoseHealth);
        genericFunctions.Add(eventGainHealth);
        genericFunctions.Add(eventUnluckyDice);
        genericFunctions.Add(eventLuckyDice);
        genericFunctions.Add(eventLoseGlyphs);
        genericFunctions.Add(eventGainGlyph);

        //Example of adding city-specific events. All empty for now.
        alchemistTownFunctions = new List<Action>();
        arcanistTownFunctions = new List<Action>();
        elementalTownFunctions = new List<Action>();
    }

    //Call this one for generic events
    public void executeEventSpace()
    {
        int size = genericFunctions.Count;

        //Get a random function from the list of possible events and execute it.
        Action evnt = genericFunctions[(int) Random.Range(0,(float) size-1)];
        evnt();
    }

    //Call this one for town-specific events
    //TODO: Finalize and implement town-specific events.
    public void executeEventSpace(int townID)
    {
        switch (townID)
        {
            case 0:
                alchemistTownEvent();
                break;
            case 1:
                //Arcanist
                break;
            case 2:
                //Elementalist
                break;
            case 3:
                //Chronomancer
                break;
            case 4:
                //Trickster
                break;
            case 5:
                //Summoner
                break;
        }
    }

    #region board_space_events
    /* This region assumes spellCaster is not null 
     These functions are also private.*/
    //
    //
    #region generic_events_for_any_town
    private void eventAddMana()
    {
        Debug.Log("AddMana");
        int manaCount = (int)Random.Range(100, 1000);
        spellCaster.CollectMana(manaCount);
        PanelHolder.instance.displayEvent("You found " + manaCount.ToString() + " mana!");
    }

    private void eventDeleteMana()
    {
        Debug.Log("LoseMana");
        int manaCount = (int)Random.Range(100, 1000);
        spellCaster.LoseMana((int)Random.Range(100, 500));
        PanelHolder.instance.displayEvent("You lost " + manaCount.ToString() + " mana...");
    }

    private void eventLoseHealth()
    {
        //Need to make TakeDamage public in SpellCaster.cs
        int damage = (int)Random.Range(1f, 5f);
        spellCaster.TakeDamage(damage);
        PanelHolder.instance.displayEvent("You tripped over a rock and lost " + damage.ToString() + " health.");
    }

    private void eventGainHealth()
    {
        Debug.Log("GainHealth");
        int health = (int)Random.Range(1f, 3f);
        //Need to make HealDamage public in SpellCaster.cs
        spellCaster.HealDamage(health);
        PanelHolder.instance.displayEvent("After encountering a mystical sorcerer, you regained " + health.ToString() + " health!");
    }

    private void eventUnluckyDice()
    {
        Debug.Log("UnluckyDice");
        //Dice roll gets nerfed.
        PanelHolder.instance.displayEvent("You got hit with the curse Unlucky Dice! You can only roll 1-3 next turn.");
    }

    private void eventLuckyDice()
    {
        Debug.Log("LuckyDice");
        //Dice roll gets buffed.
        PanelHolder.instance.displayEvent("You found some lucky dice! You will roll a 5 or 6 next turn.");
    }

    private void eventLoseGlyphs()
    {
        Debug.Log("LoseGlyphs");
        string glyphLost = spellCaster.LoseRandomGlyph();
        PanelHolder.instance.displayEvent("An eagle swooped by and stole your " + glyphLost + "!");
    }

    private void eventGainGlyph()
    {
        Debug.Log("GainGlyphs");
        string glyph = spellCaster.CollectRandomGlyph();
        PanelHolder.instance.displayEvent("A mysterious figure came and gave you a " + glyph + ".");
    }
    #endregion
    /// 
    ///
    #region alchemist_town_events
    private void alchemistTownEvent()
    {
        int size = alchemistTownFunctions.Count;

        //Get a random function from the list of possible events and execute it.
        Action evnt = alchemistTownFunctions[(int)Random.Range(0, (float)size - 1)];
        evnt();
    }
    //TODO create alchemist town events.
    #endregion
    #endregion   
}
