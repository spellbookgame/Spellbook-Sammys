using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

//Notifies the player when it is their turn.
public class NotifyUI : MonoBehaviour
{
    public Text titleText;
    public Text infoText;

    [SerializeField] private Button singleButton;

    public void DisplayNotify(string title, string info)
    {
        titleText.text = title;
        infoText.text = info;

        singleButton.onClick.AddListener((okClick));

        gameObject.SetActive(true);
    }
    public void DisplayCombat(string title, string info)
    {
        titleText.text = title;
        infoText.text = info;

        singleButton.onClick.AddListener((combatClick));

        gameObject.SetActive(true);
    }
    public void DisplayEvent(string title, string info)
    {
        titleText.text = title;
        infoText.text = info;

        singleButton.onClick.AddListener((eventClick));

        gameObject.SetActive(true);
    }

    private void okClick()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        gameObject.SetActive(false);
    }
    private void combatClick()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        gameObject.SetActive(false);
        SceneManager.LoadScene("CombatScene");
    }
    private void eventClick()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        gameObject.SetActive(false);

        Debug.Log("On click");
        //GameObject player = GameObject.Find("LocalPlayer(Clone)");
        GameObject player = GameObject.FindGameObjectWithTag("LocalPlayer");

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