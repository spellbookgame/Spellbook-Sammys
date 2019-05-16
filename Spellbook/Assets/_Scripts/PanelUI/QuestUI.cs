using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// used for all quest purposes
public class QuestUI : MonoBehaviour
{
    [SerializeField] private Text titleText;
    [SerializeField] private Text infoText;
    [SerializeField] private Image rewardImage1;
    [SerializeField] private Image rewardImage2;
    [SerializeField] private Button singleButton;
    [SerializeField] private Button singleButton1;

    Image[] rewardImages = new Image[2];

    public bool panelActive = false;
    public string panelID = "quest";

    private void DisablePanel()
    {
        gameObject.SetActive(false);
    }
    public void EnablePanel()
    {
        gameObject.SetActive(true);
    }

    public void DisplayQuest(Quest quest)
    {
        titleText.text = quest.questName;
        infoText.text = quest.questFlavor;

        // set the images for quest rewards
        rewardImages[0] = rewardImage1;
        rewardImages[1] = rewardImage2;

        int i = 0;
        foreach(KeyValuePair<string, string> kvp in quest.rewards)
        {
            switch(kvp.Key)
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

        singleButton.onClick.AddListener(() => buttonClicked("accept", quest));
        singleButton1.onClick.AddListener(() => buttonClicked("deny", quest));

        gameObject.SetActive(true);

        if (!PanelHolder.panelQueue.Peek().Equals(panelID))
        {
            DisablePanel();
        }
    }

    private void buttonClicked(string input, Quest q)
    {
        GameObject player = GameObject.FindGameObjectWithTag("LocalPlayer");

        // add quest to player's list of active quests if they accept
        if (input.Equals("accept"))
        {
            SoundManager.instance.PlaySingle(SoundManager.questaccept);
            player.GetComponent<Player>().Spellcaster.activeQuests.Add(q);
            SceneManager.LoadScene("MainPlayerScene");
        }
        else
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);

        gameObject.SetActive(false);

        // for start of game: show tutorial prompt if player hasn't seen tutorial yet
        if (!player.GetComponent<Player>().Spellcaster.tutorialShown)
            UICanvasHandler.instance.ShowTutorialPrompt();

        if (PanelHolder.panelQueue.Count > 0)
            PanelHolder.panelQueue.Dequeue();
        PanelHolder.instance.CheckPanelQueue();
    }
}
