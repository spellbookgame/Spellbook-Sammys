using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// script used to load different scenes
public class SceneScript : MonoBehaviour
{
    public void loadMainScene()
    {
        SceneManager.LoadScene("MainPlayerScene");
    }

    public void loadCombatScene()
    {
        SceneManager.LoadScene("CombatScene");
    }

    public void loadSpellCreateScene()
    {
        SceneManager.LoadScene("SpellCreateScene");
    }

    public void loadQRScene()
    {
        SceneManager.LoadScene("VuforiaScene");
    }

    public void loadSpellCastScene()
    {
        SceneManager.LoadScene("SpellCastScene");
    }
}
