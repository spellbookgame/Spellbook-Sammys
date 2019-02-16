using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

//Notifies the player when it is their turn.
public class NotifyUI : MonoBehaviour
{
    public Text infoText;
    public Text buttonText;
    public Button singleButton;

    public void Display(string text)
    {
        infoText.text = text;

        singleButton.onClick.AddListener((okClick));

        gameObject.SetActive(true);
    }
    public void DisplayCombat(string text)
    {
        infoText.text = text;

        singleButton.onClick.AddListener((combatClick));

        gameObject.SetActive(true);
    }
    public void DisplayEvent(string text)
    {
        infoText.text = text;

        singleButton.onClick.AddListener((eventClick));

        gameObject.SetActive(true);
    }

    private void okClick()
    {
        gameObject.SetActive(false);
    }
    private void combatClick()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("CombatScene");
    }
    private void eventClick()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("MainPlayerScene");
    }
}