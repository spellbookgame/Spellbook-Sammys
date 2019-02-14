using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Bolt.EntityEventListener<ISpellcasterState>
{
    //TODO: Change to private when done testing.
    public bool bIsMyTurn = false;
    
    //The player's chosen spellcaster class.
    private SpellCaster spellcaster;

    // References to the turn order.
    private int currentTurn = 0;
    private ArrayList spellcasterTurnOrder;

    // Remains -1 if you do not control this player.
    // TODO: Change to private when done testing.
    public int spellcasterClass = -1;

    public SpellCaster Spellcaster {
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
    public void setup(int spellcasterID)
    {
        if (entity.isOwner)
        {
            spellcasterClass = spellcasterID;
            BoltConsole.Write("Initialized LocalPlayer with Spellcaster ID " + spellcasterID);
            state.SpellcasterClass = spellcasterID;
            chooseSpellcaster(spellcasterID);
            spellcasterTurnOrder = new ArrayList();
            StartCoroutine(determineTurnOrder());
            gameObject.tag = "LocalPlayer";
        }
    }

    /*Waits for everyone to join the local scene, then 
     calculates turns.
     Turns go in ascending order based on spellcaster IDs.
     TODO:  Maybe switch up the turn order later. */
    IEnumerator determineTurnOrder()
    {
        //BoltConsole.print("Time start: " +Time.time);
        yield return new WaitForSeconds(3);
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
        if (spellcasterClass == (int)spellcasterTurnOrder[0])
        {
            BoltConsole.Write("My Turn");
            bIsMyTurn = true;
            PanelHolder panelHolder = GameObject.Find("PanelHolder").GetComponent<PanelHolder>();
            panelHolder.displayYourTurn();
        }
        //BoltConsole.print("Time done: " + Time.time);
        
    }

    private void chooseSpellcaster(int num)
    {
        switch (num)
        {
            case 0:
                spellcaster = new Alchemist();
                break;
            case 1:
                spellcaster = new Arcanist();
                break;
            case 2:
                spellcaster = new Elementalist();
                break;
            case 3:
                spellcaster = new Chronomancer();
                break;
            case 4:
                spellcaster = new Trickster();
                break;
            case 5:
                spellcaster = new Summoner();
                break;
        }
    }
    #region turn_handlers
    /* Called when player clicks the end turn button. 
     Sends a reliable Global event letting everyone know.*/
    public void onEndTurnClick()
    {
        BoltConsole.Write("OnEndTurnClick");
        if (bIsMyTurn)
        {
            BoltConsole.Write("My Turn is over");
            var nextTurnEvnt = NextPlayerTurnEvent.Create(Bolt.GlobalTargets.Everyone);
            nextTurnEvnt.NextSpellcaster = "Next";
            nextTurnEvnt.Send();
        }
    }

    /* When our LobbyManager (aka our GlobalEventListener) recieves a
     NextTurnEvent, this method is called.
     The second if-statement does nothing if its not this player's turn.*/
    public void nextTurnEvent()
    {
        BoltConsole.Write("NextTurnEvent()");
        currentTurn++;
        if (currentTurn > spellcasterTurnOrder.Count - 1) currentTurn = 0;
        if((int) spellcasterTurnOrder[currentTurn] == spellcasterClass)
        {
            BoltConsole.Write("Its my turn.");
            bIsMyTurn = true;
            PanelHolder panelHolder = GameObject.Find("PanelHolder").GetComponent<PanelHolder>();
            panelHolder.displayYourTurn();
        }
    }
    #endregion

    #region button_class_clicks
    /*
     Class choosing.  This region may move to another file 
     in the future, possibly a singleton (GameManager).
        

    public void onClickAclhemist()
    {
        spellcaster = new Alchemist();
        Debug.Log("Local Player chose " + spellcaster.classType);
    }

    public void onClickElementalist()
    {
        spellcaster = new Elementalist();
        Debug.Log("Local Player chose " + spellcaster.classType);
    }

    public void onClickArcanist()
    {
        spellcaster = new Arcanist();
        Debug.Log("Local Player chose " + spellcaster.classType);

    }

    public void onClickChronomancer()
    {
        spellcaster = new Chronomancer();
        Debug.Log("Local Player chose " + spellcaster.classType);

    }

    public void onClickTrickster()
    {
        spellcaster = new Trickster();
        Debug.Log("Local Player chose " + spellcaster.classType);

    }

    public void onClickSummoner()
    {
        spellcaster = new Summoner();
        Debug.Log("Local Player chose " + spellcaster.classType);

    }
    */
    #endregion

}

