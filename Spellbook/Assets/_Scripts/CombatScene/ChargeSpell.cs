using Bolt.Samples.Photon.Lobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeSpell : MonoBehaviour
{
    SpellCaster localSpellcaster;
    public Image OuterBackgroundBar;
    public GameObject OrbButton;
    public GameObject Arrows;
//    public Button ChargeButton;
    public GameObject CastSpellButton;
    public Image ChargeButtonBar;
    public ParticleSystem MatchParticleSystem;
    public GameObject BossPanekGameObject;
    public Text CountdownText;

    private int totalSecs = 1;
    private int stopTime = 0;
    private Spell CombatSpell;

    private float timeSinceLastMove;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    public int taps = 0;

    // Start is called before the first frame update
    void Start()
    {
        float vertExtent = Camera.main.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        minX = -horzExtent / 2;
        maxX = horzExtent / 2;
        minY = -vertExtent / 2;
        maxY = vertExtent / 2;


        CastSpellButton.GetComponent<Button>().onClick.AddListener(OnClickCastSpell);
        OrbButton.GetComponent<Button>().onClick.AddListener(OnFirstTap);
        try
        {
            totalSecs = (int) NetworkGameState.instance.GetTapSecondsAllowed();
        }
        catch
        {

        }
    }

    void OnFirstTap()
    {
        Arrows.SetActive(false);
        CountdownText.gameObject.SetActive(true);
        OrbButton.GetComponent<Button>().onClick.RemoveAllListeners();
        OrbButton.GetComponent<Button>().onClick.AddListener(OnTap);
        InvokeRepeating("Countdown", 0f, 1f);
        InvokeRepeating("MoveButtonTransform", 1.2f, 0.3f);

    }

    void OnTap()
    {
        taps++;
        //OuterBackgroundBar.fillAmount += 0.02f;
        ChargeButtonBar.fillAmount += 0.02f;
    }

    void Countdown()
    {
        if(totalSecs <= stopTime)
        {
            CancelInvoke();
            OrbButton.SetActive(false);
            CastSpellButton.SetActive(true);
        }
        CountdownText.text = "" + totalSecs--;
    }

    public void SetCombatSpell(Spell selectedSpell, GameObject SpellButton, SpellCaster spellCaster)
    {
        localSpellcaster = spellCaster;
        //OrbButton.GetComponent<Image>().sprite = SpellButton.GetComponent<Image>().sprite;
        CombatSpell = selectedSpell;
        //ChargeButton = SpellButton.GetComponent<Button>();
    }

    public void MoveButtonTransform()
    {
        if ((int)Random.Range(0, 2) == 1 && Time.time - timeSinceLastMove >= .7f)
        {
            Vector3 v3 = OrbButton.transform.position;
            float newX = Random.Range(minX, maxX);
            float newY = Random.Range(minY, maxY);
            v3.x = Mathf.Clamp(newX , minX, maxX);
            v3.y = Mathf.Clamp(newY , minY, maxY);
            OrbButton.transform.position = v3;
            timeSinceLastMove = Time.time;
        }
    } 

    private void OnClickCastSpell()
    {
        //Send number of taps to network

        try
        {
            NetworkManager.s_Singleton.SendOrbUpdateToNetwork(localSpellcaster.spellcasterID, taps, ChargeButtonBar.fillAmount);
        }
        catch
        {

        }
        BossPanekGameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
