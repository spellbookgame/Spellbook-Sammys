using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class MultiTargetEventHandler : MonoBehaviour, ITrackableEventHandler
{
    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;

    private bool isTracked = false;
    private int numTracked = 0;

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
            OnTrackingFound();
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
        isTracked = true;
        CheckTargetsTracked();
    }

    protected virtual void OnTrackingLost()
    {
        // when trackable item is lost
    }

    private void ScanItem()
    {
        // scan
        switch (localPlayer.Spellcaster.classType)
        {
            // collect spell based on class
            case "Alchemist":
                localPlayer.Spellcaster.CollectSpell(localPlayer.Spellcaster.chapter.spellsAllowed[3]);
                break;
            case "Arcanist":
                localPlayer.Spellcaster.CollectSpell(localPlayer.Spellcaster.chapter.spellsAllowed[3]);
                break;
            case "Chronomancer":
                localPlayer.Spellcaster.CollectSpell(localPlayer.Spellcaster.chapter.spellsAllowed[2]);
                break;
            case "Elementalist":
                localPlayer.Spellcaster.CollectSpell(localPlayer.Spellcaster.chapter.spellsAllowed[2]);
                break;
            case "Summoner":
                localPlayer.Spellcaster.CollectSpell(localPlayer.Spellcaster.chapter.spellsAllowed[2]);
                break;
            case "Trickster":
                localPlayer.Spellcaster.CollectSpell(localPlayer.Spellcaster.chapter.spellsAllowed[2]);
                break;
        }
    }

    // if 4 targets are detected, scan item
    private void CheckTargetsTracked()
    {
        int targetsTracked = 0;
        foreach(MultiTargetEventHandler m in FindObjectsOfType<MultiTargetEventHandler>())
        {
            if (!m.isTracked)
            {
                // do nothing
            }
            else
                ++targetsTracked;
        }
        if (targetsTracked >= 4)
        {
            Debug.Log("4 items tracked!");
            ScanItem();
        }
    }
}