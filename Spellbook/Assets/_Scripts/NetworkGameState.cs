using Bolt.Samples.Photon.Lobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*TODO.
 * 1. int the orb fill percentage (int from 1 -100 preferably) //DONE
2. int team's total tap count  //DONE
3. float or int the time given to charge the orb  //DONE
4. int player's total damage output (before applying multipliers)
5. Access/Deal dmg to boss*/  //DONE

/**
 Written by Moises Martinez
    A class that keeps track of player's, and the gamestate such as turns and turnorder, global
    events (crisis).
     */
public class NetworkGameState : Bolt.EntityEventListener<IGameState>
{
    public static NetworkGameState instance = null;
    public GlobalEvents globalEvents;

    public int numOfPlayers = 0;  //Players in lobby
    public int numOfSpellcasters = 0;

    // each index in this array corresponds to the 6 spellcaster ids
    // if index i has value 0:  spellcaster is not taken
    // if index i has value 1:  spellcaster is taken
    public int[] spellcasterList;

    //NOTE: This array is not used for the Sammys.
    //TODO (after graduation): Turn based 
    // each index in this array correspond to the 6 spellcaster ids
    // if index i has value 0:       this spellcaster is not in the game
    // if index i has a value 1-6:   this spellcaster attacks after i-1 spellcasters
    // public int[] combatOrder;


    public int yearsUntilNextEvent;
    public string nextGlobalEvent;
    const string sYearsUntilNext = " years until next ";
    private List<int> turnOrder;
    public int turn_i = 0;  //index counter for List<int> turnOrder
    public int totalYearsSoFar = 0;

    bool needToNotifyPlayersNewEvent = false;
    bool savedByHero = false;



    // Bolt's version of the Unity's Start()
    public override void Attached()
    {
        BoltConsole.Write("Attached - NetworkGameState");
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        DontDestroyOnLoad(this);

        if (entity.isOwner)
        {
            spellcasterList = new int[6];

            //state.SpellcasterList[0] = 0;
            //combatOrder = new int[6];
            state.BossMaxHealth = 30f;
            state.BossHealth = 30f;

            state.TapSecondsAllowed = 8f;
            turnOrder = new List<int>();

        }
    }

    public void setNextEvent(string evnt, string desc, int years)
    {
        //nextGlobalEvent = globalEvents.GetNextEvent();
        state.NextGlobalEventName = evnt;
        state.NextGlobalEventDesc = desc;
        state.YearsUntilNextEvent = years;
        yearsUntilNextEvent = years;
    }

    public string getMatchName()
    {
        return state.MatchName;
    }

    public string getGlobalEventString()
    {
        //return turnsUntilNextEvent + displayGlobalEvent + nextGlobalEvent;
        return state.YearsUntilNextEvent + sYearsUntilNext + state.NextGlobalEventName;
    }

    public int RoundsUntilCrisisActivates()
    {
        return state.YearsUntilNextEvent;
    }

    public void ModifyRoundsUntilNextCrisis(int x)
    {
        state.YearsUntilNextEvent += x;
        if (state.YearsUntilNextEvent < 0)
        {
            state.YearsUntilNextEvent = 0;
        }

    }


    public string getEventInfo()
    {
        //string evntName = state.NextGlobalEventName;
        return state.NextGlobalEventName + "\n" + state.NextGlobalEventDesc;
    }

    public string getCurrentCrisis()
    {
        return state.NextGlobalEventName;
    }

    public void onCreateRoom(string matchName)
    {
        state.MatchName = matchName;
    }

    public int onPlayerJoined()
    {
        numOfPlayers++;
        state.NumOfPlayers++;
        return state.NumOfPlayers;
    }

    public int numOfPlayersInGame()
    {
        return state.NumOfPlayers;
    }

    public int numOfSpellcastersInGame()
    {
        return state.NumOfSpellcasters;
    }

    public bool allPlayersSelected()
    {
        if (state.NumOfPlayers - state.NumOfSpellcasters > 0)
        {
            return false;
        }
        return true;
    }

    public int actuallyAReturningPlayer()
    {
        numOfPlayers--;
        state.NumOfPlayers--;
        return state.NumOfPlayers;
    }

    //input: the >>permanently<< disconnected spellcaster ID
    public void onRemovePlayer(int sID)
    {
        BoltConsole.Write("Removing spellcasterID " + sID);
        numOfPlayers--;
        state.NumOfPlayers--;
        //combatOrder[sID] = 0;
        spellcasterList[sID] = 0;
        bool currentDisconnected = false;
        int curSpellcasterID = state.CurrentSpellcasterTurn;
        if (curSpellcasterID == sID)
        {
            currentDisconnected = true;
            curSpellcasterID = getNextTurn();
        }

        //Refresh the turn order list
        determineTurnOrder(curSpellcasterID);

        //Update the pointer
        for (int i = 0; i < turnOrder.Count; i++)
        {
            if (turnOrder[i] == curSpellcasterID)
            {
                turn_i = i;
            }
        }
        state.CurrentSpellcasterTurn = turnOrder[turn_i];
        BoltConsole.Write(state.CurrentSpellcasterTurn + "==" + curSpellcasterID);
        if (currentDisconnected)
        {


            BoltConsole.Write("The turn was the spellcaster that logged out");
            var nextTurnEvnt = NotifyTurnEvent.Create(Bolt.GlobalTargets.OnlyServer);
            nextTurnEvnt.Send();

        }
        //If the current turn spellcaster is the one that disconnected, notify the next spellcaster
    }

    public void onSpellcasterSelected(int spellcasterID, int previous)
    {
        /*if (previous > -1)
        {
            spellcasterList[previous] = 0;
            state.SpellcasterList[previous] = 0;

        }
        else
        {*/
        numOfSpellcasters++;
        state.NumOfSpellcasters++;
        //}
        spellcasterList[spellcasterID] = 1;
        state.SpellcasterList[spellcasterID] = 1;
        determineTurnOrder(-1);
        //globalEvents.determineGlobalEvents();
    }

    public void onSpellcasterCanceled(int spellcasterID)
    {
        spellcasterList[spellcasterID] = 0;
        state.SpellcasterList[spellcasterID] = 0;
        numOfSpellcasters--;
        state.NumOfSpellcasters--;
        determineTurnOrder(-1);
    }

    public Bolt.NetworkArray_Integer GetSpellcasterList()
    {
        return state.SpellcasterList;
    }

    public void onCollectedSpell(int spellcasterId, string spellName)
    {
        switch (spellcasterId)
        {
            case 0:
                updateStateArray(state.AlchemistProgress, spellName);
                break;
            case 1:
                updateStateArray(state.ArcanistProgress, spellName);
                break;
            case 2:
                updateStateArray(state.ElementalistProgress, spellName);
                break;
            case 3:
                updateStateArray(state.ChronomancerProgress, spellName);
                break;
            case 4:
                updateStateArray(state.TricksterProgress, spellName);
                break;
            case 5:
                updateStateArray(state.SummonerProgress, spellName);
                break;
        }
    }

    private void updateStateArray(Bolt.NetworkArray_String spellProgress, string spellName)
    {
        for (int i = 0; i < spellProgress.Length; i++)
        {
            if (spellProgress[i] == null)
            {
                spellProgress[i] = spellName;
                break;
            }
            if (spellProgress[i] == spellName)
            {
                break;
            }
        }
    }

    //Input is -1 if we want the first spellcaster in the list to go first.
    //Temporary hack ^
    public void determineTurnOrder(int sID)
    {
        turnOrder.Clear();
        for (int i = 0; i < spellcasterList.Length; i++)
        {
            if (spellcasterList[i] > 0)
            {
                turnOrder.Add(i);
            }
        }
        if (sID == -1 && turnOrder.Count > 0)
        {
            state.CurrentSpellcasterTurn = turnOrder[0];
        }
        else if (turnOrder.Count <= 0)
        {
            state.CurrentSpellcasterTurn = -1;
        }
    }

    public void SavedByHero(string crisisName, string heroClass)
    {
        savedByHero = true;
        currentCrisis = crisisName;
        hero = heroClass;
    }

    public void ResetSavedByHero()
    {
        savedByHero = false;
        currentCrisis = "";
        hero = "";
    }

    /* When our NetworkManager (aka our GlobalEventListener) recieves a
     NextTurnEvent, this method is called.*/
    //These fields below are here because they are only used only in this function.
    bool PeacefulYear = false;
    bool GlobalEventHappened = false;
    string currentCrisis = "";
    string hero = "";
    public int startNewTurn()
    {
        turn_i++;

        //If everyone moved this turn, then a year/round has passed. 
        if (turn_i >= turnOrder.Count)
        {
            if (state.YearsUntilNextEvent <= 1 && !GlobalEventHappened && !savedByHero)
            {
                BoltConsole.Write("Global EVENT happening!!");
                GlobalEventHappened = true;
                globalEvents.executeGlobalEvent();
                //needToNotifyPlayersNewEvent = true;
            }
            else if (state.YearsUntilNextEvent <= 1 && !GlobalEventHappened && savedByHero)
            {
                NetworkManager.s_Singleton.ResolveCrisis(currentCrisis, hero);
                GlobalEventHappened = true;
            }
            BoltConsole.Write("NEW ROUND!!!!!");
            turn_i = 0;
            if (!globalEvents.AllCrisisHappened())
            {
                yearsUntilNextEvent--;
                state.YearsUntilNextEvent--;
            }
            else
            {
                //Temporary hack.
                yearsUntilNextEvent = 100;
                state.YearsUntilNextEvent = 100;
            }


            if (PeacefulYear)
            {
                BoltConsole.Write("Peaceful year ended");
                PeacefulYear = false;
                GlobalEventHappened = false;
                bool crisisPrepared = globalEvents.PrepareNextEvent();
                if (crisisPrepared)
                {
                    ResetSavedByHero();
                    DisplayNextEvent();
                }
            }

            if (GlobalEventHappened) //|| savedByHero
            {
                PeacefulYear = true;  //After the global event happends let players play a round with no crisis to prep for.
                //needToNotifyPlayersNewEvent = false;
                BoltConsole.Write("Starting Peaceful year");
            }
        }
        state.CurrentSpellcasterTurn = turnOrder[turn_i];
        return turnOrder[turn_i];
    }

    //Returns what the next turn spellcaster ID would be, and doesn't update anything.
    //Think of it as stack.peek()
    public int getNextTurn()
    {
        int nextTurn = turn_i + 1;
        //Return the beginning of the turnorder if the current spellcaster is the last one to go in the round.
        if (nextTurn >= turnOrder.Count)
        {
            return 0;
        }
        return spellcasterList[nextTurn];
    }

    public void DisplayNextEvent()
    {
        globalEvents.notifyAboutNewUpcomingEvent();
    }

    public int getCurrentTurn()
    {
        return state.CurrentSpellcasterTurn;
    }

    //Clear the itembox when a player takes an item.
    public string ClearItemBox()
    {
        string item = state.ItemForGrab;
        BoltConsole.Write("Item is Picked up : " + item);
        state.ItemForGrab = "";
        BoltConsole.Write("Item is now cleared : " + state.ItemForGrab);
        return item;
    }


    //Called when player drops an item in the Item-Scan location
    public void ItemDropOff(string item)
    {
        BoltConsole.Write("item drop off: " + item);
        state.ItemForGrab = item;
    }

    public string ItemForGrabs()
    {
        BoltConsole.Write("ItemForGrabs() : " + state.ItemForGrab);
        return state.ItemForGrab;
    }

    public string getTurnSpellcasterName()
    {

        switch (state.CurrentSpellcasterTurn)
        {
            case 0:
                return "Alchemist";
            case 1:
                return "Arcanist";
            case 2:
                return "Elementalist";
            case 3:
                return "Chronomancer";
            case 4:
                return "Illusionist";
            case 5:
                return "Summoner";
            default:
                return "Error";
        }
    }

    public void UpdateTapsForSpellcaster(int spellcasterID, int numTaps, float orbPercentage)
    {
        state.SpellcasterTaps[spellcasterID] = numTaps;
        state.SpellcasterOrbPercentages[spellcasterID] = orbPercentage;
    }

    public void IncreaseTeamTapPercentage(float percent)
    {
        for (int i = 0; i < state.SpellcasterTaps.Length; i++)
        {
            state.SpellcasterTaps[i] = state.SpellcasterTaps[i] + ((int)(state.SpellcasterTaps[i] * percent));
        }
    }

    //For combat API
    public int GetTapsForSpellcaster(int spellcasterID)
    {
        return state.SpellcasterTaps[spellcasterID];
    }

    //For combat API
    //Returns the orb value from 0-1, with 1 being "100%"
    public float GetOrbPercentageForSpellcaster(int spellcasterID)
    {
        return state.SpellcasterOrbPercentages[spellcasterID];
    }

    public void ResetTapsForSpellcaster(int spellcasterID)
    {
        state.SpellcasterTaps[spellcasterID] = 0;
    }

    //TODO: make robust (after graduation)
    public void IncreaseTapSecondsAllowed()
    {
        state.TapSecondsAllowed = 10;
    }


    //Call this when combat phase is done.
    //TODO: make robust (after graduation)
    public void ResetTapSecondsAllowed()
    {
        state.TapSecondsAllowed = 8;
    }

    //For Combat API
    public float GetTapSecondsAllowed()
    {
        return state.TapSecondsAllowed;
    }

    public void SetSpellcasterDmg(int spellcasterID, float dmg)
    {
        state.SpellcasterDamages[spellcasterID] = dmg;
    }

    //For combat API
    public float GetSpellcasterDmg(int spellcasterID)
    {
        return state.SpellcasterDamages[spellcasterID];
    }

    public void IncreaseAllyDmgByPercent(int allySpellcasterID, float percent)
    {
        state.SpellcasterDamages[allySpellcasterID]
            = state.SpellcasterDamages[allySpellcasterID] + (state.SpellcasterDamages[allySpellcasterID] * percent);
    }

    //Input float between 0-1
    public void IncreaseTeamDmgByPercent(float percent)
    {
        for (int i = 0; i < state.SpellcasterDamages.Length; i++)
        {
            state.SpellcasterDamages[i] = state.SpellcasterDamages[i] + (state.SpellcasterDamages[i] * percent);
        }
    }

    int totalSpellCount = 0;
    public void BalanceBossHp(int numSpells)
    {
        totalSpellCount += numSpells;
        if(state.NumOfSpellcasters > 3)
        {
            int dif = state.NumOfSpellcasters - 3;
            int addedHP = dif * 15;
            state.BossMaxHealth = 30f + addedHP;
        }
        else
        {
            state.BossMaxHealth = 30f; 
        }

        int buffSomeMore = totalSpellCount / 3;
        state.BossMaxHealth += (buffSomeMore * 5);
        state.BossHealth = state.BossMaxHealth;

    }

    //For combat API
    public float GetBossHealth()
    {
        return state.BossHealth / state.BossMaxHealth;
    }

    public float GetBossCurrentHealth()
    {
        return state.BossHealth;
    }

    public float GetBossMaxHealth()
    {
        return state.BossMaxHealth;
    }

    public void DealDmgToBoss(float dmg)
    {
        state.BossHealth -= dmg;
    }

    public void DealPercentDmgToBoss(float percent)
    {
        state.BossHealth = state.BossHealth - (state.BossHealth * percent);
    }

    public void IncrementAttackCount()
    {
        state.AttackCount++;

        //if everyone casted their spell, its the boss turn to attack.
        if(state.AttackCount >= state.NumOfSpellcasters)
        {
            ResetAttackCount();
            float damage = 0;
            for (int i = 0; i < state.SpellcasterDamages.Length; i++)
            {
                damage += state.SpellcasterDamages[i];

                //Reset for next combat/round.
                state.SpellcasterDamages[i] = 0;
            }
            state.BossHealth -= damage;
            if(state.BossHealth <= 0)
            {
                NetworkManager.s_Singleton.BossDies();
            }
            else
            {
                state.BossAttackDamage = Random.Range(2f, 5.1f);
                NetworkManager.s_Singleton.BossAttacksEveryone(state.BossAttackDamage);
                state.BossAttacked = true;
            }
        }
    }

    public float GetBossAttackDamage()
    {
        return state.BossAttackDamage;
    }

    public bool IfBossAttacked()
    {
        return state.BossAttacked;
    }

    public void ResetAttackCount()
    {
        state.AttackCount = 0;
        state.BossAttacked = false;
    }

    public void IncrementDeathCount()
    {
        state.DeathCount++;
        if(state.DeathCount >= state.NumOfSpellcasters)
        {
            NetworkManager.s_Singleton.GameOver();
        }
    }

    //Returns a formatted string to display in the spellbook progress scene.
    public string getSpellbookProgess()
    {
        int pCount = state.NumOfSpellcasters;
        int outOf = pCount * 4;
        int progress = 0;
        string result = "";

        if (state.SpellcasterList[0] != 0)
        {
            result += "\n Alchemist Progress:\n";
            int sCount = 0;
            for (int i = 0; i < state.AlchemistProgress.Length; i++)
            {
                if (state.AlchemistProgress[i] == null)
                {
                    break;
                }
                progress++;
                sCount++;
                result += state.AlchemistProgress[i] + " ";
            }
            result +="\n"+ sCount + " spells";
        }


        if (state.SpellcasterList[1] != 0)
        {
            result += "\n Arcanist Progress:\n";
            int sCount = 0;
            for (int i = 0; i < state.ArcanistProgress.Length; i++)
            {
                if (state.ArcanistProgress[i] == null)
                {
                    break;
                }
                progress++;
                sCount++;
                result += state.ArcanistProgress[i] + " ";
            }
            result += "\n" + sCount + " spells";
        }

        if (state.SpellcasterList[2] != 0)
        {
            result += "\n\n Elementalist Progress:\n";
            int sCount = 0;
            for (int i = 0; i < state.ElementalistProgress.Length; i++)
            {
                if (state.ElementalistProgress[i] == null)
                {
                    break;
                }
                progress++;
                sCount++;
                result += state.ElementalistProgress[i] + " ";
            }
            result += "\n" + sCount + " spells";
        }

        if (state.SpellcasterList[3] != 0)
        {
            result += "\n\n Time Wizard Progress:\n";
            int sCount = 0;
            for (int i = 0; i < state.ChronomancerProgress.Length; i++)
            {
                if (state.ChronomancerProgress[i] == null)
                {
                    break;
                }
                progress++;
                sCount++;
                result += state.ChronomancerProgress[i] + " ";
            }
            result += "\n" + sCount + " spells";
        }

        if (state.SpellcasterList[4] != 0)
        {
            result += "\n\n Trickster Progress:\n";
            int sCount = 0;
            for (int i = 0; i < state.TricksterProgress.Length; i++)
            {
                if (state.TricksterProgress[i] == null)
                {
                    break;
                }
                progress++;
                sCount++;
                result += state.TricksterProgress[i] + " ";
            }
            result += "\n" + sCount + " spells";
        }

        if (state.SpellcasterList[5] != 0)
        {
            result += "\n\n Summoner Progress:\n";
            int sCount = 0;
            for (int i = 0; i < state.SummonerProgress.Length; i++)
            {
                if (state.SummonerProgress[i] == null)
                {
                    break;
                }
                progress++;
                sCount++;
                result += state.SummonerProgress[i] + " ";
            }
            result += "\n" + sCount + " spells";
        }




        result += "\n\n Total Progress \n" + progress + " out of " + outOf + " Spells \n";
        return result;
    }
}
