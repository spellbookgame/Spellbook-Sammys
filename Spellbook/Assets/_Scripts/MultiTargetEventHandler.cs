using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

// for scanning multiple (4) images
public class MultiTargetEventHandler : MonoBehaviour, ITrackableEventHandler
{
    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;

    private Text targetName;
    private Text targetName1;
    private Text targetNumber;

    private bool isTracked = false;
    private Dictionary<string, int> targets;
    private int targetsTracked;

    Player localPlayer;

    void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        targets = new Dictionary<string, int>();

        targetName = GameObject.Find("text_targetNameValue").GetComponent<Text>();
        targetName1 = GameObject.Find("text_targetNameValue (1)").GetComponent<Text>();
        targetNumber = GameObject.Find("text_targetNumberValue").GetComponent<Text>();

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
            Debug.Log(mTrackableBehaviour.TrackableName + " found");
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
        // when track is lost, reset bool and remove from dictionary
        isTracked = false;
        targets.Clear();
        targetName.text = "";
        targetName1.text = "";
        targetNumber.text = "";
    }

    // if 4 targets are detected, scan item
    private void CheckTargetsTracked()
    {
        // reset values at the beginning, so it only reads accurately when there are 4 targets tracked
        targetsTracked = 0;
        targets.Clear();

        foreach (MultiTargetEventHandler m in FindObjectsOfType<MultiTargetEventHandler>())
        {
            if (!m.isTracked)
            {
                // do nothing
            }
            else
            {
                if (targets.ContainsKey(m.name))
                {
                    targets[m.name] += 1;
                }
                else
                    targets.Add(m.name, 1);

                ++targetsTracked;
                // set text on canvas to debug
                targetName1.text = m.name;
                targetNumber.text = targetsTracked.ToString();
            }
        }
        // if 4 targets were tracked, start scanning
        if (targetsTracked >= 4)
        {
            Debug.Log("4 items tracked! Dictionary:");
            foreach(KeyValuePair<string, int> kvp in targets)
            {
                targetName.text = targetName.text + "\n" + kvp.Key;
            }
            CompareSpells();
        }
    }

    // compares targets dictionary to spell's required glyphs dictionary
    private void CompareSpells()
    {
        bool isEqual = false;

        Dictionary<string, int> d1 = targets;
        for(int i = 0; i <= localPlayer.Spellcaster.chapter.spellsAllowed.Count; i++)
        {
            Dictionary<string, int> d2 = localPlayer.Spellcaster.chapter.spellsAllowed[i].requiredRunes;

            // tier 3 spell: only needs to check if d1 contains the required glyph
            if(localPlayer.Spellcaster.chapter.spellsAllowed[i].iTier == 3)
            {
                if(d2.Keys.All(k => d1.ContainsKey(k)))
                {
                    localPlayer.Spellcaster.CollectSpell(localPlayer.Spellcaster.chapter.spellsAllowed[i]);
                    break;
                }
                else
                {
                    PanelHolder.instance.displayNotify("No Spell Detected", "You did not create a spell.", "OK");
                    break;
                }
            }
            // tier 2 and 1 spells
            else
            {
                foreach(KeyValuePair<string, int> kvp in d2)
                {
                    if (d1.ContainsKey(kvp.Key))
                    {
                        isEqual = true;
                    }
                    else
                    {
                        isEqual = false;
                    }
                }
                if(isEqual)
                {
                    localPlayer.Spellcaster.CollectSpell(localPlayer.Spellcaster.chapter.spellsAllowed[i]);
                    break;
                }
                else
                {
                    PanelHolder.instance.displayNotify("No Spell Detected", "You did not create a spell.", "OK");
                    break;
                }
            }
        }
    }
}