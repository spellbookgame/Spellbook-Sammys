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
    }

    private void FindQuest()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);

        Quest summonManaQuest = new SummoningManaQuest(localPlayer.Spellcaster.NumOfTurnsSoFar);
        if (QuestTracker.instance.HasQuest(summonManaQuest))
        {
            PanelHolder.instance.displayNotify("Summoner Town", "You're already on a quest for this town.", "OK");
        }
        else
        {
            PanelHolder.instance.displayQuest(summonManaQuest);
        }
    }
}