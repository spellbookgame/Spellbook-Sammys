using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTurnClick : MonoBehaviour
{
    public void OnEndTurnClick()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);

        Player localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        bool endSuccessful = localPlayer.onEndTurnClick();
        if (endSuccessful)
        {
            // disable buttons
            UICanvasHandler.instance.ActivateEndTurnButton(false);
            UICanvasHandler.instance.EnableDisableDiceButton(false);

            localPlayer.Spellcaster.hasAttacked = false;
            localPlayer.Spellcaster.hasRolled = false;

            Scene m_Scene = SceneManager.GetActiveScene();
            if(m_Scene.name != "MainPlayerScene")
            {
                SceneManager.LoadScene("MainPlayerScene");
            }
        }
        
    }
}
