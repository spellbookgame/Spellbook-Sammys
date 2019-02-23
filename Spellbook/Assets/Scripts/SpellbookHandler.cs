using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpellbookHandler : MonoBehaviour
{
    [SerializeField] private Button spellCreateButton;
    [SerializeField] private Button spellCastButton;
    [SerializeField] private Text glyphText;

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

        // show player how many glyphs they have
        foreach (KeyValuePair<string, int> kvp in localPlayer.Spellcaster.glyphs)
        {
            if (kvp.Value > 0)
            {
                glyphText.text = glyphText.text + kvp.Key + ": " + kvp.Value + "\n";
            }
        }
    }

    private void Update()
    {
        
    }
}
