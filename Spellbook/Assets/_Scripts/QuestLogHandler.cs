using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestLogHandler : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Button backButton;

    [SerializeField] private Text activeQuestText;
    [SerializeField] private Text turnsText;
    [SerializeField] private Text rewardsText;

    private Player localPlayer;

    // Start is called before the first frame update
    void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        exitButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("MainPlayerScene");
        });
        backButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("SpellbookScene");
        });

        foreach(Quest q in localPlayer.Spellcaster.activeQuests)
        {
            activeQuestText.text = activeQuestText.text + q.questName + "\n\n\n";
            turnsText.text = turnsText.text + (q.turnLimit - (localPlayer.Spellcaster.NumOfTurnsSoFar - q.startTurn)).ToString() + "\n\n\n";
            rewardsText.text = rewardsText.text + q.DisplayReward();
        }
    }
}
