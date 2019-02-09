using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class CustomEventHandler : MonoBehaviour, ITrackableEventHandler
{
    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;

    [SerializeField] private GameObject panel;
    private bool panelOpen = false;

    private Coroutine coroutineReference;
    private bool CR_running;

    private Dictionary<string, float> storedTimes;

    Player localPlayer;
    
    void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        storedTimes = new Dictionary<string, float>()
        {
            {"decal_alchemist", 0.0f },
            {"decal_arcanist", 0.0f },
            {"decal_chronomancer", 0.0f },
            {"decal_elementalist", 0.0f },
            {"decal_illusionist", 0.0f },
            {"decal_summoner", 0.0f },
        };

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
            // basically, wait 3 seconds before itll start scanning the target
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
        // add corresponding glyph to player's collection
        switch (mTrackableBehaviour.TrackableName)
        {
            case "decal_alchemist":
                panel.transform.GetChild(0).GetComponent<Text>().text = "Found 1 Alchemy glyph";
                panel.SetActive(true);
                panelOpen = true;
                localPlayer.Spellcaster.glyphs["Alchemy1"] += 1;
                break;
            case "decal_arcanist":
                panel.transform.GetChild(0).GetComponent<Text>().text = "Found 1 Arcane glyph";
                panel.SetActive(true);
                panelOpen = true;
                localPlayer.Spellcaster.glyphs["Arcane1"] += 1;
                break;
            case "decal_chronomancer":
                panel.transform.GetChild(0).GetComponent<Text>().text = "Found 1 Time glyph";
                panel.SetActive(true);
                panelOpen = true;
                localPlayer.Spellcaster.glyphs["Time1"] += 1;
                break;
            case "decal_elementalist":
                panel.transform.GetChild(0).GetComponent<Text>().text = "Found 1 Elemental glyph";
                panel.SetActive(true);
                panelOpen = true;
                localPlayer.Spellcaster.glyphs["Elemental1"] += 1;
                break;
            case "decal_illusionist":
                panel.transform.GetChild(0).GetComponent<Text>().text = "Found 1 Illusion glyph";
                panel.SetActive(true);
                panelOpen = true;
                localPlayer.Spellcaster.glyphs["Illusion1"] += 1;
                break;
            case "decal_summoner":
                panel.transform.GetChild(0).GetComponent<Text>().text = "Found 1 Summoning glyph";
                panel.SetActive(true);
                panelOpen = true;
                localPlayer.Spellcaster.glyphs["Summoning1"] += 1;
                break;
        }
    }

    protected virtual void OnTrackingLost()
    {
        if(CR_running)
        {
            StopCoroutine(coroutineReference);
        }
        if(panelOpen)
        {
            panel.SetActive(false);
        }
    }

    IEnumerator ScanTime()
    {
        CR_running = true;
        yield return new WaitForSeconds(1.5f);
        CR_running = false;
        OnTrackingFound();
    }
}
