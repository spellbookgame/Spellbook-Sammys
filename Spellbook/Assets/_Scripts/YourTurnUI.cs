using UnityEngine;
using UnityEngine.SceneManagement;
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
        singleButton.onClick.AddListener(() => 
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            gameObject.SetActive(false);

            if(!SceneManager.GetActiveScene().name.Equals("MainPlayerScene"))
                SceneManager.LoadScene("MainPlayerScene");
        });

        gameObject.SetActive(true);
    }
}