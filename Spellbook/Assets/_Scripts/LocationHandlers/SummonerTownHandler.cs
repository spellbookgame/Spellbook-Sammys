using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SummonerTownHandler : MonoBehaviour
{
    [SerializeField] private Button findQuestButton;
    [SerializeField] private Button dropItemButton;
    [SerializeField] private Button pickupItemButton;
    [SerializeField] private Button leaveButton;
    [SerializeField] private Text dialogueText;

    private Quest[] quests;

    private Player localPlayer;
    private void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        quests = new Quest[]
        {
            new SummoningErrandQuest(localPlayer.Spellcaster.NumOfTurnsSoFar),
            new SummoningManaQuest(localPlayer.Spellcaster.NumOfTurnsSoFar)
        };

        findQuestButton.onClick.AddListener(FindQuest);

        leaveButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("MainPlayerScene");
        });

        QuestTracker.instance.TrackLocationQuest("town_summoner");
        QuestTracker.instance.TrackErrandQuest("town_summoner");
    }

    private void FindQuest()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);

        // if player doesn't have a quest from this town yet, give random quest
        if (QuestTracker.instance.HasQuest(quests[0]) || QuestTracker.instance.HasQuest(quests[1]))
        {
            PanelHolder.instance.displayNotify("Summoner Town", "You're already on a quest for this town.", "OK");
        }
        else
        {
            int r = Random.Range(0, 2);
            PanelHolder.instance.displayQuest(quests[r]);
        }
    }
}