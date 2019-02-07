using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// script used to load different scenes
public class SceneScript : MonoBehaviour
{
    public void loadMainScene()
    {
        SceneManager.LoadScene("SpellBookGame");
    }

    public void loadDungeonScene()
    {
        SceneManager.LoadScene("CombatScene");
    }

    public void loadSpellCreateScene()
    {
        SceneManager.LoadScene("SpellCreateScene");
    }

    public void loadQRScene()
    {
        SceneManager.LoadScene("VuforiaTestScene");
    }
}
