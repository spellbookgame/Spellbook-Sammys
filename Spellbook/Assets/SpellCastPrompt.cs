using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpellCastPrompt : MonoBehaviour
{
    [SerializeField] private Button buttonYes;
    [SerializeField] private Button buttonNo;

    private void Start()
    {
        buttonYes.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("SpellCastScene");
            gameObject.SetActive(false);
        });
        buttonNo.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            gameObject.SetActive(false);
        });
    }
}