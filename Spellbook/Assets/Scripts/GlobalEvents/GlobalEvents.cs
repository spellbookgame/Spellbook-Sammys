using Bolt.Samples.Photon.Lobby;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/*
 For host device only.
     */
public class GlobalEvents
{

    class GlobalEvent
    {
        public string name;
        public string description;
        public int ID;
        public Action action;

        public GlobalEvent(string name, string description, int ID, Action action)
        {
            this.name = name;
            this.description = description;
            this.ID = ID;
            this.action = action;
        }
    }


    //TODO: Load descriptions from JSON file after prototype phase.
    static class GlobalEventDescriptions
    {
        public static string tsunamiDesc = "A tsunami like the realm has never " +
            "seen before is about to hit the six cities.It will deal half " +
            "of current health of all wizards.If the Elementalist has a level " +
            "three spell unlocked the Elementalist will be able to stop the waters " +
            "from crashing into the cities.";

    }

    Dictionary<int,GlobalEvent> list_AllEvents;
    public int[] spellcasterList;

    int alchemistID = 0;
    int arcanistID = 1;
    int elementalistID = 2;
    int chronomancerID = 3;
    int tricksterID = 4;
    int summonerID = 5;

    int counterSpellcasterID;

    private void Start()
    {
        spellcasterList = NetworkGameState.instance.spellcasterList;
        list_AllEvents = new Dictionary<int, GlobalEvent>();
        
        if(spellcasterList[elementalistID] > 0)
        {
            GlobalEvent tsunami = new GlobalEvent("Tsunami", GlobalEventDescriptions.tsunamiDesc, 2, Tsunami);
            list_AllEvents[2] = tsunami;
        }
        



       
    }

    //Call this one for generic events
    public void executeGlobalEvent()
    {
        // Update list just in case someone left/died.
        spellcasterList = NetworkGameState.instance.spellcasterList;

        int size = list_AllEvents.Count;
        //Get a random function from the list of possible events and execute it.
        Action evnt = list_AllEvents[(int)Random.Range(0, (float)size - 1)].action;
        evnt();
    }


    public void GlobalEventCounter(int evntID, bool isCountered)
    {
        if (isCountered)
        {
            // Give players feedback that their spellCaster friend saved them.
            //LobbyManager.s_Singleton.LetEveryoneKnowCountered();
        }
        else
        {
            switch (evntID)
            {
                case 2:
                    dealPercentDamageToAllSpellcasters(0.5f);
                    break;
            }
        }
    }

    private void dealPercentDamageToAllSpellcasters(float percent)
    {
        for (int id = 0; id < spellcasterList.Length; id++)
        {
            if (spellcasterList[id] > 0)
            {
                LobbyManager.s_Singleton.DealPercentDamage(id, percent);
            }
        }
    }

    //ID = 2
    private void Tsunami()
    {
        counterSpellcasterID = elementalistID;
        LobbyManager.s_Singleton.CheckIfCanCounter(counterSpellcasterID, 0, 2);
    }

    //ID = 
    private void GilronsCornet()
    {

    }

    //ID = 
    private void StonelungPlague ()
    {

    }

    //ID = 
    private void DivineIntervention()
    {

    }

    //ID = 
    private void AmbassadorOfTheRealm()
    {

    }

    //ID = 
    private void Famine()
    {

    }

   
}
