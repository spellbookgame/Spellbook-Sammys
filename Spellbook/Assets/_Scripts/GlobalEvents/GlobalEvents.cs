using Bolt.Samples.Photon.Lobby;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/*
 For host device only.
     */
public class GlobalEvents : MonoBehaviour
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

    List<GlobalEvent> list_AllEvents;
    List<GlobalEvent> eventOrder;
    int currentEventIndex = 0;
    bool allEventsHappened = false;

    public int[] spellcasterList;

    int alchemistID = 0;
    int arcanistID = 1;
    int elementalistID = 2;
    int chronomancerID = 3;
    int tricksterID = 4;
    int summonerID = 5;

    int counterSpellcasterID;
    string counterSpellcasterClass;

    private void Start()
    {
        spellcasterList = NetworkGameState.instance.spellcasterList;
        list_AllEvents = new List<GlobalEvent>();
        
        if(spellcasterList[elementalistID] > 0)
        {
            GlobalEvent tsunami = new GlobalEvent("Tsunami", GlobalEventDescriptions.tsunamiDesc, 2, Tsunami);
            list_AllEvents.Add(tsunami);
        }

      

        /*Randomize event order at the start of the game*/
        Shuffler.Shuffle(list_AllEvents);
        BoltConsole.Write("Global Event Order:");
        Debug.Log("Global Event Order:");
        foreach(GlobalEvent evnt in list_AllEvents)
        {
            BoltConsole.Write(evnt.name);
            Debug.Log(evnt.name);
        }

        GlobalEvent finalBoss = new GlobalEvent("Final Boss", "Final Boss", 7, FinalBossBattle);
        list_AllEvents.Add(finalBoss);

        NetworkGameState.instance.setNextEvent(list_AllEvents[currentEventIndex].name);


    }

    public string GetNextEvent()
    {
        return list_AllEvents[currentEventIndex].name;
    }

    //Call this one for generic events
    public void executeGlobalEvent()
    {
        if (!allEventsHappened && list_AllEvents.Count > 0)
        {
            // Update list just in case someone left/died.
            spellcasterList = NetworkGameState.instance.spellcasterList;

            int size = list_AllEvents.Count;
            //Get a random function from the list of possible events and execute it.
            Action evnt = list_AllEvents[currentEventIndex].action;
            currentEventIndex++;
            if (currentEventIndex >= list_AllEvents.Count)
            {
                currentEventIndex = 0;
                //All events passed.
                allEventsHappened = true;
            }
            evnt();
        }
        
    }


    public void GlobalEventCounter(int evntID, bool isCountered)
    {
        if (isCountered)
        {
            // Give players feedback that their spellCaster friend saved them.
            Debug.Log("Global event is countered by:" + counterSpellcasterClass);
            BoltConsole.Write("Global event is countered by:" + counterSpellcasterClass);
            LobbyManager.s_Singleton.LetEveryoneKnowCountered(counterSpellcasterClass);
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

    /*Each player loses x% of their health*/
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

    private void FinalBossBattle()
    {
        //Not yet implemented until combat is done
        LobbyManager.s_Singleton.startFinalBossBattle();
    }

    //ID = 2
    private void Tsunami()
    {
        counterSpellcasterClass = "Elementalist";
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

static class Shuffler
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}