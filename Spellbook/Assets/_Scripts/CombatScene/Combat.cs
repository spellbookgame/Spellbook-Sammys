﻿using Bolt.Samples.Photon.Lobby;
using DigitalRubyShared;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Combat : MonoBehaviour
{
    public Vector2 WorldUnitsInCamera;
    public Vector2 WorldToPixelAmount;
    public FingersImageGestureHelperComponentScript ImageScript;
    public ParticleSystem MatchParticleSystem;
    public AudioSource AudioSourceOnMatch;
    bool hasDrawned = false;
    bool firstTime = true;
    float lastX = 0;
    float lastY = 0;
    public Button ResetButton;
    public Button basicAttackButton;
    public Text SwipeInstructionText;
    public Spell selectedSpell;
    public SpellCaster localSpellcaster;
    public float orbPercentage;
    public bool isInBossPanel = false;
    public GameObject BossHealthBar;
    public GameObject PlayerHealthBar;
    public GameObject DialogueField;
    public SwipeGuideSpawner swipeGuideSpawner;
    public bool onlyBasicAttack = false;
    private bool hasDoneBasicAttack = false;

    [SerializeField] private GameObject spellProjectile;
    [SerializeField] private Text damageText;

    private void LinesUpdated(object sender, System.EventArgs args)
    {
        if (isInBossPanel)
        {
            hasDrawned = true;
            //Debug.LogFormat("Lines updated, new point: {0},{1}", ImageScript.Gesture.FocusX, ImageScript.Gesture.FocusY);
            lastX = ImageScript.Gesture.FocusX;
            lastY = ImageScript.Gesture.FocusY;
        }
    }

    private void LinesCleared(object sender, System.EventArgs args)
    {
        //Debug.LogFormat("Lines cleared!");
    }

    private void Awake()
    {
        GameObject p = GameObject.FindGameObjectWithTag("LocalPlayer");
        if (p != null)
        {
            Debug.Log("Combat found local player");
            localSpellcaster = p.GetComponent<Player>().Spellcaster;
        }
        else
        {
            Debug.Log("Combat in test");
            //Tests

            /*
                        localSpellcaster = new Summoner();
                        localSpellcaster.chapter.spellsCollected.Add(new Skeletons()); // Pass
                        localSpellcaster.chapter.spellsCollected.Add(new Ravenssong()); // Pass
                        localSpellcaster.chapter.spellsCollected.Add(new Bearsfury());; // Pass
                 localSpellcaster = new Illusionist();
                        localSpellcaster.chapter.spellsCollected.Add(new Catharsis()); //Pass
                        localSpellcaster.chapter.spellsCollected.Add(new Catastrophe()); // Pass
                        localSpellcaster.chapter.spellsCollected.Add(new Tragedy()); //Pass
                           localSpellcaster = new Chronomancer();
                        localSpellcaster.chapter.spellsCollected.Add(new ReverseWounds()); // Pass
                        localSpellcaster.chapter.spellsCollected.Add(new Manipulate()); //Pass
                        localSpellcaster.chapter.spellsCollected.Add(new Chronoblast());  // Pass
                        localSpellcaster = new Elementalist();
                        localSpellcaster.chapter.spellsCollected.Add(new Fireball()); //Looks like ToxicPotion
                        localSpellcaster.chapter.spellsCollected.Add(new EyeOfTheStorm());  // Looks like Bears fury, chronoblast, Natural Disaster
                        localSpellcaster.chapter.spellsCollected.Add(new NaturalDisaster()); // Looks like Tragedy
                       localSpellcaster = new Arcanist();
                        localSpellcaster.chapter.spellsCollected.Add(new MarcellasBlessing()); //Looks like DistilledPotion
                        localSpellcaster.chapter.spellsCollected.Add(new RunicDarts()); // Pass
                        localSpellcaster.chapter.spellsCollected.Add(new Archive());  // Pass
                */
            localSpellcaster = new Alchemist();
            localSpellcaster.chapter.spellsCollected.Add(new DistilledPotion());  // Pass
            localSpellcaster.chapter.spellsCollected.Add(new PotionofBlessing()); // Needs more
            localSpellcaster.chapter.spellsCollected.Add(new ToxicPotion()); // Looks like Fireball, NaturalDisaster  /* */
        }

        InvokeRepeating("UpdateHealthBars", 0f, 1f);

        ImageScript.LinesUpdated += LinesUpdated;
        ImageScript.LinesCleared += LinesCleared;

        //Finding Pixel To World Unit Conversion Based On Orthographic Size Of Camera
        WorldUnitsInCamera.y = gameObject.GetComponent<Camera>().orthographicSize * 2;
        WorldUnitsInCamera.x = WorldUnitsInCamera.y * Screen.width / Screen.height;

        WorldToPixelAmount.x = Screen.width / WorldUnitsInCamera.x;
        WorldToPixelAmount.y = Screen.height / WorldUnitsInCamera.y;

        ResetButton.onClick.AddListener(ResetSwipe);
    }

    void UpdateHealthBars()
    {

        try
        {
            BossHealthBar.GetComponent<UIHealthbarController>().healthPercentage = NetworkGameState.instance.GetBossHealth();
            BossHealthBar.transform.GetChild(2).GetComponent<Text>().text = ((int)NetworkGameState.instance.GetBossCurrentHealth()).ToString() + "/" + NetworkGameState.instance.GetBossMaxHealth().ToString();
        }
        catch
        {
            //If we are here then we are just testing.
        }

        try
        {
            PlayerHealthBar.GetComponent<UIHealthbarController>().healthPercentage = localSpellcaster.fCurrentHealth / localSpellcaster.fMaxHealth;
            PlayerHealthBar.transform.GetChild(2).GetComponent<Text>().text = ((int)localSpellcaster.fCurrentHealth).ToString() + "/" + localSpellcaster.fMaxHealth.ToString();
        }
        catch
        {

        }
    }

    public void BasicAttack()
    {
        if (!hasDoneBasicAttack)
        {
            SoundManager.instance.PlaySingle(SoundManager.spellcast);
            hasDoneBasicAttack = true;
            float baseDmg = 2f;
            if(orbPercentage > .75f)
            {
                baseDmg = Random.Range(3, 4.1f);
            }else if(orbPercentage > .5f)
            {
                baseDmg = Random.Range(2, 4.1f);
            }else if(orbPercentage > .25f)
            {
                baseDmg = Random.Range(2, 3.1f);
            }

            NetworkManager.s_Singleton.DealDmgToBoss(baseDmg);

            spellProjectile.GetComponent<UIWanderingProjectile>().Launch();
            damageText.text = ((int)baseDmg).ToString() + " damage!";

            basicAttackButton.gameObject.SetActive(false);
            ResetButton.gameObject.SetActive(true);
        } 
    }

    private void LateUpdate()
    {
        if (hasDrawned && firstTime && isInBossPanel && !onlyBasicAttack)
        {
            hasDrawned = false;
            ImageGestureImage match = ImageScript.CheckForImageMatch();

            if (match != null) //  && match.Name == selectedSpell.sSpellName
            {
                Debug.Log(match.Name + " == " + selectedSpell.sSpellName);
                Debug.Log("Match Score : " + match.Score);
                firstTime = false;
                var shape = MatchParticleSystem.shape;
                MatchParticleSystem.transform.localPosition = ConvertToWorldUnits(lastX, lastY);
                MatchParticleSystem.Play();
                Debug.Log("Match Found: " + match.Name);
                SwipeInstructionText.text = "You casted " + match.Name;
                //TODO: Maybe scrap out ICombatSpell interface, and stick with just Spell (after graduation).
                ICombatSpell combatSpell = (ICombatSpell)selectedSpell;
                try
                {

                    combatSpell.CombatCast(localSpellcaster, orbPercentage);

                    // only call this if combat spell did damage
                    if(((Spell)combatSpell).damageSpell)
                    {
                        spellProjectile.GetComponent<UIWanderingProjectile>().Launch();
                        // get damage from combat spell
                        damageText.text = ((Spell)combatSpell).damageDealt + " damage!";
                    } 
                }
                catch { }
                //NetworkManager.s_Singleton.CombatSpellCast(selectedSpell.sSpellName, match.Score);
                //AudioSourceOnMatch.Play();
                swipeGuideSpawner.selectedSpell.SetActive(false);
                ResetButton.gameObject.SetActive(true);

            }
            else
            {
                //Debug.Log(match.Name + " != " + selectedSpell.sSpellName);
                Debug.Log("No match found!");
            }
        }
        // TODO: Do something with the match
        // You could get a texture from it:
        // Texture2D texture = FingersImageAutomationScript.CreateTextureFromImageGestureImage(match);
        //}

        try
        {
            if (NetworkGameState.instance.IfBossAttacked())
            {
                DialogueField.SetActive(true);
                DialogueField.transform.GetChild(0).GetComponent<Text>().text = "The Black Mage dealt " + ((int)NetworkGameState.instance.GetBossAttackDamage()).ToString() + " damage to everyone!";
            }
        }
        catch
        {
            //If we are here that means we are testing.
        }
    }
    public Vector3 ConvertToWorldUnits(float x, float y)
    {
        Vector3 result = new Vector3(); ;
        result.x = ((x / WorldToPixelAmount.x) - (WorldUnitsInCamera.x / 2)) +
        GetComponent<Camera>().transform.position.x;
        result.y = ((y / WorldToPixelAmount.y) - (WorldUnitsInCamera.y / 2)) +
        GetComponent<Camera>().transform.position.y;
        return result;
    }

    private void ResetSwipe()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        DialogueField.SetActive(false);
        SceneManager.LoadScene("CombatSceneV2");
        Debug.Log("Reset Swipe");
        firstTime = true;
        hasDrawned = false;
        ImageScript.Reset();
    }
}
