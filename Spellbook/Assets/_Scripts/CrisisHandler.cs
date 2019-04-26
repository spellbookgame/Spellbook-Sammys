using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrisisHandler : MonoBehaviour
{
    public static CrisisHandler instance = null;

    // public variables
    public bool crisisSolved;

    public string crisisName = "";
    public string crisisDetails = "";
    public string crisisConsequence = "";
    public string crisisReward = "";
    public string requiredLocation = "";
    public string requiredClass = "";

    public int requiredSpellTier = 0;
    public int roundsUntilCrisis = 0;

    // tracking each crisis to see which one is currently active
    public bool tsunamiActive = false;

    Player localPlayer;

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

    private void Start()
    {
        localPlayer = GameObject.Find("LocalPlayer").GetComponent<Player>();
    }

    #region tsunami
    // call this to alert players of new crisis
    public void CallTsunami()
    {
        tsunamiActive = true;
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

    // call this to check if tsunami requirements are met
    public void CheckTsunami(Player player, string location)
    {
        if(tsunamiActive && !crisisSolved)
        {
            if(player.Spellcaster.classType.Equals("Elementalist"))
            {
                // if elementalist has a tier 3 spell collected and is in the forest
                if(player.Spellcaster.chapter.spellsCollected.Any(x => x.iTier == 3) && location.Equals(requiredLocation))
                {
                    ResolveTsunami();
                }
            }
        }
    }

    // call this when crisis is resolved early
    public void ResolveTsunami()
    {
        PanelHolder.instance.displayNotify("Tsunami Averted", "Congratulations! Your Elementalist stopped the tsunami from destroying our lands.", "OK");
        crisisSolved = true;
    }

    // call this when crisis arrives (if roundsUntilCrisis == 0)
    public void FinishTsunami()
    {
        // if(crisisSolved) give rewards
        // if(!crisisSolved) give consequence
        tsunamiActive = false;
    }
    #endregion
}
