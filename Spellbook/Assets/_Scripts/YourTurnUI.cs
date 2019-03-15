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
        if (!PanelHolder.instance.panelQueue.Peek().Equals(panelID))
        {
            DisablePanel();
        }

        gameObject.SetActive(true);
        
        singleButton.onClick.AddListener(() => 
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            gameObject.SetActive(false);
            
            if(!SceneManager.GetActiveScene().name.Equals("MainPlayerScene"))
                SceneManager.LoadScene("MainPlayerScene");

            PanelHolder.instance.panelQueue.Dequeue();
            PanelHolder.instance.CheckPanelQueue();
        });
    }
}