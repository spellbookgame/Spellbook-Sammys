using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArcaneTownHandler : MonoBehaviour
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
    }

    private void FindQuest()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);

        Quest arcaneErrandQuest = new ArcaneErrandQuest(localPlayer.Spellcaster.NumOfTurnsSoFar);
        if (QuestTracker.instance.HasQuest(arcaneErrandQuest))
        {
            PanelHolder.instance.displayNotify("Arcane Town", "You're already on a quest for this town.", "OK");
        }
        else
        {
            PanelHolder.instance.displayQuest(arcaneErrandQuest);
        }
    }
}
