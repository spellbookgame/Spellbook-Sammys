using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpellbookHandler : MonoBehaviour
{
    [SerializeField] private Button mainButton;
    [SerializeField] private Button collectionButton;
    [SerializeField] private Button questLogButton;
    [SerializeField] private Button spellbookProgressButton;
    [SerializeField] private Text activeSpellsText;

    Player localPlayer;

    // TESTING
    ItemObject item;

    // Start is called before the first frame update
    void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        // TESTING
        item = new ItemObject("Glowing Mushroom", Resources.Load<Sprite>("Art Assets/Items and Currency/GlowingMushroom"),
            1000, 500, "Oooo Glowy", "Heals 25% damage, can probably be sold to someone at a high price.");

        mainButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("MainPlayerScene");
        });
        collectionButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.pageturn);
            SceneManager.LoadScene("SpellCollectionScene");
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

    private void Update()
    {
        // TESTING
        if(Input.GetKeyDown(KeyCode.C))
        {
            localPlayer.Spellcaster.AddToInventory(item);
            Debug.Log("item added");
        }
    }
}
