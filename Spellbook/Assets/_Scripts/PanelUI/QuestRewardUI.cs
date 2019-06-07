using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

// used to display all notification panels
public class QuestRewardUI : MonoBehaviour
{
    [SerializeField] private Text titleText;
    [SerializeField] private Text rewardText1;
    [SerializeField] private Text rewardText2;
    [SerializeField] private Image rewardImage1;
    [SerializeField] private Image rewardImage2;
    [SerializeField] private Button singleButton;
    [SerializeField] private GameObject runeContainer;
    [SerializeField] private GameObject itemContainer;

    Image[] rewardImages = new Image[2];
    Text[] rewardText = new Text[2];

    public bool panelActive = false;
    public string panelID = "quest reward";

    private void DisablePanel()
    {
        gameObject.SetActive(false);
    }
    public void EnablePanel()
    {
        gameObject.SetActive(true);
    }

    public void DisplayQuestRewards(Quest quest)
    {
        titleText.text = quest.questName + " Completed!";

        // set the images and text for quest rewards
        rewardImages[0] = rewardImage1;
        rewardImages[1] = rewardImage2;

        rewardText[0] = rewardText1;
        rewardText[1] = rewardText2;

        Player player = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        if (quest.rewards.Count > 1)
        {
            int i = 0;
            foreach (KeyValuePair<string, string> kvp in quest.rewards)
            {
                switch (kvp.Key)
                {
                    case "Rune":
                        rewardImages[i].sprite = runeContainer.transform.Find(kvp.Value).GetComponent<Image>().sprite;
                        rewardText[i].text = "Draw this from the deck.";
                        ++i;
                        continue;
                    case "Class Rune":
                        rewardImages[i].sprite = runeContainer.transform.Find(player.Spellcaster.classType + " " + kvp.Value).GetComponent<Image>().sprite;
                        rewardText[i].text = "Draw this from the deck.";
                        ++i;
                        continue;
                    case "Mana":
                        rewardImages[i].sprite = itemContainer.transform.Find("ManaCrystal").GetComponent<SpriteRenderer>().sprite;
                        rewardText[i].text = "You earned " + Int32.Parse(kvp.Value) + " mana!";
                        ++i;
                        continue;
                    case "Item":
                        rewardImages[i].sprite = itemContainer.transform.Find(kvp.Value).GetComponent<SpriteRenderer>().sprite;
                        rewardText[i].text = "You earned a " + kvp.Value + "!";
                        ++i;
                        continue;
                    case "Dice":
                        rewardImages[i].sprite = itemContainer.transform.Find("Blank Dice").GetComponent<SpriteRenderer>().sprite;
                        rewardText[i].text = "You earned a temporary " + kvp.Value + "!";
                        ++i;
                        continue;
                    default:
                        ++i;
                        continue;
                }
            }
        }

        singleButton.onClick.AddListener((OkClick));

        gameObject.SetActive(true);

        // if next panel in queue is NOT a notify panel, disable this panel
        if (!PanelHolder.panelQueue.Peek().Equals(panelID))
        {
            DisablePanel();
        }
    }

    private void OkClick()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        gameObject.SetActive(false);

        if(PanelHolder.panelQueue.Count > 0)
            PanelHolder.panelQueue.Dequeue();
        PanelHolder.instance.CheckPanelQueue();
    }
}