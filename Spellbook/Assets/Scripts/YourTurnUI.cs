using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Notifies the player when it is their turn.
public class YourTurnUI : MonoBehaviour
{
    public Text infoText;
    public Text buttonText;
    public Button singleButton;

    public void Display()
    {
        singleButton.onClick.AddListener(() => { gameObject.SetActive(false); });

        gameObject.SetActive(true);
    }
}