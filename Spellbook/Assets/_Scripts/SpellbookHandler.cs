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
    [SerializeField] private Button questLogButton;
    [SerializeField] private Button spellbookProgressButton;
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
        questLogButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("QuestLogScene");
        });
        spellbookProgressButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("SpellbookProgress");
        });

        // show player's active spells
        foreach (Spell entry in localPlayer.Spellcaster.activeSpells)
        {
            activeSpellsText.text = activeSpellsText.text + entry.sSpellName + "\n";
        }
    }

    private void Update()
    {
        // disable some buttons if not player's turn
        if (!localPlayer.bIsMyTurn)
        {
            spellCastButton.enabled = false;
            spellCreateButton.enabled = false;
        }
        else
        {
            spellCastButton.enabled = true;
            spellCreateButton.enabled = true;
        }
    }
}
