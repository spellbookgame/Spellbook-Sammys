using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Grace Ko
/// script used to manage tutorial at start of game
/// </summary>
public class TutorialHandler : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private Button promptYesButton;
    [SerializeField] private Button promptNoButton;
    [SerializeField] private Text tutorialText;
    [SerializeField] private Button okButton;
    [SerializeField] private GameObject tutorialArrow;

    private int buttonClicks = 0;

    private void Start()
    {
        promptYesButton.onClick.AddListener(() => BeginTutorial());
        promptNoButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            gameObject.SetActive(false);

            GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>().Spellcaster.tutorialShown = true;
        });
        okButton.onClick.AddListener(() => ClickTutorial());
    }

    private void BeginTutorial()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        gameObject.SetActive(false);
        tutorialPanel.SetActive(true);
        tutorialArrow.SetActive(true);

        GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>().Spellcaster.tutorialShown = true;
    }

    private void ClickTutorial()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);

        // switch text every time button is clicked
        switch(buttonClicks)
        {
            case 0:
                tutorialText.text = "Open the Dice Tray to roll and move!";
                tutorialArrow.transform.localPosition += new Vector3(350, 0, 0);
                ++buttonClicks;
                break;
            case 1:
                tutorialText.text = "Use the scanner if you've arrived at a location, or if you want to create a spell.";
                tutorialArrow.transform.localPosition += new Vector3(350, 0, 0);
                ++buttonClicks;
                break;
            case 2:
                tutorialText.text = "Open up your Inventory to view items you've collected.";
                tutorialArrow.transform.localPosition += new Vector3(350, 0, 0);
                ++buttonClicks;
                break;
            case 3:
                tutorialText.text = "Collect runes to create spells before the crisis arrives! Good luck!";
                tutorialArrow.SetActive(false);
                ++buttonClicks;
                break;
            case 4:
                tutorialPanel.SetActive(false);
                break;
        }
    }
}
