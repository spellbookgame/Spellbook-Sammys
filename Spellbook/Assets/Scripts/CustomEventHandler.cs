using System.Collections;
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
        StartCoroutine(ScanTime(mTrackableBehaviour.TrackableName));
    }

    protected virtual void OnTrackingLost()
    {
        if(panelOpen)
        {
            panel.SetActive(false);
        }
    }

    IEnumerator ScanTime(string target_name)
    {
        yield return new WaitForSeconds(1.5f);

        switch (target_name)
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
}
