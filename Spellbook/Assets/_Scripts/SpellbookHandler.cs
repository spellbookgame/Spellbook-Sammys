using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpellbookHandler : MonoBehaviour
{
    [SerializeField] private Button mainButton;
    [SerializeField] private Button libraryButton;
    [SerializeField] private Button collectionButton;
    [SerializeField] private Button questLogButton;
    [SerializeField] private Button spellbookProgressButton;
    [SerializeField] private Text activeSpellsText;

    Player localPlayer;

    // Start is called before the first frame update
    void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        mainButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.spellbookClose);
            SceneManager.LoadScene("MainPlayerScene");
        });
        collectionButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.pageturn);
            SceneManager.LoadScene("SpellCollectionScene");
        });
        libraryButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.pageturn);
            SceneManager.LoadScene("LibraryScene");
        });
        questLogButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.pageturn);
            SceneManager.LoadScene("QuestLogScene");
        });
        spellbookProgressButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.pageturn);
            SceneManager.LoadScene("SpellbookProgress");
        });

        // show player's active spells
        foreach (Spell entry in localPlayer.Spellcaster.activeSpells)
        {
            activeSpellsText.text = activeSpellsText.text + entry.sSpellName + "\n";
        }
    }
}
