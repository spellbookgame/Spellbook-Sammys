using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

// for scanning one image combination
public class RuneEventHandler : MonoBehaviour, ITrackableEventHandler
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
            // basically, wait 3 seconds before it'll start scanning the target
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
        ScanItem(mTrackableBehaviour.TrackableName);
    }

    protected virtual void OnTrackingLost()
    {
        if (CR_running)
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

    private void ScanItem(string trackableName)
    {
        #region RuneSpells1-01
        if(trackableName.Equals("RuneSpells1-01"))
        {
            switch (localPlayer.Spellcaster.classType)
            {
                // collect spell based on class
                case "Alchemist":
                    Debug.Log("Alchemist scanned " + trackableName);
                    localPlayer.Spellcaster.CollectSpell(localPlayer.Spellcaster.chapter.spellsAllowed[3]);
                    break;
                case "Arcanist":
                    Debug.Log("Arcanist scanned " + trackableName);
                    localPlayer.Spellcaster.CollectSpell(localPlayer.Spellcaster.chapter.spellsAllowed[3]);
                    break;
                case "Chronomancer":
                    Debug.Log("Chronomancer scanned " + trackableName);
                    localPlayer.Spellcaster.CollectSpell(localPlayer.Spellcaster.chapter.spellsAllowed[2]);
                    break;
                case "Elementalist":
                    Debug.Log("Elementalist scanned " + trackableName);
                    localPlayer.Spellcaster.CollectSpell(localPlayer.Spellcaster.chapter.spellsAllowed[2]);
                    break;
                case "Summoner":
                    Debug.Log("Summoner scanned " + trackableName);
                    localPlayer.Spellcaster.CollectSpell(localPlayer.Spellcaster.chapter.spellsAllowed[2]);
                    break;
                case "Trickster":
                    Debug.Log("Trickster scanned " + trackableName);
                    localPlayer.Spellcaster.CollectSpell(localPlayer.Spellcaster.chapter.spellsAllowed[2]);
                    break;
            }
        }
        #endregion
        #region RuneSpells2-01
        else if (trackableName.Equals("RuneSpells2-01"))
        {
            switch (localPlayer.Spellcaster.classType)
            {
                // collect spell based on class
                case "Alchemist":
                    Debug.Log("Alchemist scanned " + trackableName);
                    localPlayer.Spellcaster.CollectSpell(localPlayer.Spellcaster.chapter.spellsAllowed[2]);
                    break;
                case "Arcanist":
                    Debug.Log("Arcanist scanned " + trackableName);
                    localPlayer.Spellcaster.CollectSpell(localPlayer.Spellcaster.chapter.spellsAllowed[2]);
                    break;
                case "Chronomancer":
                    Debug.Log("Chronomancer scanned " + trackableName);
                    localPlayer.Spellcaster.CollectSpell(localPlayer.Spellcaster.chapter.spellsAllowed[1]);
                    break;
                case "Elementalist":
                    Debug.Log("Elementalist scanned " + trackableName);
                    localPlayer.Spellcaster.CollectSpell(localPlayer.Spellcaster.chapter.spellsAllowed[1]);
                    break;
                case "Summoner":
                    Debug.Log("Summoner scanned " + trackableName);
                    localPlayer.Spellcaster.CollectSpell(localPlayer.Spellcaster.chapter.spellsAllowed[1]);
                    break;
                case "Trickster":
                    Debug.Log("Trickster scanned " + trackableName);
                    localPlayer.Spellcaster.CollectSpell(localPlayer.Spellcaster.chapter.spellsAllowed[1]);
                    break;
            }
        }
        #endregion
    }
}