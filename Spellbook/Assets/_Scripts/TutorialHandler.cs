using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Grace Ko
/// script used to manage tutorials
/// </summary>
public class TutorialHandler : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject promptPanel;
    [SerializeField] private Button promptYesButton;
    [SerializeField] private Button promptNoButton;
    [SerializeField] private Text tutorialText;
    [SerializeField] private Button okButton;
    [SerializeField] private GameObject tutorialArrow;

    private int buttonClicks;

    public GameObject[] tutorialObjects;
    public string[] tutorialTexts;

    public void PromptTutorial()
    {
        promptPanel.SetActive(true);

        promptYesButton.onClick.AddListener(() => BeginTutorial());
        promptNoButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            promptPanel.SetActive(false);

            GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>().Spellcaster.tutorialShown = true;
        });
    }

    private void BeginTutorial()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        promptPanel.SetActive(false);
        tutorialPanel.SetActive(true);
        tutorialArrow.SetActive(true);

        buttonClicks = 0;

        PositionTutorialArrow(buttonClicks, 400);
        tutorialText.text = tutorialTexts[buttonClicks];
        DisableAllExcept(tutorialObjects[buttonClicks].name);

        okButton.onClick.AddListener(() => ClickTutorial());
        ++buttonClicks;

        GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>().Spellcaster.tutorialShown = true;
    }

    private void ClickTutorial()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);

        if(buttonClicks < tutorialTexts.Length)
        {
            if(buttonClicks < tutorialObjects.Length)
            {
                tutorialText.text = tutorialTexts[buttonClicks];
                PositionTutorialArrow(buttonClicks, 400);
                DisableAllExcept(tutorialObjects[buttonClicks].name);
                ++buttonClicks;
            }
            else
            {
                tutorialText.text = tutorialTexts[buttonClicks];
                tutorialArrow.SetActive(false);
                ++buttonClicks;
            }
        }
        else
        {
            tutorialPanel.SetActive(false);
            EnableAllObjects();
        }
    }
    private void PositionTutorialArrow(int objectIndex, int yOffset)
    {
        // if the object is below the center of screen, position arrow above object, otherwise rotate/position below object
        if(tutorialObjects[objectIndex].transform.localPosition.y <= 0)
        {
            if(tutorialArrow.transform.rotation.eulerAngles.z == 270)
                tutorialArrow.transform.Rotate(0, 0, 180, Space.Self);
            tutorialArrow.GetComponent<UIAutoHover>()._startPosition = tutorialObjects[objectIndex].transform.localPosition.y + yOffset;
            tutorialArrow.transform.localPosition = tutorialObjects[objectIndex].transform.localPosition + new Vector3(0, yOffset, 0);
        }
        else
        {
            tutorialArrow.transform.Rotate(0, 0, -180, Space.Self);
            tutorialArrow.GetComponent<UIAutoHover>()._startPosition = tutorialObjects[objectIndex].transform.localPosition.y - yOffset - 200;
            tutorialArrow.transform.localPosition = tutorialObjects[objectIndex].transform.localPosition - new Vector3(0, yOffset - 200, 0);
        }
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

    private void EnableAllObjects()
    {
        foreach(GameObject g in tutorialObjects)
        {
            g.GetComponent<Button>().interactable = true;
            g.GetComponent<Button>().enabled = true;
            g.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
