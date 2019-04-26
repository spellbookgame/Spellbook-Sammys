using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpellbookProgress : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Text progressText;

    [SerializeField] private Button crisisButton;
    [SerializeField] private GameObject crisisPanel;
    [SerializeField] private Text crisisNameText;
    [SerializeField] private Text crisisDetailsText;
    [SerializeField] private Text crisisConsequenceText;
    [SerializeField] private Text crisisRewardText;
    [SerializeField] private Text roundsLeftText;

    private bool crisisPanelOpen;

    // Start is called before the first frame update
    void Start()
    {
        exitButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("MainPlayerScene");
        });
        backButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("SpellbookScene");
        });

        // set crisis button
        crisisButton.transform.GetChild(0).GetComponent<Text>().text = "Current Crisis: " + CrisisHandler.instance.crisisName;
        crisisButton.onClick.AddListener((DisplayCrisisPanel));

        progressText.text = NetworkGameState.instance.getSpellbookProgess();
    }

    private void DisplayCrisisPanel()
    {
        if (crisisPanelOpen)
        {
            crisisPanel.SetActive(false);
            crisisPanelOpen = false;
        }
        else
        {
            crisisNameText.text = CrisisHandler.instance.crisisName;
            crisisDetailsText.text = CrisisHandler.instance.crisisDetails;
            crisisConsequenceText.text = "FAIL: " + CrisisHandler.instance.crisisConsequence;
            crisisRewardText.text = "SUCCEED: " + CrisisHandler.instance.crisisReward;
            roundsLeftText.text = "Rounds Left: " + CrisisHandler.instance.roundsUntilCrisis;

            crisisPanel.SetActive(true);
            crisisPanelOpen = true;
        }
    }
}
