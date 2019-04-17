using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Controller for the MagicDice prefab.
/// 
/// Written by Malcolm Riley
/// CMPS 17X, Winter 2019
/// </summary>
public class DiceRoll : MonoBehaviour
{

    // Public fields
    public Image dicePips;
    public Image diceCube;
    public Sprite pipsNine;
    public Sprite pipsEight;
    public Sprite pipsSeven;
    public Sprite pipsSix;
    public Sprite pipsFive;
    public Sprite pipsFour;
    public Sprite pipsThree;
    public Sprite pipsTwo;
    public Sprite pipsOne;

    [Range(0, 9)]
    public int maxRoll = 6;

    public int LastRoll
    {
        get { return _roll; }
        private set { SetRoll(value); }
    }

    // private fields
    private int _roll;
    private Sprite[] _pipsArray;
    private int _rollAdd;
    private float _rollMult;

    public int _rollMinimum;
    public int _rollMaximum;

    // Grace Ko's additions: implementing spell/quest tracking and scanner
    private Button rollButton;
    private GameObject diceTrayPanel;
    public bool rollEnabled;

    private int diceRoll;
    private int pressedNum;
    Player localPlayer;

    void Start()
    {
        _pipsArray = new Sprite[] { pipsOne, pipsTwo, pipsThree, pipsFour, pipsFive, pipsSix, pipsSeven, pipsEight, pipsNine };
        SetDefaults();
        // Roll();

        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        rollButton = GameObject.Find("button_roll").GetComponent<Button>();
        diceTrayPanel = GameObject.Find("Dice Tray").gameObject;

        pressedNum = 0; // to set button text to either roll or scan

        rollButton.onClick.AddListener(() => Roll());
    }

    public void Roll()
    {
        // only execute if roll is enabled
        if(rollEnabled)
        {
            GameObject.Find("Dice Tray").GetComponent<DiceUIHandler>().hasRolled = true;

            // when button is clicked for first time, roll and change button to Scan
            if (pressedNum == 0)
            {
                SoundManager.instance.PlaySingle(SoundManager.diceroll);

                // wiggle the dice
                gameObject.GetComponent<WiggleElement>().Wiggle();

                LastRoll = Clamp((int)(_rollMult * Random.Range(_rollMinimum, _rollMaximum + 1) + _rollAdd), _rollMinimum, _rollMaximum);
                SetDefaults();

                // if Potion of Luck was cast, remove it after rolling dice
                SpellTracker.instance.EndPotionofLuck();
                // if Tailwind was cast, remove it after rolling dice
                SpellTracker.instance.EndTailwind();

                CheckMoveRoll(LastRoll);
                CheckManaRoll(LastRoll);

                //QuestTracker.instance.CheckMoveQuest(diceRoll);

                // disable drag on ALL dice once they're rolled
                gameObject.GetComponent<DiceDragHandler>().enabled = false;
                foreach(Transform t in GameObject.Find("Scroll Content").transform)
                {
                    if(t.childCount > 0)
                    {
                        t.GetChild(0).GetComponent<DiceDragHandler>().enabled = false;
                    }
                }

                rollButton.GetComponentInChildren<Text>().text = "Scan!";
                ++pressedNum;
            }
            else if (pressedNum >= 1)
            {
                diceTrayPanel.SetActive(false);
                SceneManager.LoadScene("VuforiaScene");
            }
        }
    }

    // store the number of spaces player has traveled
    private void CheckMoveRoll(int rollValue)
    {
        if(transform.parent.name.Equals("slot1"))
        {
            localPlayer.Spellcaster.spacesTraveled += rollValue;
        }
    }

    // add a percentage to mana multiplier for earning mana at the end of turn
    private void CheckManaRoll(int rollValue)
    {
        if(transform.parent.name.Equals("slot2"))
        {
            decimal m = (decimal)0.05 * rollValue;
            Debug.Log("Roll: " + rollValue + "\n" + "Multiplier: " + m);
            localPlayer.Spellcaster.dManaMultiplier += m;
        }
    }

    private void CheckHealRoll()
    {
        // do something
    }

    // Internal Methods
    private void SetRoll(int value)
    {
        _roll = value;
        dicePips.sprite = (value <= 0) ? null : _pipsArray[Clamp(_roll - 1, 0, _pipsArray.Length - 1)];
    }

    private void SetDefaults()
    {
        _rollMinimum = 1;
        _rollMaximum = maxRoll;
        _rollAdd = 0;
        _rollMult = 1.0F;
    }

    // Why doesn't System.Math have an int clamp function, like seriously
    private static int Clamp(int value, int min, int max)
    {
        if (value > max) return max;
        if (value < min) return min;
        return value;
    }
}
