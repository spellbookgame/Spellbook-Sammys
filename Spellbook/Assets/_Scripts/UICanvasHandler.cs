using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UICanvasHandler : MonoBehaviour
{
    public static UICanvasHandler instance = null;

    [SerializeField] private GameObject spellbookButton;
    [SerializeField] private GameObject diceButton;
    [SerializeField] private GameObject inventoryButton;
    [SerializeField] private GameObject endTurnButton;
    [SerializeField] private DiceUIHandler diceUIHandler;

    private Player localPlayer;

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

        // find local player
        if(GameObject.FindGameObjectWithTag("LocalPlayer"))
            localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        // only show buttons on main scene
        if (!SceneManager.GetActiveScene().name.Equals("MainPlayerScene"))
        {
            // if dice tray is open in another scene other than main player, close it
            if(diceUIHandler.diceTrayOpen)
            {
                diceUIHandler.OpenCloseDiceTray();
                diceUIHandler.diceTrayOpen = false;
            }

            spellbookButton.SetActive(false);
            diceButton.SetActive(false);
            inventoryButton.SetActive(false);
            endTurnButton.SetActive(false);
        }
        else
        {
            // if player has rolled, set EndTurnButton active
            if (localPlayer != null && localPlayer.Spellcaster.hasRolled)
                ActivateEndTurnButton();
            else
                DeactivateEndTurnButton();

            spellbookButton.SetActive(true);
            diceButton.SetActive(true);
            inventoryButton.SetActive(true);
        }
    }

    public void ActivateEndTurnButton()
    {
        // set end turn button active
        endTurnButton.SetActive(true);
    }

    public void DeactivateEndTurnButton()
    {
        // set end turn button active
        endTurnButton.SetActive(false);
    }

    public void EnableDisableDiceButton(bool enabled)
    {
        diceButton.GetComponent<Button>().interactable = enabled;
        diceButton.transform.GetChild(0).gameObject.SetActive(enabled);
    }
}
