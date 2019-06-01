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
    private bool questShown;

    private Player localPlayer;
    private void Start()
    {
        SoundManager.instance.PlayGameBCM(SoundManager.andromedaBGM);
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        quests = new Quest[]
        {
            new SummoningErrandQuest(localPlayer.Spellcaster.NumOfTurnsSoFar),
            new SummoningManaQuest(localPlayer.Spellcaster.NumOfTurnsSoFar),
            new SummoningSpellQuest(localPlayer.Spellcaster.NumOfTurnsSoFar)
        };

        findQuestButton.onClick.AddListener(FindQuest);

        leaveButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SoundManager.instance.PlayGameBCM(SoundManager.gameBCG);
            SceneManager.LoadScene("MainPlayerScene");
        });

        QuestTracker.instance.TrackLocationQuest("town_summoner");
        QuestTracker.instance.TrackErrandQuest("town_summoner");
    }

    private void FindQuest()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);

        bool hasQuestInThiSTown = false;

        // check if player has quest from this town
        foreach (Quest q in quests)
        {
            if (QuestTracker.instance.HasQuest(q))
            {
                hasQuestInThiSTown = true;
                break;
            }
        }
        if (hasQuestInThiSTown)
        {
            PanelHolder.instance.displayNotify("Andromeda", "You're already on a quest for this town.", "OK");
        }
        else
        {
            if (!questShown)
            {
                int r = Random.Range(0, quests.Length);
                PanelHolder.instance.displayQuest(quests[r]);
                questShown = true;
            }
            else
            {
                PanelHolder.instance.displayNotify("Too Late", "You denied a quest, you cannot find another one until you come back.", "OK");
            }
        }
    }
}