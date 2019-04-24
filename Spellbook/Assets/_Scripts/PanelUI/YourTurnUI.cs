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

        // reset tracker bools
        GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>().Spellcaster.scannedSpaceThisTurn = false;
        GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>().Spellcaster.endTurnManaCollected = false;

        // for start of game
        if (GameObject.Find("Proclamation Panel"))
        {
            DisablePanel();
        }  

        if (!PanelHolder.panelQueue.Peek().Equals(panelID))
        {
            DisablePanel();
        }

        singleButton.onClick.AddListener(() => 
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            gameObject.SetActive(false);
            
            // bring player to main player scene
            if(!SceneManager.GetActiveScene().name.Equals("MainPlayerScene"))
            {
                SceneManager.LoadScene("MainPlayerScene");
            }

            PanelHolder.panelQueue.Dequeue();
            PanelHolder.instance.CheckPanelQueue();
        });
    }
}