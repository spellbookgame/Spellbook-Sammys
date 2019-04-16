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
    private int _rollMinimum;
    private int _rollMaximum;
    private int _rollAdd;
    private float _rollMult;
    
    // Grace Ko's additions: implementing spell/quest tracking and scanner
    [SerializeField] private Button rollButton;
    [SerializeField] private GameObject diceTrayPanel;

    private int diceRoll;
    private int pressedNum;
    Player localPlayer;

    void Start()
    {
        _pipsArray = new Sprite[] { pipsOne, pipsTwo, pipsThree, pipsFour, pipsFive, pipsSix, pipsSeven, pipsEight, pipsNine };
        SetDefaults();
        // Roll();

        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        pressedNum = 0; // to set button text to either roll or scan
        rollButton.onClick.AddListener(() => Roll());
    }

    public void Roll()
    {
        // when button is clicked for first time, roll and change button to Scan
        if (pressedNum == 0)
        {
            SoundManager.instance.PlaySingle(SoundManager.diceroll);

            string spellActive = SpellTracker.instance.CheckMoveSpell();
            if (spellActive.Equals("Accelerate"))
            {
                _rollMinimum = 5;
                _rollMaximum = 9;
            }

            LastRoll = Clamp((int)(_rollMult * Random.Range(_rollMinimum, _rollMaximum + 1) + _rollAdd), _rollMinimum, _rollMaximum);
            SetDefaults();

            localPlayer.Spellcaster.spacesTraveled += LastRoll;
            QuestTracker.instance.CheckMoveQuest(diceRoll);

            rollButton.GetComponentInChildren<Text>().text = "Scan!";
            ++pressedNum;
        }
        else if (pressedNum >= 1)
        {
            diceTrayPanel.SetActive(false);
            SceneManager.LoadScene("VuforiaScene");
        }
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
