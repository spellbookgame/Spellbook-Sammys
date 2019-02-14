using UnityEngine;
using UnityEngine.UI;

public class MainPageHandler : MonoBehaviour
{
    [SerializeField] private Text manaCrystalsValue;
    [SerializeField] private Text activeSpellsValue;

    Player localPlayer;

    private void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        manaCrystalsValue.text = localPlayer.Spellcaster.iMana.ToString();

        foreach(string entry in localPlayer.Spellcaster.activeSpells)
        {
            activeSpellsValue.text = activeSpellsValue.text + entry + "\n";
        }
    }
}
