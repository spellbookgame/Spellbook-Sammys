using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject proclamationPanel;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button panelButton;
    [SerializeField] private Animator anim;

    private Player player;

    public Color defeatedColor;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        StartCoroutine("FadeIn");

        if (player.Spellcaster.gameLost)
        {
            Transform ribbon = proclamationPanel.transform.GetChild(2);
            foreach(Transform t in ribbon)
            {
                t.gameObject.GetComponent<SpriteRenderer>().color = defeatedColor;
            }
            gameOverText.text = "Game Over";
            proclamationPanel.transform.GetChild(1).GetComponent<Text>().text = "This is a notice of warning to the Empire. All citizens must read carefully.\n\n" +
                                                                                "The Empire has been overrun by the Black Mage.\nThe Council has decided to surrender, at the best interest of all citizens." +
                                                                                "\n\nSpellcasters must NOT engage in combat. We repeat, do NOT engage in combat.";
        }

        panelButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.parchmentBurn);
            StartCoroutine("SetObjectsActive");
        });

        restartButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);

            // System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".exe"));
            Application.Quit();
        });

        quitButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);

            Application.Quit();
        });
    }

    private IEnumerator FadeIn()
    {
        anim.SetBool("FadeIn", true);
        yield return new WaitUntil(() => anim.gameObject.GetComponent<Image>().color.a == 0);
        anim.gameObject.SetActive(false);
    }

    private IEnumerator SetObjectsActive()
    {
        yield return new WaitForSeconds(2f);

        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }
}
