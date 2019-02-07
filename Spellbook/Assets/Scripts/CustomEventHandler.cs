using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Vuforia;

public class CustomEventHandler : MonoBehaviour, ITrackableEventHandler
{
    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;

    [SerializeField] private GameObject panel;
    private bool panelOpen = false;
    
    void Start()
    {
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
        if (mTrackableBehaviour.TrackableName.Equals("decal_chronomancer"))
        {
            panel.transform.GetChild(0).GetComponent<Text>().text = "Chronomancer scanned";
            panel.SetActive(true);
            panelOpen = true;
        }
        else if (mTrackableBehaviour.TrackableName.Equals("decal_summoner"))
        {
            panel.transform.GetChild(0).GetComponent<Text>().text = "Summoner scanned";
            panel.SetActive(true);
            panelOpen = true;
        }
        else if (mTrackableBehaviour.TrackableName.Equals("decal_alchemist"))
        {
            panel.transform.GetChild(0).GetComponent<Text>().text = "Alchemist scanned";
            panel.SetActive(true);
            panelOpen = true;
        }
        else if (mTrackableBehaviour.TrackableName.Equals("decal_arcanist"))
        {
            panel.transform.GetChild(0).GetComponent<Text>().text = "Arcanist scanned";
            panel.SetActive(true);
            panelOpen = true;
        }
        else if (mTrackableBehaviour.TrackableName.Equals("decal_illusionist"))
        {
            panel.transform.GetChild(0).GetComponent<Text>().text = "Trickster scanned";
            panel.SetActive(true);
            panelOpen = true;
        }
        else if (mTrackableBehaviour.TrackableName.Equals("decal_elementalist"))
        {
            panel.transform.GetChild(0).GetComponent<Text>().text = "Elementalist scanned";
            panel.SetActive(true);
            panelOpen = true;
        }
    }

    protected virtual void OnTrackingLost()
    {
        if(panelOpen)
        {
            panel.SetActive(false);
        }
    }
}
