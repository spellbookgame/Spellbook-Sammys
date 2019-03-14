using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public Text titleText;
    public Text infoText;

    [SerializeField] private Button singleButton;
    [SerializeField] private Button singleButton1;

    // use this if quest rewards are glyphs
    public void DisplayQuestGlyphs(Quest quest)
    {
        titleText.text = quest.questName;
        infoText.text = quest.questDescription + "\nTurn Limit: " + quest.turnLimit;

        gameObject.SetActive(true);

        string reward1 = "", reward2 = "";
        // getting the glyph rewards of the quest and loading its image
        foreach(KeyValuePair<string, List<string>> kvp in quest.rewards)
        {
            if(kvp.Key.Equals("Glyph"))
            {
                reward1 = kvp.Value[0];
                reward2 = kvp.Value[1];
            }
        }

        // setting panel images to glyphs to display rewards
        gameObject.transform.Find("image_reward1").GetComponent<Image>().sprite = Resources.Load<Sprite>("GlyphArt/" + reward1);
        gameObject.transform.Find("image_reward2").GetComponent<Image>().sprite = Resources.Load<Sprite>("GlyphArt/" + reward2);

        singleButton.onClick.AddListener(() => buttonClicked("accept", quest));
        singleButton1.onClick.AddListener(() => buttonClicked("deny", quest));
    }

    private void buttonClicked(string input, Quest q)
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        gameObject.SetActive(false);
        
        GameObject player = GameObject.FindGameObjectWithTag("LocalPlayer");

        // add quest to player's list of active quests if they accept
        if (input.Equals("accept"))
            player.GetComponent<Player>().Spellcaster.activeQuests.Add(q);

        // end player's turn and take them back to main page
        bool endSuccessful = player.GetComponent<Player>().onEndTurnClick();
        if (endSuccessful)
        {
            player.GetComponent<Player>().Spellcaster.hasAttacked = false;
            Scene m_Scene = SceneManager.GetActiveScene();
            if (m_Scene.name != "MainPlayerScene")
            {
                SceneManager.LoadScene("MainPlayerScene");
            }

        }
    }
}
