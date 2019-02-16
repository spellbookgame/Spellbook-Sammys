using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainPageHandler : MonoBehaviour
{
    [SerializeField] private Text manaCrystalsValue;
    [SerializeField] private Text activeSpellsValue;
    [SerializeField] private Enemy enemy;

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
    }
}
