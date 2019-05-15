using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

// Used to notify player it is their turn
public class YourTurnUI : MonoBehaviour
{
    [SerializeField] private Text infoText;
    [SerializeField] private Button singleButton;

    public bool panelActive = false;
    public string panelID = "yourturn";

    private void DisablePanel()
    {
        gameObject.SetActive(false);
    }
    public void EnablePanel()
    {
        gameObject.SetActive(true);
    }

    public void Display()
    {
        gameObject.SetActive(true);

        Debug.Log("num of turns so far: " + GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>().Spellcaster.NumOfTurnsSoFar);

        if (!PanelHolder.panelQueue.Peek().Equals(panelID))
        {
            DisablePanel();
        }

        singleButton.onClick.AddListener(() => 
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            gameObject.SetActive(false);

            // enable player's dice button
            UICanvasHandler.instance.EnableDiceButton(true);

            // bring player to main player scene
            if (!SceneManager.GetActiveScene().name.Equals("MainPlayerScene"))
            {
                SceneManager.LoadScene("MainPlayerScene");
                UICanvasHandler.instance.ActivateSpellbookButtons(false);
            }

            // for start of game: give players a spell quest to start
            QuestTracker.instance.GiveSpellQuest();

            if (PanelHolder.panelQueue.Count > 0)
                PanelHolder.panelQueue.Dequeue();
            PanelHolder.instance.CheckPanelQueue();
        });
    }
}