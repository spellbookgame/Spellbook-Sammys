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
    [SerializeField] private GameObject ribbon;

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

        // if current scene is vuforia, remove ribbon from panel
        if (SceneManager.GetActiveScene().name.Equals("VuforiaScene"))
        {
            ribbon.SetActive(false);
        }

        // set the images and text for quest rewards
        rewardImages[0] = rewardImage1;
        rewardImages[1] = rewardImage2;

        rewardText[0] = rewardText1;
        rewardText[1] = rewardText2;

        if (quest.rewards.Count > 1)
        {
            int i = 0;
            foreach (KeyValuePair<string, string> kvp in quest.rewards)
            {
                switch (kvp.Key)
                {
                    case "Rune":
                        rewardImages[i].sprite = Resources.Load<Sprite>("RuneArt/" + kvp.Value);
                        rewardText[i].text = "Draw this from the deck.";
                        ++i;
                        continue;
                    case "Class Rune":
                        rewardImages[i].sprite = Resources.Load<Sprite>("RuneArt/" +
                            GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>().Spellcaster.classType + " " + kvp.Value);
                        rewardText[i].text = "Draw this from the deck.";
                        ++i;
                        continue;
                    case "Mana":
                        rewardImages[i].sprite = Resources.Load<Sprite>("Art Assets/Items and Currency/ManaCrystal");
                        rewardText[i].text = "You earned " + Int32.Parse(kvp.Value) + " mana!";
                        ++i;
                        continue;
                    case "Item":
                        rewardImages[i].sprite = Resources.Load<Sprite>("Art Assets/Items and Currency/" + kvp.Value);
                        rewardText[i].text = "You earned a " + kvp.Value + "!";
                        ++i;
                        continue;
                    case "Dice":
                        rewardImages[i].sprite = Resources.Load<Sprite>("Art Assets/Items and Currency/Blank Dice");
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

        // if proclamation panel is found in the scene, disable this panel 
        if (GameObject.Find("Proclamation Panel"))
        {
            DisablePanel();
        }

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

        PanelHolder.panelQueue.Dequeue();
        PanelHolder.instance.CheckPanelQueue();
    }
}