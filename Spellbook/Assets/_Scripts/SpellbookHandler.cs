using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpellbookHandler : MonoBehaviour
{
    [SerializeField] private Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        exitButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.spellbookClose);
            UICanvasHandler.instance.ActivateSpellbookButtons(false);
            SceneManager.LoadScene("MainPlayerScene");
        });
    }
}
