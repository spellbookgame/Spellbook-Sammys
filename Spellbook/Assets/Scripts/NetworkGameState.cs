using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkGameState : Bolt.EntityEventListener<IGameState>
{
    public int numOfPlayers = 0;  //Players in lobby
    public int numOfSpellcasters = 0;

    // each index in this array corresponds to the 6 spellcaster ids
    // if index i has value 0:  spellcaster is not taken
    // if index i has value 1:  spellcaster is taken
    public int[] spellcasterList;

    // each index in this array correspond to the 6 spellcaster ids
    // if index i has value 0:       this spellcaster is not in the game
    // if index i has a value 1-6:   this spellcaster attacks after i-1 spellcasters
    public int[] combatOrder;


    public int turnsUntilNextEvent;
    public string nextGlobalEvent;
    const string displayGlobalEvent = " years until next ";
    private List<int> turnOrder;
    public int turn_i = 0;  //

    // Bolt's version of the Unity's Start()
    public override void Attached()
    {
        BoltConsole.Write("Attached - NetworkGameState");
        DontDestroyOnLoad(this);

        if (entity.isOwner)
        {
            spellcasterList = new int[6];
            //state.SpellcasterList[0] = 0;
            combatOrder = new int[6];
            turnsUntilNextEvent = 3;  // TODO: rewrite.
            nextGlobalEvent = "Dragon Attack";
            state.NextGlobalEventName = nextGlobalEvent;
            turnOrder = new List<int>();

        }
    }

    public string getGlobalEventString()
    {
        return turnsUntilNextEvent + displayGlobalEvent + nextGlobalEvent;
    }


    public void onSpellcasterSelected(int spellcasterID, int previous)
    {
        if (previous > -1)
        {
            spellcasterList[previous] = 0;
            state.SpellcasterList[previous] = 0;

        }
        else
        {
            numOfSpellcasters++;
            state.NumOfPlayers++;
            Bolt.Samples.Photon.Lobby.LobbyManager.s_Singleton.updateNumOfPlayers(numOfSpellcasters);
        }
        spellcasterList[spellcasterID] = 1;
        state.SpellcasterList[spellcasterID] = 1;
        determineTurnOrder();
    }

    public void determineTurnOrder()
    {
        turnOrder.Clear();
        for (int i = 0; i < spellcasterList.Length; i++)
        {
            if (spellcasterList[i] > 0)
            {
                turnOrder.Add(i);
            }
        }
    }

    /* When our LobbyManager (aka our GlobalEventListener) recieves a
     NextTurnEvent, this method is called.*/
    public int startNewTurn()
    {
        turnsUntilNextEvent--;
        state.TurnsUntilNextEvent--;
        if (turnsUntilNextEvent <= 0)
        {
            //Make Global Event happen (Example: dragon invasion)
            turnsUntilNextEvent = 3;
            state.TurnsUntilNextEvent = 3;
        }
        turn_i++;
        if (turn_i >= turnOrder.Count)
        {

            turn_i = 0;
        }
        state.CurrentSpellcasterTurn = turnOrder[turn_i];
        return turnOrder[turn_i];
    }

    public int getCurrentTurn()
    {
        return turnOrder[turn_i];
    }
}
