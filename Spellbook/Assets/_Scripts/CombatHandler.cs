using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// script to manage UI in CombatScene
// slider from https://www.youtube.com/watch?v=GfuxWs6UAJQ
public class CombatHandler : MonoBehaviour
{
    // buttons
    [SerializeField] private Button attackButton;
    [SerializeField] private Button mainButton;
    [SerializeField] private Button spellbookButton;

    // for the player
    [SerializeField] private Slider Slider_healthbar;
    [SerializeField] private Text Text_healthtext;

    // for the enemy
    [SerializeField] private Slider Slider_enemyHealthBar;
    [SerializeField] private Text Text_enemyHealthText;

    Player localPlayer;
    Enemy enemy;
    public static CombatHandler instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        setUpCombatHandler();    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
            localPlayer.Spellcaster.TakeDamage(6);
        if (Input.GetKeyDown(KeyCode.C))
            enemy.HitEnemy(6);

        UpdatePlayerStats();
        UpdateEnemyStats();
    }

// ------------------------------------------------------ COMBAT/HEALTH -----------------------------------------------------
    // calculating health to decrease from slider
    private float CalculatePlayerHealth()
    {
        return localPlayer.Spellcaster.fCurrentHealth / localPlayer.Spellcaster.fMaxHealth;
    }
    private float CalculateEnemyHealth()
    {
        return enemy.fCurrentHealth / enemy.fMaxHealth;
    }

    // change slider values
    public void UpdatePlayerStats()
    {
        Slider_healthbar.value = CalculatePlayerHealth();
        Text_healthtext.text = localPlayer.Spellcaster.fCurrentHealth.ToString();
    }
    public void UpdateEnemyStats()
    {
        Slider_enemyHealthBar.value = CalculateEnemyHealth();
        Text_enemyHealthText.text = enemy.fCurrentHealth.ToString();
    }

    // player's basic attack
    public void attackClick()
    {
        if (localPlayer.Spellcaster.hasAttacked == false)
        {
            localPlayer.Spellcaster.fBasicAttackStrength = Random.Range(1, 6);
            if (GameObject.FindGameObjectWithTag("Enemy") != null)
                enemy.HitEnemy(localPlayer.Spellcaster.fBasicAttackStrength);
            localPlayer.Spellcaster.hasAttacked = true;
        }
        else
        {
            //PanelHolder.instance.displayNotify("Oops!", "You already attacked this turn.");
        }
    }

    public void setUpCombatHandler()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        if (localPlayer == null)
        {
            Debug.Log("local player is null");
        }

        // check to see if enemy exists
        if(GameObject.FindGameObjectWithTag("Enemy"))
            enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();

        // setting the slider and text value to current health
        Slider_healthbar.value = CalculatePlayerHealth();
        Text_healthtext.text = localPlayer.Spellcaster.fCurrentHealth.ToString();

        Slider_enemyHealthBar.value = CalculateEnemyHealth();
        Text_enemyHealthText.text = enemy.fCurrentHealth.ToString();

        // adding onclick listeners to buttons
        attackButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            attackClick();
        });
        mainButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("MainPlayerScene");
        });
        spellbookButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("SpellbookScene");
        });
    }
}
