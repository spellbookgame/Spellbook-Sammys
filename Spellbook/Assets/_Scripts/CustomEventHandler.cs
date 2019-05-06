using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

// for scanning board spaces
public class CustomEventHandler : MonoBehaviour, ITrackableEventHandler
{
    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;

    private Coroutine coroutineReference;
    private bool CR_running;

    Player localPlayer;
    
    void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            // basically, wait x seconds before it'll start scanning the target
            coroutineReference = StartCoroutine(ScanTime());
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }
    protected virtual void OnTrackingFound()
    {
        // only scan item if player hasn't scanned a space this turn
        if(!localPlayer.Spellcaster.scannedSpaceThisTurn && UICanvasHandler.instance.spacesMoved > 0)
            scanItem(mTrackableBehaviour.TrackableName);
        else if(localPlayer.Spellcaster.scannedSpaceThisTurn)
        {
            PanelHolder.instance.displayNotify("","You already scanned a location this turn.", "Main");
        }
        else if(UICanvasHandler.instance.spacesMoved <= 0)
        {
            PanelHolder.instance.displayNotify("", "You can't scan a location if you haven't moved.", "Main");
        }
    }

    protected virtual void OnTrackingLost()
    {
        if(CR_running)
        {
            StopCoroutine(coroutineReference);
        }
    }

    IEnumerator ScanTime()
    {
        Debug.Log("coroutine running");
        CR_running = true;
        yield return new WaitForSeconds(1f);
        CR_running = false;

        OnTrackingFound();
    }
    
    private void scanItem(string trackableName)
    {
        // check for crises
        CrisisHandler.instance.CheckCrisis(localPlayer, CrisisHandler.instance.currentCrisis, trackableName);

        // check for quests
        QuestTracker.instance.TrackErrandQuest(trackableName);

        // call function based on target name
        switch (trackableName)
        {
            #region town_spaces
            case "town_alchemist":
                SceneManager.LoadScene("AlchemyTownScene");
                break;

            case "town_arcanist":
                SceneManager.LoadScene("ArcaneTownScene");
                break;

            case "town_chronomancer":
                SceneManager.LoadScene("ChronomancyTownScene");
                break;

            case "town_elementalist":
                SceneManager.LoadScene("ElementalTownScene");
                break;

            case "town_illusionist":
                SceneManager.LoadScene("IllusionTownScene");
                break;

            case "town_summoner":
                SceneManager.LoadScene("SummonerTownScene");
                break;
            #endregion

            #region locations
            case "location_mines":
                SceneManager.LoadScene("MineScene");
                break;
            case "location_swamp":
                SceneManager.LoadScene("SwampScene");
                break;
            case "location_forest":
                SceneManager.LoadScene("ForestScene");
                break;
            case "location_capital":
                SceneManager.LoadScene("ShopScene");
                break;
            #endregion

            default:
                break;
        }
        localPlayer.Spellcaster.scannedSpaceThisTurn = true;
    }
}