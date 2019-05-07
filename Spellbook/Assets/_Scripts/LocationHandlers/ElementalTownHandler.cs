using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ElementalTownHandler : MonoBehaviour
{
    [SerializeField] private Button findQuestButton;
    [SerializeField] private Button dropItemButton;
    [SerializeField] private Button pickupItemButton;
    [SerializeField] private Button leaveButton;

    private Quest[] quests;

    private Player localPlayer;
    private void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        quests = new Quest[]
        {
            new ElementalErrandQuest(localPlayer.Spellcaster.NumOfTurnsSoFar),
            new ElementalMoveQuest(localPlayer.Spellcaster.NumOfTurnsSoFar)
        };

        findQuestButton.onClick.AddListener(FindQuest);

        leaveButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("MainPlayerScene");
        });

        QuestTracker.instance.TrackLocationQuest("town_elementalist");
        QuestTracker.instance.TrackErrandQuest("town_elementalist");
    }

    private void FindQuest()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);

        // if player doesn't have a quest from this town yet, give random quest
        if (QuestTracker.instance.HasQuest(quests[0]) || QuestTracker.instance.HasQuest(quests[1]))
        {
            PanelHolder.instance.displayNotify("Elementalist Town", "You're already on a quest for this town.", "OK");
        }
        else
        {
            int r = Random.Range(0, 2);
            PanelHolder.instance.displayQuest(quests[r]);
        }
    }
}
