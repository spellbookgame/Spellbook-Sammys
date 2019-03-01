using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// put this script into the scene
/// drag the button prefab into the inspector field
/// </summary>
public class SpellCastHandler : MonoBehaviour
{
    [SerializeField] Button spellButton;
    [SerializeField] private Button mainButton;
    [SerializeField] private Button backButton;

    Player localPlayer;

    private void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        int yPos = 780;
        // add buttons for each spell the player has collected
        for (int i = 0; i < localPlayer.Spellcaster.chapter.spellsCollected.Count; i++)
        {
            Button newSpellButton = Instantiate(spellButton);
            newSpellButton.transform.parent = GameObject.Find("Canvas").transform;
            newSpellButton.GetComponentInChildren<Text>().text = localPlayer.Spellcaster.chapter.spellsCollected[i].sSpellName;
            newSpellButton.transform.localPosition = new Vector3(0, yPos, 0);

            // new int to pass into button onClick listener so loop will not throw index out of bounds error
            int i2 = i;
            // add listener to button
            newSpellButton.onClick.AddListener(() => localPlayer.Spellcaster.chapter.spellsCollected[i2].SpellCast(localPlayer.Spellcaster));

            // to position new button underneath prev button
            yPos -= 150;
        }

        // adding onclick listeners to buttons
        mainButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("MainPlayerScene");
        });
        backButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("SpellbookScene");
        });
    }
}
