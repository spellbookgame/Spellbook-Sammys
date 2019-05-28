using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CompassSceneHandler : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Image picture;

    private void Start()
    {
        exitButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("MainPlayerScene");
        });
    }
}
