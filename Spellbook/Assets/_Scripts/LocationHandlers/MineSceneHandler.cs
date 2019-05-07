using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MineSceneHandler : MonoBehaviour
{
    [SerializeField] private Button grabManaButton;
    [SerializeField] private Button leaveButton;

    private bool collectedMana;

    private Player localPlayer;
    private void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        collectedMana = false;
        grabManaButton.onClick.AddListener(GrabMana);

        leaveButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("MainPlayerScene");
        });

        QuestTracker.instance.TrackLocationQuest("location_mines");
        QuestTracker.instance.TrackErrandQuest("location_mines");
    }

    private void GrabMana()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);

        if (!collectedMana)
        {
            int manaCollected = Random.Range(600, 2500);
            localPlayer.Spellcaster.CollectMana(manaCollected);
            PanelHolder.instance.displayNotify("You found mana!", "You grabbed some crystals and collected " + manaCollected + " mana!", "OK");
            collectedMana = true;
        }
        else
        {
            PanelHolder.instance.displayNotify("Don't be greedy!", "You can only take once per visit. Now leave!", "MainPlayerScene");
        }
    }
}
