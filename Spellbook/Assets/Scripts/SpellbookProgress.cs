using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpellbookProgress : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Text progressText;
    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("SpellbookScene");
        });

        progressText.text = NetworkGameState.instance.getSpellbookProgess();
    }
}
