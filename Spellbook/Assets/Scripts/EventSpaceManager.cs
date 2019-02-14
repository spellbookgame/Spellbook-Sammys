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
        genericFunctions.Add(eventGainGlyphs);

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
        spellCaster.iMana += (int) Random.Range(5f, 50f);
    }

    private void eventDeleteMana()
    {
        Debug.Log("LoseMana");
        spellCaster.iMana += (int)Random.Range(5f, 50f);
    }

    private void eventLoseHealth()
    {
        Debug.Log("LoseHealth");
        //Need to make TakeDamage public in SpellCaster.cs
        //spellCaster.TakeDamage((int)Random.Range(1f, 3f));
    }

    private void eventGainHealth()
    {
        Debug.Log("GainHealth");
        //Need to make HealDamage public in SpellCaster.cs
        //spellCaster.HealDamage((int)Random.Range(1f, 5f));
    }

    private void eventUnluckyDice()
    {
        Debug.Log("UnluckyDice");
        //Dice roll gets nerfed.
    }

    private void eventLuckyDice()
    {
        Debug.Log("LuckyDice");
        //Dice roll gets buffed.
    }

    private void eventLoseGlyphs()
    {
        Debug.Log("LoseGlyphs");
    }

    private void eventGainGlyphs()
    {
        Debug.Log("GainGlyphs");
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
