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
    List<ItemObject> itemList;
    
    void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        itemList = GameObject.Find("ItemList").GetComponent<ItemList>().listOfItems;

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
        // only scan item if player hasn't scanned a space this turn
        if(!localPlayer.Spellcaster.scannedSpaceThisTurn)
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
        Debug.Log("coroutine running");
        CR_running = true;
        yield return new WaitForSeconds(1f);
        CR_running = false;

        OnTrackingFound();
    }
    
    private void scanItem(string trackableName)
    {
        // check for tsunami crisis
        CrisisHandler.instance.CheckTsunami(localPlayer, trackableName);

        // call function based on target name
        switch (trackableName)
        {
            case "mana":
                int m = (int)UnityEngine.Random.Range(100, 700);
                localPlayer.Spellcaster.CollectMana(m);
                break;

            case "item":
                // choose a random item to give to player from list
                ItemObject item = itemList[UnityEngine.Random.Range(0, itemList.Count - 1)];
                PanelHolder.instance.displayBoardScan("You found an Item!", "You got found a " + item.name + "!", item.sprite);
                SoundManager.instance.PlaySingle(SoundManager.itemfound);
                localPlayer.Spellcaster.AddToInventory(item);
                break;

            #region town_spaces
            case "town_alchemist":
                Quest alchemyManaQuest = new AlchemyManaQuest(localPlayer.Spellcaster.NumOfTurnsSoFar);
                if (QuestTracker.instance.HasQuest(alchemyManaQuest))
                {
                    PanelHolder.instance.displayNotify("Alchemist Town", "You're already on a quest for this town.", "OK");
                }
                else
                {
                    PanelHolder.instance.displayQuest(alchemyManaQuest);
                }
                break;

            case "town_arcanist":
                break;

            case "town_chronomancer":
                Quest timeMoveQuest = new TimeMoveQuest(localPlayer.Spellcaster.NumOfTurnsSoFar);
                if (QuestTracker.instance.HasQuest(timeMoveQuest))
                {
                    PanelHolder.instance.displayNotify("Chronomancer Town", "You're already on a quest for this town.", "OK");
                }
                else
                {
                    PanelHolder.instance.displayQuest(timeMoveQuest);
                }
                break;

            case "town_elementalist":
                Quest elementalMoveQuest = new ElementalMoveQuest(localPlayer.Spellcaster.NumOfTurnsSoFar);
                if (QuestTracker.instance.HasQuest(elementalMoveQuest))
                {
                    PanelHolder.instance.displayNotify("Elemental Town", "You're already on a quest for this town.", "OK");
                }
                else
                {
                    PanelHolder.instance.displayQuest(elementalMoveQuest);
                }
                break;

            case "town_illusionist":
                Quest illusionSpaceQuest = new IllusionManaQuest(localPlayer.Spellcaster.NumOfTurnsSoFar);
                if (QuestTracker.instance.HasQuest(illusionSpaceQuest))
                {
                    PanelHolder.instance.displayNotify("Illusion Town", "You're already on a quest for this town.", "OK");
                }
                else
                {
                    PanelHolder.instance.displayQuest(illusionSpaceQuest);
                }
                break;

            case "town_summoner":
                Quest summonManaQuest = new SummoningManaQuest(localPlayer.Spellcaster.NumOfTurnsSoFar);
                if (QuestTracker.instance.HasQuest(summonManaQuest))
                {
                    PanelHolder.instance.displayNotify("Summoner Town", "You're already on a quest for this town.", "OK");
                }
                else
                {
                    PanelHolder.instance.displayQuest(summonManaQuest);
                }
                break;
            #endregion

            #region locations
            case "location_mines":
                SceneManager.LoadScene("MineScene");
                break;
            case "location_swamp":
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