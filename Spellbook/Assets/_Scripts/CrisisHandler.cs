using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrisisHandler : MonoBehaviour
{
    public static CrisisHandler instance = null;

    public string requiredLocation = "";
    public string requiredClass = "";
    public int requiredSpellTier = 0;

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

    // call this to alert players of new crisis
    public void CallTsunami()
    {
        tsunamiActive = true;
        requiredLocation = "Forest";
        requiredClass = "Elementalist";
        requiredSpellTier = 3;
    }

    // call this when crisis is resolved early
    public void ResolveTsunami()
    {
        PanelHolder.instance.displayNotify("Tsunami Averted", "Congratulations! Your Elementalist stopped the tsunami from destroying our lands.", "OK");
        tsunamiActive = false;
    }

    // call this when crisis arrives
    public void FinishTsunami(bool isSolved)
    {
        // if(isSolved) give rewards
        // if(!isSolved) give consequence
        tsunamiActive = false;
    }
}
