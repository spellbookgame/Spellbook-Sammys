using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UICanvasHandler : MonoBehaviour
{
    public static UICanvasHandler instance = null;

    // public variables
    public int spacesMoved = 0; // reset on EndTurn click

    #region private_fields
    [SerializeField] private GameObject spellbookButton;
    [SerializeField] private GameObject diceButton;
    [SerializeField] private GameObject inventoryButton;
    [SerializeField] private GameObject endTurnButton;
    [SerializeField] private GameObject spellbookMainButton;
    [SerializeField] private GameObject libraryButton;
    [SerializeField] private GameObject questButton;
    [SerializeField] private GameObject progressButton;
    [SerializeField] private GameObject movePanel;
    [SerializeField] private DiceUIHandler diceUIHandler;
    [SerializeField] private GameObject combatButton;
    [SerializeField] private GameObject scanButton;
    
    #endregion

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
        spellbookMainButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.pageturn);
            SceneManager.LoadScene("SpellbookScene");
        });
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
        scanButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("VuforiaScene");
        });

        // initially position the buttons properly on main player scene
        spellbookButton.transform.localPosition = new Vector3(-475, -1225, 0);
        diceButton.transform.localPosition = new Vector3(0, -1225, 0);
        inventoryButton.transform.localPosition = new Vector3(475, -1225, 0);
        scanButton.transform.localPosition = new Vector3(0, -800, 0);
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
        // find local player
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        // set render camera to main camera
        gameObject.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // if we're not in main scene main scene
        if (!SceneManager.GetActiveScene().name.Equals("MainPlayerScene"))
        {
            // if dice tray is open but moved scenes, close dice tray
            if(diceUIHandler.diceTrayOpen)
            {
                diceUIHandler.OpenCloseDiceTray();
            }

            // deactive main buttons
            spellbookButton.SetActive(false);
            diceButton.SetActive(false);
            inventoryButton.SetActive(false);
            endTurnButton.SetActive(false);
            scanButton.SetActive(false);
        }
        // if we're in the main scene
        else
        {
            //Temporary
            combatButton.SetActive(true);

            if (localPlayer != null)
                ActivateEndTurnButton(localPlayer.Spellcaster.hasRolled);

            spellbookButton.SetActive(true);
            diceButton.SetActive(true);
            inventoryButton.SetActive(true);
            scanButton.SetActive(true);
        }
    }

    // activate end turn button if player has rolled
    public void ActivateEndTurnButton(bool enabled)
    {
        endTurnButton.SetActive(enabled);
        Debug.Log("end turn button: " + enabled);

        if(enabled)
        {
            // move main page buttons up
            spellbookButton.transform.localPosition = new Vector3(-475, -1015, 0);
            diceButton.transform.localPosition = new Vector3(0, -1015, 0);
            inventoryButton.transform.localPosition = new Vector3(475, -1015, 0);
            scanButton.transform.localPosition = new Vector3(0, -600, 0);
        }
        else
        {
            // move main page buttons down
            spellbookButton.transform.localPosition = new Vector3(-475, -1225, 0);
            diceButton.transform.localPosition = new Vector3(0, -1225, 0);
            inventoryButton.transform.localPosition = new Vector3(475, -1225, 0);
            scanButton.transform.localPosition = new Vector3(0, -800, 0);
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
        spellbookMainButton.SetActive(enabled);
        libraryButton.SetActive(enabled);
        questButton.SetActive(enabled);
        progressButton.SetActive(enabled);
    }

    public void ShowMovePanel()
    {
        StartCoroutine(StartMovePanel());
    }

    private IEnumerator StartMovePanel()
    {
        yield return new WaitForSeconds(1f);
        // close dice tray
        if(diceUIHandler.diceTrayOpen)
            diceUIHandler.OpenCloseDiceTray();
        // set move text
        movePanel.transform.GetChild(0).GetComponent<Text>().text = "Move " + spacesMoved.ToString();
        movePanel.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        movePanel.SetActive(false);
    }

    //Temporary
    public void Combat()
    {
        combatButton.SetActive(false);
        SceneManager.LoadScene("CombatSceneV2");
    }
}
