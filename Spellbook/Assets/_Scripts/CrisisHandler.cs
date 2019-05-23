using Bolt.Samples.Photon.Lobby;
using System;
using System.Linq;
using UnityEngine;

public class CrisisHandler : MonoBehaviour
{
    public static CrisisHandler instance = null;

    // public variables
    public bool crisisSolved;

    public string crisisName = "";              // these are for the Crisis panel information
    public string crisisDetails = "";
    public string crisisConsequence = "";
    public string crisisReward = "";

    public string requiredLocation = "";        // these are to track the progress/completion
    public string requiredSpellName = "";     
    public string requiredClass = "";
    public int requiredSpellTier = 0;
    public int roundsUntilCrisis = 0;

    // tracking to see which crisis is currently active/next crisis
    public string currentCrisis = "";
    public string nextCrisis = "";

    public Player player;   // defined in MainPageHandler.cs
    string spellcasterHero;  //spellcaster class that saved the day

    #region singleton
    void Awake()
    {
        //Check if there is already an instance of QuestTracker
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of QuestTracker.
            Destroy(gameObject);

        //Set QuestTracker to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    // tsunami implemented
    #region tsunami
    // call this to alert players of new crisis
    public void CallTsunami()
    {
        currentCrisis = "Tsunami";
        nextCrisis = "Plague";
        crisisSolved = false;
        roundsUntilCrisis = 3;

        crisisName = "Tsunami";
        requiredLocation = "town_elementalist";
        requiredClass = "";
        requiredSpellTier = 3;

        crisisDetails = "A Spellcaster must go to Sarissa with a TIER 3 spell unlocked.";
        crisisConsequence = "All wizards will lose half their HP. Towns will be closed next round.";
        crisisReward = "All wizards will receive the highest tier rune from their respective class.";

        PanelHolder.instance.displayCrisis("Crisis Alert: Tsunami", roundsUntilCrisis);
    }

    // call this when crisis arrives (if roundsUntilCrisis == 0)
    public void FinishTsunami()
    {
        if(crisisSolved)
        {
            PanelHolder.instance.displayBoardScan("Tsunami Averted", "Out of gratitude for saving the Empire, the Capital is rewarding each wizard with an A tier rune from their class.",
                                                    Resources.Load<Sprite>("RuneArt/" + player.Spellcaster.classType + " A Rune"), "MainPlayerScene");
        }
        else
        {
            PanelHolder.instance.displayNotify("Tsunami Disaster", "You weren't able to stop the tsunami in time. All wizards lost half HP. Towns will not be scannable next round.", "MainPlayerScene");
            player.Spellcaster.TakeDamage((int)player.Spellcaster.fMaxHealth / 2);
            player.Spellcaster.tsunamiConsequence = true;   // checked in CustomEventHandler.cs
            player.Spellcaster.tsunamiConsTurn = player.Spellcaster.NumOfTurnsSoFar;    // tsunami consequence deactivated after 1 turn has passed (endturnclick)
        }
        currentCrisis = "";
    }
    #endregion

    // comet implemented, but not in crisis list
    #region comet
    public void CallComet()
    {
        currentCrisis = "Comet";
        nextCrisis = "Plague";
        crisisSolved = false;
        roundsUntilCrisis = 3;

        crisisName = "Gilron's Comet";
        requiredLocation = "location_capital";  
        requiredClass = "";
        requiredSpellTier = 2;

        crisisDetails = "A Spellcaster must go to the Capital with a TIER 2 spell unlocked.";
        crisisConsequence = "All wizards will lose 15 HP. Mana flow will be cut off next round.";
        crisisReward = "All towns will be able to hold 2 runes.";

        PanelHolder.instance.displayCrisis("Crisis Alert: Gilron's Comet", roundsUntilCrisis);
    }
    // call this when crisis arrives (if roundsUntilCrisis == 0)
    public void FinishComet()
    {
        if (crisisSolved)
        {
            PanelHolder.instance.displayNotify("Comet Averted", "The debri from the comet blast left the towns with an influx of magical energy! Place a second rune in all towns.", "OK");
        }
        else
        {
            player.Spellcaster.TakeDamage(15);
            PanelHolder.instance.displayNotify("Comet Destruction", "You weren't able to stop the comet in time. All wizards lost 15 HP. Mana flow will be cut off next round.", "OK");
            player.Spellcaster.cometConsequence = true;     // checked in Spellcaster.cs CollectMana();
            player.Spellcaster.cometConsTurn = player.Spellcaster.NumOfTurnsSoFar;
        }
        currentCrisis = "";
    }
    #endregion

    // plague implemented
    #region plague
    public void CallPlague()
    {
        currentCrisis = "Plague";
        nextCrisis = "Boss Battle";
        crisisSolved = false;
        roundsUntilCrisis = 3;

        crisisName = "Stonelung Plague";
        requiredLocation = "location_capital";
        requiredClass = "";

        crisisDetails = "A Spellcaster must go to the capital with a TIER 2 spell unlocked.";
        crisisConsequence = "All wizards will not be able to cast spells next round. Swamp will be closed for the next round.";
        crisisReward = "All wizards will earn a permanent D6 and an Abyssal Ore.";

        PanelHolder.instance.displayCrisis("Crisis Alert: Stonelung Plague", roundsUntilCrisis);
    }

    // call this when crisis arrives (if roundsUntilCrisis == 0)
    public void FinishPlague()
    {
        if (crisisSolved)
        {
            PanelHolder.instance.displayBoardScan("Plague Averted", "The local apothecaries have gathered special talismans for each wizard for saving them from doing work. " +
                                                    "Each wizard will earn a permanent D6 and an Abyssal Ore!", Resources.Load<Sprite>("Art Assets/Items and Currency/Abyssal Ore"), "MainPlayerScene");
            player.Spellcaster.dice["D6"] += 1;
        }
        else
        {
            PanelHolder.instance.displayNotify("Plague Epidemic", "Riddled with disease, all wizards will be unable to cast spells next round. The Swamp will be closed for the next round.", "MainPlayerScene");
            player.Spellcaster.plagueConsequence = true;   // checked in CustomEventHandler.cs and SpellCollectionHandler.cs
            player.Spellcaster.plagueConsTurn = player.Spellcaster.NumOfTurnsSoFar;
        }
        currentCrisis = "";
    }
    #endregion

    #region divine_intervention
    public void CallIntervention()
    {
        currentCrisis = "Intervention";
        nextCrisis = "War of the Realms";
        crisisSolved = false;
        roundsUntilCrisis = 3;

        crisisName = "Divine Intervention";
        requiredLocation = "town_summoner";
        requiredClass = "Summoner";
        requiredSpellName = "Call of the Moon - Umbra's Eclipse";

        crisisDetails = "Summoner must go to the Summoner Town with Call of the Moon - Umbra's Eclipse unlocked.";
        crisisConsequence = "All wizards will receive 5-10 damage and must discard their bottom 2 runes.";
        crisisReward = "Another rune slot will be available in the Summoner Town!";

        PanelHolder.instance.displayCrisis("Crisis Alert: Divine Intervention", roundsUntilCrisis);
    }

    // call this when crisis arrives (if roundsUntilCrisis == 0)
    public void FinishIntervention()
    {
        // if(crisisSolved) give rewards
        // if(!crisisSolved) give consequence
        currentCrisis = "";
    }
    #endregion

    #region realm
    public void CallRealm()
    {
        currentCrisis = "Realm";
        nextCrisis = "Famine";
        crisisSolved = false;
        roundsUntilCrisis = 3;

        crisisName = "War of the Realms";
        requiredLocation = "location_forest";
        requiredClass = "Illusionist";
        requiredSpellName = "Catharsis";

        crisisDetails = "Illusionist must go to the Forest with Catharsis unlocked.";
        crisisConsequence = "All wizards will 1-10 damage. Marketplace will be closed and cities will not give quests next round.";
        crisisReward = "Each wizard will receive a high tier item.";

        PanelHolder.instance.displayCrisis("Crisis Alert: War of the Realms", roundsUntilCrisis);
    }

    // call this when crisis arrives (if roundsUntilCrisis == 0)
    public void FinishRealm()
    {
        // if(crisisSolved) give rewards
        // if(!crisisSolved) give consequence
        currentCrisis = "";
    }
    #endregion

    #region famine
    public void CallFamine()
    {
        currentCrisis = "Famine";
        nextCrisis = "Preparation";
        crisisSolved = false;
        roundsUntilCrisis = 3;

        crisisName = "Famine";
        requiredLocation = "town_chronomancer";
        requiredClass = "Chronomancer";
        requiredSpellName = "Manipulate";

        crisisDetails = "Chronomancer must go to the Time Town with Manipulate unlocked.";
        crisisConsequence = "Marketplace and all towns will be closed next round. Wizards may travel through them but may not scan spaces.";
        crisisReward = "Another rune slot in the Time town, and all wizards will receive a random item.";

        PanelHolder.instance.displayCrisis("Crisis Alert: Famine", roundsUntilCrisis);
    }

    // call this when crisis arrives (if roundsUntilCrisis == 0)
    public void FinishFamine()
    {
        // if(crisisSolved) give rewards
        // if(!crisisSolved) give consequence
        currentCrisis = "";
    }
    #endregion

    #region preparation
    public void CallPreparation()
    {
        currentCrisis = "Preparation";
        nextCrisis = "Final Battle";
        crisisSolved = false;
        roundsUntilCrisis = 3;

        crisisName = "Preparation";
        requiredLocation = "";
        requiredClass = "";
        requiredSpellName = "";

        crisisDetails = "Each wizard must have a TIER 1 spell unlocked and visit their respective towns.";
        crisisConsequence = "All wizards lose half their health and mana.";
        crisisReward = "All wizards gain 5000 mana and a charge on a random spell.";

        PanelHolder.instance.displayCrisis("Crisis Alert: Preparation", roundsUntilCrisis);
    }

    // call this when crisis arrives (if roundsUntilCrisis == 0)
    public void FinishPreparation()
    {
        // if(crisisSolved) give rewards
        // if(!crisisSolved) give consequence
        currentCrisis = "";
    }
    #endregion

    #region boss_battle
    public void CallBossBattle()
    {
        currentCrisis = "Boss Battle";
        crisisSolved = false;
        PanelHolder.instance.displayCrisis("Crisis Alert: Boss Battle", roundsUntilCrisis);
    }

    public void FinishBossBattle()
    {

    }
        #endregion

    #region checkCrisis
    // call this to check if crisis is resolved
    public void CheckCrisis(Player player, string currentCrisis, string location)
    {
        this.player = player;
        if (!crisisSolved)
        {
            switch (currentCrisis)
            {
                case "Tsunami":
                    if (player.Spellcaster.chapter.spellsCollected.Any(x => x.iTier == 3) && location.Equals(requiredLocation))
                    {
                        spellcasterHero = player.Spellcaster.classType;
                        PanelHolder.instance.displayNotify("Tsunami Averted", "Congratulations! The " + spellcasterHero + " has succeeded in stopping the tsunami!", "OK");
                        crisisSolved = true;
                    }
                    break;
                case "Comet":
                    if (player.Spellcaster.chapter.spellsCollected.Any(x => x.iTier == 2) && location.Equals(requiredLocation))
                    {
                        spellcasterHero = player.Spellcaster.classType;
                        PanelHolder.instance.displayNotify("Comet Averted", "Congratulations! The " + spellcasterHero + " has stopped the comet from destroying the land.", "OK");
                        crisisSolved = true;
                    }
                    break;
                case "Plague":
                    if (player.Spellcaster.chapter.spellsCollected.Any(x => x.iTier == 2) && location.Equals(requiredLocation))
                    {
                        spellcasterHero = player.Spellcaster.classType;
                        PanelHolder.instance.displayNotify("Plague Averted", "Congratulations! The " + spellcasterHero + " has stopped the plague from spreading.", "OK");
                        crisisSolved = true;
                    }
                    break;
                case "Intervention":
                    if (player.Spellcaster.classType.Equals("Summoner"))
                    {
                        // if summoner has required spell collected and is in the summoner town
                        if (player.Spellcaster.chapter.spellsCollected.Any(x => x.sSpellName.Equals(requiredSpellName)) && location.Equals(requiredLocation))
                        {
                            PanelHolder.instance.displayNotify("Intervention Averted", "Congratulations! Your Summoner maintained peace between the two realms.", "OK");
                            crisisSolved = true;
                        }
                    }
                    break;
                case "Realm":
                    if (player.Spellcaster.classType.Equals("Illusionist"))
                    {
                        // if illusionist has required spell collected and is in the forest
                        if (player.Spellcaster.chapter.spellsCollected.Any(x => x.sSpellName.Equals(requiredSpellName)) && location.Equals(requiredLocation))
                        {
                            PanelHolder.instance.displayNotify("War of the Realms Averted", "Congratulations! Your Illusionist maintained peace in the war in the Forest.", "OK");
                            crisisSolved = true;
                        }
                    }
                    break;
                case "Famine":
                    if (player.Spellcaster.classType.Equals("Chronomancer"))
                    {
                        // if illusionist has required spell collected and is in the forest
                        if (player.Spellcaster.chapter.spellsCollected.Any(x => x.sSpellName.Equals(requiredSpellName)) && location.Equals(requiredLocation))
                        {
                            PanelHolder.instance.displayNotify("Famine Averted", "Congratulations! Your Chronomancer manipulated time to allow crops to grow faster.", "OK");
                            crisisSolved = true;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
    #endregion

    #region NETWORK
    
    //Call this to notify network if the spellcaster solved it or not
    //Call even if they didn't solve it
    public void SolveCrisis()
    {
        if(currentCrisis!= "")
        {
            NetworkManager.s_Singleton.SolveCrisis(currentCrisis, crisisSolved, player.Spellcaster.classType);
        }
    }

    // Called from Network, everyone recieves this.
    public void CallCrisis(string CrisisName)
    {
        AllCrisisDict.CallCrisis[CrisisName]();
    }

    // Called from Network, everyone recieves this.
    public void CheckCrisisPhase1(Player p, string crisisName, string location)
    {
        CheckCrisis(p, crisisName, location);
    }

    // Called from Network, everyone recieves this.
    // finishAction is either:
    // FinishPlague()
    // FinishTsunami()
    // FinishBossBattle()
    //
    // spellcasterHero is an empty string if everyone failed to counter the crisis
    public void FinishCrisis(Action finishAction, bool isSolved, string spellcasterHero)
    {
        this.spellcasterHero = spellcasterHero;
        crisisSolved = isSolved;
        finishAction();
    }
    #endregion
}