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

    public GameObject[] tutorialObjects;

    private void Start()
    {
        promptYesButton.onClick.AddListener(() => BeginTutorial());
        promptNoButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            gameObject.SetActive(false);

            GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>().Spellcaster.tutorialShown = true;
        });
    }

    private void BeginTutorial()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        gameObject.SetActive(false);
        tutorialPanel.SetActive(true);
        tutorialArrow.SetActive(true);

        okButton.onClick.AddListener(() => ClickTutorial());

        PositionTutorialArrow(0, 375);
        DisableAllExcept(tutorialObjects[0].name);

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
                PositionTutorialArrow(1, 375);
                DisableAllExcept(tutorialObjects[1].name);
                ++buttonClicks;
                break;
            case 1:
                tutorialText.text = "Use the scanner if you've arrived at a location, or if you want to create a spell.";
                PositionTutorialArrow(2, 375);
                DisableAllExcept(tutorialObjects[2].name);
                ++buttonClicks;
                break;
            case 2:
                tutorialText.text = "Open up your Inventory to view items you've collected.";
                PositionTutorialArrow(3, 375);
                DisableAllExcept(tutorialObjects[3].name);
                ++buttonClicks;
                break;
            case 3:
                tutorialText.text = "Collect runes to create spells before the crisis arrives! Good luck!";
                tutorialArrow.SetActive(false);
                ++buttonClicks;
                break;
            case 4:
                tutorialPanel.SetActive(false);
                UICanvasHandler.instance.EnableMainSceneButtons(true);
                break;
        }
    }
    private void PositionTutorialArrow(int objectIndex, int yOffset)
    {
        tutorialArrow.transform.localPosition = tutorialObjects[objectIndex].transform.localPosition + new Vector3(0, yOffset, 0);
    }

    private void DisableAllExcept(string objectName)
    {
        foreach(GameObject g in tutorialObjects)
        {
            g.GetComponent<Button>().interactable = false;

            if(!g.name.Equals(objectName))
            {
                g.transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                g.GetComponent<Button>().interactable = true;
                g.GetComponent<Button>().enabled = false;
                g.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
