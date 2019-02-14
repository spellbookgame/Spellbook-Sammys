using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

//Notifies the player when it is their turn.
public class EventUI : MonoBehaviour
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

    private void okClick()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("MainPlayerScene");
    }
}