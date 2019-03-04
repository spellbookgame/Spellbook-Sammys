using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpellbookHandler : MonoBehaviour
{
    [SerializeField] private Button spellCreateButton;
    [SerializeField] private Button spellCastButton;
    [SerializeField] private Button mainButton;
    [SerializeField] private Button collectionButton;
    [SerializeField] private Button spellbookProgressButton;
    [SerializeField] private Text glyphText;
    [SerializeField] private Text activeSpellsText;

    Player localPlayer;

    // Start is called before the first frame update
    void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        spellCreateButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("SpellCreateScene");
        });
        spellCastButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("SpellCastScene");
        });
        mainButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("MainPlayerScene");
        });
        collectionButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("SpellCollectionScene");
        });
        spellbookProgressButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("SpellbookProgress");
        });

        // show player how many glyphs they have
        foreach (KeyValuePair<string, int> kvp in localPlayer.Spellcaster.glyphs)
        {
            if (kvp.Value > 0)
            {
                glyphText.text = glyphText.text + kvp.Key + ": " + kvp.Value + "\n";
            }
        }

        // show player's active spells
        foreach (Spell entry in localPlayer.Spellcaster.activeSpells)
        {
            activeSpellsText.text = activeSpellsText.text + entry.sSpellName + "\n";
        }
    }
}
