using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPageHandler : MonoBehaviour
{
    [SerializeField] private Text manaCrystalsValue;
    [SerializeField] private Text activeSpellsValue;
    [SerializeField] private Text classText;
    [SerializeField] private Enemy enemy;

    [SerializeField] private Button scanButton;
    [SerializeField] private Button createSpellButton;
    [SerializeField] private Button castSpellButton;
    [SerializeField] private Button combatButton;

    Player localPlayer;
    public static MainPageHandler instance = null;

    void Awake()
    {
        //Check if there is already an instance of MainPageHandler
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of MainPageHandler.
            Destroy(gameObject);
    }

    private void Start()
    {
        setupMainPage();
    }


    public void setupMainPage()
    {
        if (GameObject.FindGameObjectWithTag("LocalPlayer") == null) return;
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        manaCrystalsValue.text = localPlayer.Spellcaster.iMana.ToString();

        classText.text = "You are playing as " + localPlayer.Spellcaster.classType;

        foreach (string entry in localPlayer.Spellcaster.activeSpells)
        {
            activeSpellsValue.text = activeSpellsValue.text + entry + "\n";
        }

        // if an enemy does not exist, create one
        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            // instantiating enemy with 20 health
            enemy = Instantiate(enemy);
            enemy.Initialize(20f);
            enemy.fCurrentHealth = enemy.fMaxHealth;
        }

        // set onclick listeners for buttons
        scanButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("VuforiaScene");
        });
        createSpellButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("SpellCreateScene");
        });
        castSpellButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("SpellCastScene");
        });
        combatButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("CombatScene");
        });
    }
}
