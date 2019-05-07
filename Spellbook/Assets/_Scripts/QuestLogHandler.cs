using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestLogHandler : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Button questButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject questInfoPanel;
    [SerializeField] private Text questName;
    [SerializeField] private Text questTask;
    [SerializeField] private Text turnsLeft;

    private Player localPlayer;

    // Start is called before the first frame update
    void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        exitButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            UICanvasHandler.instance.ActivateSpellbookButtons(false);
            SceneManager.LoadScene("MainPlayerScene");
        });

        closeButton.onClick.AddListener((ClosePanel));

        int yPos = 105;
        foreach (Quest q in localPlayer.Spellcaster.activeQuests)
        {
            Button newButton = Instantiate(questButton, GameObject.Find("Canvas").transform);
            newButton.GetComponentInChildren<Text>().text = q.questName;

            newButton.transform.localPosition = new Vector3(0, yPos, 0);
            newButton.onClick.AddListener(() => DisplayQuest(q));

            yPos -= 235;
        }

        PanelHolder.instance.SetPanelHolderLast();
    }

    private void DisplayQuest(Quest quest)
    {
        SoundManager.instance.PlaySingle(SoundManager.spellbookopen);

        questInfoPanel.transform.SetAsLastSibling();

        questName.text = quest.questName;
        questTask.text = quest.questTask;
        turnsLeft.text = "Turns left: " + (quest.turnLimit - (localPlayer.Spellcaster.NumOfTurnsSoFar - quest.startTurn)).ToString();

        questInfoPanel.SetActive(true);
    }

    private void ClosePanel()
    {
        SoundManager.instance.PlaySingle(SoundManager.spellbookClose);
        questInfoPanel.SetActive(false);
    }
}
