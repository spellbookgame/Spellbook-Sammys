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
    public string requiredClass = "";
    public int requiredSpellTier = 0;
    public int roundsUntilCrisis = 0;

    // tracking each crisis to see which one is currently active
    public string currentCrisis = "";

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
        crisisSolved = false;
        roundsUntilCrisis = 3;

        crisisName = "Tsunami";
        requiredLocation = "capital";   // change to forest eventually
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
        crisisSolved = false;
        roundsUntilCrisis = 3;

        crisisName = "Stonelung Plague";
        requiredLocation = "Capital";
        requiredClass = "Alchemist";
        requiredSpellTier = 2;

        crisisDetails = "Alchemist must go to the capital with a TIER 2 spell unlocked.";
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
                        // if alchemist has a tier 2 spell collected and is in the capital
                        if (player.Spellcaster.chapter.spellsCollected.Any(x => x.iTier == 2) && location.Equals(requiredLocation))
                        {
                            PanelHolder.instance.displayNotify("Plague Averted", "Congratulations! Your Alchemist stopped the plague from spreading.", "OK");
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
}
