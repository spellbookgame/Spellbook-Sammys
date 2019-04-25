using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void Start()
    {
        // wait for localPlayer to initialize and THEN look for it
        StartCoroutine("SetupLocalPlayer");
    }

    IEnumerator SetupLocalPlayer()
    {
        yield return new WaitForSeconds(1.0f);
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
    }

    private void OnLevelWasLoaded()
    {
        // set render camera to main camera
        gameObject.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // only show buttons on main scene
        if(!SceneManager.GetActiveScene().name.Equals("MainPlayerScene"))
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
            else if(localPlayer != null && !localPlayer.Spellcaster.hasRolled)
                DeactivateEndTurnButton();

            spellbookButton.SetActive(true);
            diceButton.SetActive(true);
            inventoryButton.SetActive(true);
        }
    }

    public void ActivateEndTurnButton()
    {
        // move 3 buttons up
        spellbookButton.transform.localPosition = new Vector3(-470, -800, 0);
        diceButton.transform.localPosition = new Vector3(0, -800, 0);
        inventoryButton.transform.localPosition = new Vector3(470, -800, 0);

        // set end turn button active
        endTurnButton.SetActive(true);
    }

    public void DeactivateEndTurnButton()
    {
        // move buttons back to original place
        spellbookButton.transform.localPosition = new Vector3(-470, -1000, 0);
        diceButton.transform.localPosition = new Vector3(0, -1000, 0);
        inventoryButton.transform.localPosition = new Vector3(470, -1000, 0);

        // set end turn button active
        endTurnButton.SetActive(false);
    }
}
