using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

public class UICanvasHandler : MonoBehaviour
{
    public static UICanvasHandler instance = null;

    [SerializeField] private GameObject spellbookButton;
    [SerializeField] private GameObject diceButton;
    [SerializeField] private GameObject inventoryButton;
    [SerializeField] private GameObject endTurnButton;

    void Awake()
    {
        //Check if there is already an instance of UICanvasHandler
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of UICanvasHandler.
            Destroy(gameObject);

        //Set UICanvasHandler to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded()
    {
        // set render camera to main camera
        gameObject.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // only show buttons on main scene
        if(!SceneManager.GetActiveScene().name.Equals("MainPlayerScene"))
        {
            spellbookButton.SetActive(false);
            diceButton.SetActive(false);
            inventoryButton.SetActive(false);
            endTurnButton.SetActive(false);
        }
        else
        {
            spellbookButton.SetActive(true);
            diceButton.SetActive(true);
            inventoryButton.SetActive(true);
            endTurnButton.SetActive(true);
        }
    }
}
