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
        // only scan item if player hasn't scanned a space this turn, if they've moved, OR if they used a location item that teleported them
        if ((!localPlayer.Spellcaster.scannedSpaceThisTurn && UICanvasHandler.instance.spacesMoved > 0) || localPlayer.Spellcaster.locationItemUsed)
            scanItem(mTrackableBehaviour.TrackableName);
        else if(localPlayer.Spellcaster.scannedSpaceThisTurn)
        {
            PanelHolder.instance.displayNotify("","You already scanned a location this turn.", "MainPlayerScene");
        }
        else if(UICanvasHandler.instance.spacesMoved <= 0)
        {
            PanelHolder.instance.displayNotify("", "You can't scan a location if you haven't moved.", "MainPlayerScene");
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

        // reset location item used bool
        localPlayer.Spellcaster.locationItemUsed = false;

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
            case "location_shrine":
                ItemList itemList = GameObject.Find("ItemList").GetComponent<ItemList>();
                List<ItemObject> il1 = itemList.tier1Items;
                List<ItemObject> il2 = itemList.tier2Items;
                List<ItemObject> il3 = itemList.tier3Items;

                // give a random item based on percentage
                int r = UnityEngine.Random.Range(0, 101);
                ItemObject randItem;
                if (r < 10)
                    randItem = il1[UnityEngine.Random.Range(0, il1.Count)];
                else if (r >= 10 && r < 40)
                    randItem = il2[UnityEngine.Random.Range(0, il2.Count)];
                else
                    randItem = il3[UnityEngine.Random.Range(0, il3.Count)];

                localPlayer.Spellcaster.AddToInventory(randItem);
                SceneManager.LoadScene("MainPlayerScene");
                PanelHolder.instance.displayBoardScan("Shrine", "The shrine has given you a " + randItem.name + "!", randItem.sprite, "OK");
                break;
            case "location_springs":
                int healAmount = UnityEngine.Random.Range(2, 11);
                localPlayer.Spellcaster.HealDamage(healAmount);
                int manaAmount = UnityEngine.Random.Range(100, 1000);
                localPlayer.Spellcaster.CollectMana(manaAmount);
                SceneManager.LoadScene("MainPlayerScene");
                PanelHolder.instance.displayNotify("Springs", "You rested in the springs and recovered " + healAmount.ToString() + 
                                                    " health! You also found " + manaAmount.ToString() + " mana.", "OK");
                break;
            #endregion

            default:
                break;
        }
        localPlayer.Spellcaster.scannedSpaceThisTurn = true;
    }
}