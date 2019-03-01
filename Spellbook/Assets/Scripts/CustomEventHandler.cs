using System;
using System.Collections;
using UnityEngine;
using Vuforia;

public class CustomEventHandler : MonoBehaviour, ITrackableEventHandler
{
    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;

    private Coroutine coroutineReference;
    private bool CR_running;
    private bool spaceScanned = false;

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
        scanItem(mTrackableBehaviour.TrackableName);
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
        CR_running = true;
        yield return new WaitForSeconds(1.5f);
        CR_running = false;

        // only track once; after a space is scanned, do not scan anymore
        if(!spaceScanned)
        {
            OnTrackingFound();
        }
    }

    private void scanItem(string trackableName)
    {
        // call function based on target name
        switch (trackableName)
        {
            case "mana":
                int manaCount = (int)UnityEngine.Random.Range(100, 1000);
                localPlayer.Spellcaster.CollectMana(manaCount);
                break;
            case "glyph":
                localPlayer.Spellcaster.CollectRandomGlyph();
                break;
            case "city":
                PanelHolder.instance.displayEvent("You landed in the city", "Nothing really to see here...");
                break;
            case "alchemy_mana":
                PanelHolder.instance.displayEvent("Alchemy Mana", "Alchemy mana scanned");
                break;
            case "arcane_mana":
                PanelHolder.instance.displayEvent("Arcane Mana", "Arcane mana scanned");
                break;
            case "alchemy_city":
                PanelHolder.instance.displayEvent("Alchemy City", "Alchemy city scanned");
                break;
            case "arcane_city":
                PanelHolder.instance.displayEvent("Arcane City", "Arcane city scanned");
                break;
            default:
                break;
        }
        spaceScanned = true;
    }
}
