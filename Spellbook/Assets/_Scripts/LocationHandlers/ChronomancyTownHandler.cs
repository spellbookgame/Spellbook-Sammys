using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChronomancyTownHandler : MonoBehaviour
{
    [SerializeField] private Button findQuestButton;
    [SerializeField] private Button dropItemButton;
    [SerializeField] private Button pickupItemButton;
    [SerializeField] private Button leaveButton;

    private Player localPlayer;
    private void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        findQuestButton.onClick.AddListener(FindQuest);

        leaveButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("MainPlayerScene");
        });

        QuestTracker.instance.TrackLocationQuest("town_chronomancer");
        QuestTracker.instance.TrackErrandQuest("town_chronomancer");
    }

    private void FindQuest()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);

        Quest timeMoveQuest = new TimeMoveQuest(localPlayer.Spellcaster.NumOfTurnsSoFar);
        if (QuestTracker.instance.HasQuest(timeMoveQuest))
        {
            PanelHolder.instance.displayNotify("Chronomancer Town", "You're already on a quest for this town.", "OK");
        }
        else
        {
            PanelHolder.instance.displayQuest(timeMoveQuest);
        }
    }
}
