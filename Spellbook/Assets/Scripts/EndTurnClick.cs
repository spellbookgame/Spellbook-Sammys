using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTurnClick : MonoBehaviour
{
    public SceneScript sceneScript;
    public void OnEndTurnClick()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);

        Debug.Log("On click");
        //GameObject player = GameObject.Find("LocalPlayer(Clone)");
        GameObject player = GameObject.FindGameObjectWithTag("LocalPlayer");

        bool endSuccessful = player.GetComponent<Player>().onEndTurnClick();
        if (endSuccessful)
        {
            Scene m_Scene = SceneManager.GetActiveScene();
            if(m_Scene.name != "MainPlayerScene")
            {
                sceneScript.loadMainScene();
            }
           
        }
        
    }
}
