using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// script used to load different scenes
public class SceneScript : MonoBehaviour
{
    public static SceneScript instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

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
