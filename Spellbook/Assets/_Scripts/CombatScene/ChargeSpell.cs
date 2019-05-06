using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeSpell : MonoBehaviour
{
    public Image OuterBackgroundBar;
    public Button ChargeButton;
    public Image ChargeButtonBar;
    public ParticleSystem MatchParticleSystem;

    public Text CountdownText;
    private int count = 10;
    private int stopTime = 0;
    public GameObject BossPanekGameObject;

    // Start is called before the first frame update
    void Start()
    {
        ChargeButton.onClick.AddListener(OnTap);
        MatchParticleSystem.transform.position = Vector3.zero;
        InvokeRepeating("Countdown", 0f, 1f);
    }

    void OnTap()
    {
        OuterBackgroundBar.fillAmount += 0.02f;
        ChargeButtonBar.fillAmount += 0.02f;
        MatchParticleSystem.Play();
    }

    void Countdown()
    {
        if(count <= stopTime)
        {
            CancelInvoke();
            BossPanekGameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
        CountdownText.text = "" + count--;
    }
}
