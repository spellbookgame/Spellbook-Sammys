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
        // in board_space_handling region
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
            #region mana_spaces
            case "alchemist_mana":
                int r1 = (int)UnityEngine.Random.Range(100, 700);
                localPlayer.Spellcaster.CollectMana(r1);
                break;
            case "arcanist_mana":
                int r2 = (int)UnityEngine.Random.Range(100, 700);
                localPlayer.Spellcaster.CollectMana(r2);
                break;
            case "chronomancer_mana":
                int r3 = (int)UnityEngine.Random.Range(100, 700);
                localPlayer.Spellcaster.CollectMana(r3);
                break;
            case "elementalist_mana":
                int r4 = (int)UnityEngine.Random.Range(100, 700);
                localPlayer.Spellcaster.CollectMana(r4);
                break;
            case "illusionist_mana":
                int r5 = (int)UnityEngine.Random.Range(100, 700);
                localPlayer.Spellcaster.CollectMana(r5);
                break;
            case "summoner_mana":
                int r6 = (int)UnityEngine.Random.Range(100, 700);
                localPlayer.Spellcaster.CollectMana(r6);
                break;
            case "generic_mana":
                int m = (int)UnityEngine.Random.Range(100, 700);
                localPlayer.Spellcaster.CollectMana(m);
                break;
            #endregion

            #region city_spaces
            case "alchemist_city":
                PanelHolder.instance.displayEvent("Alchemist city", "Nothing really to see here...");
                break;
            case "arcanist_city":
                PanelHolder.instance.displayEvent("Arcanist city", "Nothing really to see here...");
                break;
            case "chronomancer_city":
                PanelHolder.instance.displayEvent("Chronomancer city", "Nothing really to see here...");
                break;
            case "elementalist_city":
                PanelHolder.instance.displayEvent("Elementalist city", "Nothing really to see here...");
                break;
            case "illusionist_city":
                PanelHolder.instance.displayEvent("Illusionist city", "Nothing really to see here...");
                break;
            case "summoner_city":
                PanelHolder.instance.displayEvent("Summoner city", "Nothing really to see here...");
                break;
            case "generic_city":
                PanelHolder.instance.displayEvent("Generic city", "Nothing really to see here...");
                break;
            #endregion

            #region glyph_spaces
            case "alchemist_glyph":
                int g1 = (int)UnityEngine.Random.Range(0, 2);
                if (g1 == 0)
                    localPlayer.Spellcaster.CollectGlyph("Alchemy D Glyph");
                else
                    localPlayer.Spellcaster.CollectGlyph("Alchemy C Glyph");
                break;
            case "arcanist_glyph":
                int g2 = (int)UnityEngine.Random.Range(0, 2);
                if (g2 == 0)
                    localPlayer.Spellcaster.CollectGlyph("Arcane D Glyph");
                else
                    localPlayer.Spellcaster.CollectGlyph("Arcane C Glyph");
                break;
            case "chronomancer_glyph":
                int g3 = (int)UnityEngine.Random.Range(0, 2);
                if (g3 == 0)
                    localPlayer.Spellcaster.CollectGlyph("Time D Glyph");
                else
                    localPlayer.Spellcaster.CollectGlyph("Time C Glyph");
                break;
            case "elementalist_glyph":
                int g4 = (int)UnityEngine.Random.Range(0, 2);
                if (g4 == 0)
                    localPlayer.Spellcaster.CollectGlyph("Elemental D Glyph");
                else
                    localPlayer.Spellcaster.CollectGlyph("Elemental C Glyph");
                break;
            case "illusionist_glyph":
                int g5 = (int)UnityEngine.Random.Range(0, 2);
                if (g5 == 0)
                    localPlayer.Spellcaster.CollectGlyph("Illusion D Glyph");
                else
                    localPlayer.Spellcaster.CollectGlyph("Illusion C Glyph");
                break;
            case "summoner_glyph":
                int g6 = (int)UnityEngine.Random.Range(0, 2);
                if (g6 == 0)
                    localPlayer.Spellcaster.CollectGlyph("Summoning D Glyph");
                else
                    localPlayer.Spellcaster.CollectGlyph("Summoning C Glyph");
                break;
            case "generic_glyph":
                localPlayer.Spellcaster.CollectRandomGlyph();
                break;
            #endregion

            #region item_spaces
            case "alchemist_item":
                PanelHolder.instance.displayEvent("Alchemist item", "You got a [item name] item!");
                break;
            case "arcanist_item":
                PanelHolder.instance.displayEvent("Arcanist item", "You got a [item name] item!");
                break;
            case "chronomancer_item":
                PanelHolder.instance.displayEvent("Chronomancer item", "You got a [item name] item!");
                break;
            case "elementalist_item":
                PanelHolder.instance.displayEvent("Elementalist item", "You got a [item name] item!");
                break;
            case "illusionist_item":
                PanelHolder.instance.displayEvent("Illusionist item", "You got a [item name] item!");
                break;
            case "summoner_item":
                PanelHolder.instance.displayEvent("Summoner item", "You got a [item name] item!");
                break;
            #endregion

            default:
                break;
        }
        spaceScanned = true;
    }
}