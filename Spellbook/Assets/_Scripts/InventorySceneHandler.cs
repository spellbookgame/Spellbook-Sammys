using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventorySceneHandler : MonoBehaviour
{
    [SerializeField] private Button exitButton;

    private void Start()
    {
        exitButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.inventoryClose);
            SceneManager.LoadScene("MainPlayerScene");
        });
    }
}
