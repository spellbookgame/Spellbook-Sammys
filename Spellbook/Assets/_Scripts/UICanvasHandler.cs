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
    [SerializeField] private GameObject libraryButton;
    [SerializeField] private GameObject questButton;
    [SerializeField] private GameObject progressButton;
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

    // called once when UICanvasHandler is instantiated
    private void Start()
    {
        // set onclick listeners once in the game
        libraryButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.pageturn);
            SceneManager.LoadScene("LibraryScene");
        });
        questButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.pageturn);
            SceneManager.LoadScene("QuestLogScene");
        });
        progressButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.pageturn);
            SceneManager.LoadScene("SpellbookProgress");
        });

        // initially position the buttons properly on main player scene
        spellbookButton.transform.localPosition = new Vector3(-475, -1225, 0);
        diceButton.transform.localPosition = new Vector3(0, -1225, 0);
        inventoryButton.transform.localPosition = new Vector3(475, -1225, 0);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // set render camera to main camera
        gameObject.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // only show buttons on main scene
        if (!SceneManager.GetActiveScene().name.Equals("MainPlayerScene"))
        {
            // if dice tray is open in another scene other than main player, close it
            if (diceUIHandler.diceTrayOpen)
            {
                diceUIHandler.OpenCloseDiceTray();
                diceUIHandler.diceTrayOpen = false;
            }

            spellbookButton.SetActive(false);
            diceButton.SetActive(false);
            inventoryButton.SetActive(false);
            endTurnButton.SetActive(false);
        }
        // if we're in the main scene
        else
        {
            // if player has rolled, set EndTurnButton active
            if (localPlayer != null && localPlayer.Spellcaster.hasRolled)
                ActivateEndTurnButton(true);
            else
                ActivateEndTurnButton(false);

            spellbookButton.SetActive(true);
            diceButton.SetActive(true);
            inventoryButton.SetActive(true);
        }
    }

    // activate end turn button if player has rolled
    public void ActivateEndTurnButton(bool enabled)
    {
        endTurnButton.SetActive(enabled);

        if(enabled)
        {
            // move main page buttons up
            spellbookButton.transform.localPosition = new Vector3(-475, -1015, 0);
            diceButton.transform.localPosition = new Vector3(0, -1015, 0);
            inventoryButton.transform.localPosition = new Vector3(475, -1015, 0);
        }
        else
        {
            // move main page buttons down
            spellbookButton.transform.localPosition = new Vector3(-475, -1225, 0);
            diceButton.transform.localPosition = new Vector3(0, -1225, 0);
            inventoryButton.transform.localPosition = new Vector3(475, -1225, 0);
        }
    }

    // enable dice button if it's player's turn
    public void EnableDiceButton(bool enabled)
    {
        diceButton.GetComponent<Button>().interactable = enabled;
        diceButton.transform.GetChild(0).gameObject.SetActive(enabled);
    }

    // set the spellbook buttons active if in spellbook scene
    public void ActivateSpellbookButtons(bool enabled)
    {
        libraryButton.SetActive(enabled);
        questButton.SetActive(enabled);
        progressButton.SetActive(enabled);
    }
}
