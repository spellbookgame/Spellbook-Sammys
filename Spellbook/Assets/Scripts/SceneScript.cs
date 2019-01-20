using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// script used to load different scenes
public class SceneScript : MonoBehaviour
{
    public void loadDungeonScene()
    {
        SceneManager.LoadScene("CombatScene");
    }

    public void loadSpellcastScene()
    {
        SceneManager.LoadScene("SpellcastScene");
    }
}
