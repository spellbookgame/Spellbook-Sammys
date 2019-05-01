using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlchemyTownHandler : MonoBehaviour
{
    [SerializeField] private Button findQuestButton;
    [SerializeField] private Button dropItemButton;
    [SerializeField] private Button pickupItemButton;

    private Player localPlayer;
    private void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        findQuestButton.onClick.AddListener(FindQuest);
    }

    private void FindQuest()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);

        Quest alchemyManaQuest = new AlchemyManaQuest(localPlayer.Spellcaster.NumOfTurnsSoFar);
        if (QuestTracker.instance.HasQuest(alchemyManaQuest))
        {
            PanelHolder.instance.displayNotify("Alchemist Town", "You're already on a quest for this town.", "OK");
        }
        else
        {
            PanelHolder.instance.displayQuest(alchemyManaQuest);
        }
    }
}
