using Bolt.Samples.Photon.Lobby;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Bolt.EntityEventListener<ISpellcasterState>
{

    //TODO: Change to private when done testing.
    public bool bIsMyTurn = false;

    //The player's chosen spellcaster class.
    public SpellCaster spellcaster;

    // References to the turn order for all players combined.
    private int currentTurn = 0;
    private int numTurnsIHad = 0;
    private ArrayList spellcasterTurnOrder;

    // Remains -1 if you do not control this player.
    // TODO: Change to private when done testing.
    public int spellcasterID = -1;

    public SpellCaster Spellcaster
    {
        get => spellcaster;
        set => spellcaster = value;
    }

    // Bolt's version of the Unity's Start()
    public override void Attached()
    {
        BoltConsole.Write("Attached");

        // Prevents player object from being destroyed when switching scenes.
        DontDestroyOnLoad(gameObject);
    }

    /* Setup for play */
    public void setup(int id)
    {
        if (entity.isOwner)
        {
            chooseSpellcaster(id);
            BoltConsole.Write("Initialized LocalPlayer with Spellcaster ID " + spellcasterID);
            state.SpellcasterClass = spellcasterID;



            spellcasterTurnOrder = new ArrayList();
            //StartCoroutine(determineTurnOrder());
            gameObject.tag = "LocalPlayer";

            if (spellcasterID == NetworkGameState.instance.getCurrentTurn())
            {
                BoltConsole.Write("My Turn");
                numTurnsIHad++;
                spellcaster.NumOfTurnsSoFar = numTurnsIHad;
                bIsMyTurn = true;
                PanelHolder panelHolder = GameObject.Find("PanelHolder").GetComponent<PanelHolder>();
                panelHolder.displayYourTurn();
            }

        }
    }

    /*Waits for everyone to join the local scene, then 
     calculates turns.
     Turns go in ascending order based on spellcaster IDs.
     TODO:  Maybe switch up the turn order later. */
    IEnumerator determineTurnOrder()
    {
        //BoltConsole.print("Time start: " +Time.time);
        yield return new WaitForSeconds(2);
        int count = 0;
        BoltConsole.Write("Entities: ");
        foreach (var e in BoltNetwork.Entities)
        {

            BoltConsole.Write("count =" + count++);
            int eClass;
            if (e.StateIs(typeof(ISpellcasterState)))
            {
                eClass = e.GetState<ISpellcasterState>().SpellcasterClass;
                BoltConsole.Write(", " + eClass);
                spellcasterTurnOrder.Add(eClass);
            }
        }
        spellcasterTurnOrder.Sort();
        string listIds = "";
        for (int i = 0; i < spellcasterTurnOrder.Count; i++)
        {
            listIds = listIds + ", " + spellcasterTurnOrder[i];
        }
        BoltConsole.Write("All SpellcasterIds: " + listIds);
        if (spellcasterID == (int)spellcasterTurnOrder[0])
        {
            BoltConsole.Write("My Turn");
            numTurnsIHad++;
            spellcaster.NumOfTurnsSoFar = numTurnsIHad;
            bIsMyTurn = true;
            PanelHolder panelHolder = GameObject.Find("PanelHolder").GetComponent<PanelHolder>();
            panelHolder.displayYourTurn();
        }
        //saveData();
        //BoltConsole.print("Time done: " + Time.time);

    }

    private void chooseSpellcaster(int num)
    {
        bool loaded = false;
        switch (num)
        {
            case -1:
                spellcaster = SpellCaster.loadPlayerData();
                spellcasterID = spellcaster.spellcasterID;
                BoltConsole.Write("Loading data");

                foreach (var e in BoltNetwork.Entities)
                {
                    if (e.StateIs(typeof(IGameState)))
                    {
                        BoltConsole.Write("Found GameState");
                        nextTurnEvent(e.GetState<IGameState>().CurrentSpellcasterTurn);
                    }
                }
                loaded = true;
                break;
            case 0:
                spellcaster = new Alchemist();
                spellcasterID = 0;
                break;
            case 1:
                spellcaster = new Arcanist();
                spellcasterID = 1;
                break;
            case 2:
                spellcaster = new Elementalist();
                spellcasterID = 2;
                break;
            case 3:
                spellcaster = new Chronomancer();
                spellcasterID = 3;
                break;
            case 4:
                spellcaster = new Trickster();
                spellcasterID = 4;
                break;
            case 5:
                spellcaster = new Summoner();
                spellcasterID = 5;
                break;
        }
        if (!loaded)
        {
            SpellCaster.savePlayerData(spellcaster);
        }

    }



    #region turn_handlers

    /* Called when player clicks the end turn button. 
     Sends a reliable Global event letting everyone know.
     
        THIS METHOD ASSUMES YOU ARE IN A SCENE WITH SceneScript.
         */
    public bool onEndTurnClick()
    {
        BoltConsole.Write("OnEndTurnClick");
        if (bIsMyTurn)
        {
            bIsMyTurn = false;
            BoltConsole.Write("My Turn is over");
            var nextTurnEvnt = NextTurnEvent.Create(Bolt.GlobalTargets.OnlyServer);
            nextTurnEvnt.Send();
            SpellCaster.savePlayerData(spellcaster);
            return true;
        }
        return false;
    }

    /* When our LobbyManager (aka our GlobalEventListener) recieves a
     NextTurnEvent, this method is called.
     The if-statement does nothing if its not this player's turn.*/
    public void nextTurnEvent(int sID)
    {
        if (sID == spellcasterID)
        {
            BoltConsole.Write("Its my turn.");
            numTurnsIHad++;
            spellcaster.NumOfTurnsSoFar = numTurnsIHad;
            bIsMyTurn = true;
            //PanelHolder panelHolder = GameObject.Find("PanelHolder").GetComponent<PanelHolder>();
            PanelHolder.instance.displayYourTurn();
        }
    }
    #endregion

}

