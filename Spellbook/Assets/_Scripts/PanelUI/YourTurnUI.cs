﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

// Used to notify player it is their turn
public class YourTurnUI : MonoBehaviour
{
    [SerializeField] private Text infoText;
    [SerializeField] private Button singleButton;

    public bool panelActive = false;
    public string panelID = "yourturn";

    private void DisablePanel()
    {
        gameObject.SetActive(false);
    }
    public void EnablePanel()
    {
        gameObject.SetActive(true);
    }

    public void Display()
    {
        Player localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        SoundManager.instance.PlaySingle(SoundManager.yourturn);
        // set bgm volume back up if it's player's turn
        SoundManager.instance.musicSource.volume = 1f;
        // check to see if we should play main BGM
        if (localPlayer.Spellcaster.PlayMainBGM())
            SoundManager.instance.PlayGameBCM(SoundManager.gameBCG);

        gameObject.SetActive(true);
        if (!PanelHolder.panelQueue.Peek().Equals(panelID))
        {
            DisablePanel();
        }

        singleButton.onClick.AddListener(() => 
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            gameObject.SetActive(false);

            // enable player's dice button
            UICanvasHandler.instance.EnableDiceButton(true);

        // bring player to main player scene
        if (!SceneManager.GetActiveScene().name.Equals("MainPlayerScene") && !SceneManager.GetActiveScene().name.Equals("CombatSceneV2"))
            {
                SceneManager.LoadScene("MainPlayerScene");
                UICanvasHandler.instance.ActivateSpellbookButtons(false);
            }

            if (PanelHolder.panelQueue.Count > 0)
                PanelHolder.panelQueue.Dequeue();
            PanelHolder.instance.CheckPanelQueue();
        });
    }
}