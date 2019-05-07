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

    #region tsunami
    // call this to alert players of new crisis
    public void CallTsunami()
    {
        currentCrisis = "Tsunami";
        nextCrisis = "Gilron's Comet";
        crisisSolved = false;
        roundsUntilCrisis = 3;

        crisisName = "Tsunami";
        requiredLocation = "location_forest";
        requiredClass = "Elementalist";
        requiredSpellTier = 3;

        crisisDetails = "Elementalist must go to the Forest with a TIER 3 spell unlocked.";
        crisisConsequence = "All wizards will lose half their HP. Towns will be closed next round.";
        crisisReward = "All wizards will receive the highest tier rune from their respective class.";

        PanelHolder.instance.displayCrisis("Crisis Alert: Tsunami", roundsUntilCrisis);
    }

    // call this when crisis arrives (if roundsUntilCrisis == 0)
    public void FinishTsunami()
    {
        // if(crisisSolved) give rewards
        // if(!crisisSolved) give consequence
        currentCrisis = "";
    }
    #endregion

    #region comet
    public void CallComet()
    {
        currentCrisis = "Comet";
        nextCrisis = "Stonelung Plague";
        crisisSolved = false;
        roundsUntilCrisis = 3;

        crisisName = "Gilron's Comet";
        requiredLocation = "Town";  
        requiredClass = "Arcanist";
        requiredSpellTier = 2;

        crisisDetails = "Arcanist must go to any town with a TIER 2 spell unlocked.";
        crisisConsequence = "All wizards will lose 15 HP. Mana flow will be cut off next round.";
        crisisReward = "All towns will be able to hold 2 runes.";

        PanelHolder.instance.displayCrisis("Crisis Alert: Gilron's Comet", roundsUntilCrisis);
    }
    // call this when crisis arrives (if roundsUntilCrisis == 0)
    public void FinishComet()
    {
        // if(crisisSolved) give rewards
        // if(!crisisSolved) give consequence
        currentCrisis = "";
    }
    #endregion

    #region plague
    public void CallPlague()
    {
        currentCrisis = "Plague";
        nextCrisis = "Divine Intervention";
        crisisSolved = false;
        roundsUntilCrisis = 3;

        crisisName = "Stonelung Plague";
        requiredLocation = "location_capital";
        requiredClass = "Alchemist";
        requiredSpellName = "Brew - Distilled Potion";

        crisisDetails = "Alchemist must go to the capital with Brew - Distilled Potion spell unlocked.";
        crisisConsequence = "All wizards will not be able to cast spells next round. Swamp will be closed for the next 2 rounds.";
        crisisReward = "All wizards will earn a D6.";

        PanelHolder.instance.displayCrisis("Crisis Alert: Stonelung Plague", roundsUntilCrisis);
    }

    // call this when crisis arrives (if roundsUntilCrisis == 0)
    public void FinishPlague()
    {
        // if(crisisSolved) give rewards
        // if(!crisisSolved) give consequence
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

    #region checkCrisis
    // call this to check if crisis is resolved
    public void CheckCrisis(Player player, string currentCrisis, string location)
    {
        if (!crisisSolved)
        {
            switch (currentCrisis)
            {
                case "Tsunami":
                    if (player.Spellcaster.classType.Equals("Elementalist"))
                    {
                        // if elementalist has a tier 3 spell collected and is in the forest
                        if (player.Spellcaster.chapter.spellsCollected.Any(x => x.iTier == 3) && location.Equals(requiredLocation))
                        {
                            PanelHolder.instance.displayNotify("Tsunami Averted", "Congratulations! Your Elementalist stopped the tsunami from destroying our lands.", "OK");
                            crisisSolved = true;
                        }
                    }
                    break;
                case "Comet":
                    if (player.Spellcaster.classType.Equals("Arcanist"))
                    {
                        // if arcanist has a tier 2 spell collected and is in a town
                        if (player.Spellcaster.chapter.spellsCollected.Any(x => x.iTier == 2))
                        {
                            if (location.Equals("town_alchemist") || location.Equals("town_arcanist") || location.Equals("town_chronomancer")
                                || location.Equals("town_elementalist") || location.Equals("town_illusionist") || location.Equals("town_summoner"))
                            {
                                PanelHolder.instance.displayNotify("Comet Averted", "Congratulations! Your Arcanist stopped the comet just in time.", "OK");
                                crisisSolved = true;
                            }
                        }
                    }
                    break;
                case "Plague":
                    if (player.Spellcaster.classType.Equals("Alchemist"))
                    {
                        // if alchemist has required spell collected and is in the capital
                        if (player.Spellcaster.chapter.spellsCollected.Any(x => x.sSpellName.Equals(requiredSpellName)) && location.Equals(requiredLocation))
                        {
                            PanelHolder.instance.displayNotify("Plague Averted", "Congratulations! Your Alchemist stopped the plague from spreading.", "OK");
                            crisisSolved = true;
                        }
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
                // TODO - check for preparation (networking)
                default:
                    break;
            }
        }
    }
    #endregion
}