using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DiceRoll : MonoBehaviour
{
    [SerializeField] private GameObject TextBox;
    [SerializeField] private Button rollButton;

    private int diceRoll;
    private int pressedNum;
    Player localPlayer;

    private void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        pressedNum = 0;
        rollButton.onClick.AddListener(() => NumberGenerator());
    }

    public void NumberGenerator() 
    {
        // when button is clicked for first time, generate random number and change button to Scan
        if(pressedNum == 0)
        {
            diceRoll = Random.Range(1, 7);
            TextBox.GetComponent<Text>().text = diceRoll.ToString();

            localPlayer.Spellcaster.spacesTraveled += diceRoll;
            QuestTracker.instance.CheckMoveQuest(diceRoll);

            rollButton.GetComponentInChildren<Text>().text = "Scan!";
            ++pressedNum;
        }
        else if(pressedNum >= 1)
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene("VuforiaScene");
        }
    }
}
