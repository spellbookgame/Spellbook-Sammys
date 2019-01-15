using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

// script to manage combat in DungeonScene
public class CombatManager : MonoBehaviour
{
    [SerializeField]
    private Button Button_ok;

    [SerializeField]
    private GameObject Panel_starting;

    [SerializeField]
    private GameObject Panel_choices;

    public void onOKClick()
    {
        Panel_choices.SetActive(true);
        Panel_starting.SetActive(false);
    }
}
