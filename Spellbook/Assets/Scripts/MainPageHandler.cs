using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainPageHandler : MonoBehaviour
{
    [SerializeField] private Text manaCrystalsValue;
    [SerializeField] private Text activeSpellsValue;
    [SerializeField] private Enemy enemy;

    Player localPlayer;

    private void Start()
    {
        StartCoroutine(waitTime());
    }

    IEnumerator waitTime()
    {
        yield return new WaitForSeconds(2f);

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
