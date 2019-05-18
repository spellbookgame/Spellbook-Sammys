using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestLogHandler : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Button questButton;
    [SerializeField] private Button previousQuestButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject questInfoPanel;
    [SerializeField] private Text questName;
    [SerializeField] private Text questTask;
    [SerializeField] private Image rewardImage0;
    [SerializeField] private Image rewardImage1;

    private Player localPlayer;
    private Image[] rewardImages;

    // Start is called before the first frame update
    void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        rewardImages = new Image[2];

        exitButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            UICanvasHandler.instance.ActivateSpellbookButtons(false);
            SceneManager.LoadScene("MainPlayerScene");
        });

        closeButton.onClick.AddListener((ClosePanel));

        int yPos = 295;
        foreach (Quest q in localPlayer.Spellcaster.activeQuests)
        {
            Button newButton = Instantiate(questButton, GameObject.Find("Canvas").transform);
            newButton.GetComponentInChildren<Text>().text = q.questName;

            newButton.transform.localPosition = new Vector3(0, yPos, 0);
            newButton.onClick.AddListener(() => DisplayQuest(q));

            yPos -= 235;
        }

        if(QuestTracker.instance.previousQuest != null)
        {
            previousQuestButton.onClick.AddListener(() => DisplayQuest(QuestTracker.instance.previousQuest));
            previousQuestButton.transform.GetChild(0).GetComponent<Text>().text = QuestTracker.instance.previousQuest.questName;
        }

        PanelHolder.instance.SetPanelHolderLast();
    }

    private void DisplayQuest(Quest quest)
    {
        SoundManager.instance.PlaySingle(SoundManager.spellbookopen);

        questInfoPanel.transform.SetAsLastSibling();

        questName.text = quest.questName;
        questTask.text = "Task: " + quest.questTask + "\n\n Hint: " + quest.questHint;

        // set reward images
        rewardImages[0] = rewardImage0;
        rewardImages[1] = rewardImage1;

        int i = 0;
        foreach (KeyValuePair<string, string> kvp in quest.rewards)
        {
            switch (kvp.Key)
            {
                case "Rune":
                    rewardImages[i].sprite = Resources.Load<Sprite>("RuneArt/" + kvp.Value);
                    ++i;
                    continue;
                case "Class Rune":
                    rewardImages[i].sprite = Resources.Load<Sprite>("RuneArt/" +
                        GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>().Spellcaster.classType + " " + kvp.Value);
                    ++i;
                    continue;
                case "Mana":
                    rewardImages[i].sprite = Resources.Load<Sprite>("Art Assets/Items and Currency/ManaCrystal");
                    ++i;
                    continue;
                case "Item":
                    rewardImages[i].sprite = Resources.Load<Sprite>("Art Assets/Items and Currency/" + kvp.Value);
                    ++i;
                    continue;
                case "Dice":
                    rewardImages[i].sprite = Resources.Load<Sprite>("Art Assets/Items and Currency/Blank Dice");
                    ++i;
                    continue;
                default:
                    ++i;
                    continue;
            }
        }

        questInfoPanel.SetActive(true);
    }

    private void ClosePanel()
    {
        SoundManager.instance.PlaySingle(SoundManager.spellbookClose);
        questInfoPanel.SetActive(false);
    }
}
