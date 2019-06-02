using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MineSceneHandler : MonoBehaviour
{
    [SerializeField] private Button grabManaButton;
    [SerializeField] private Button leaveButton;

    private Player localPlayer;
    private void Start()
    {
        SoundManager.instance.PlayGameBCM(SoundManager.minesBGM);
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        grabManaButton.onClick.AddListener(GrabMana);

        leaveButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SoundManager.instance.PlayGameBCM(SoundManager.gameBCG);
            SceneManager.LoadScene("MainPlayerScene");
        });

        QuestTracker.instance.TrackLocationQuest("location_mines");
        QuestTracker.instance.TrackErrandQuest("location_mines");
    }

    private void GrabMana()
    {
        SoundManager.instance.PlaySingle(SoundManager.manaCollect);
        int manaCollected = Random.Range(600, 2500);
        localPlayer.Spellcaster.CollectMana(manaCollected);
        PanelHolder.instance.displayNotify("You found mana!", "You grabbed some crystals and collected " + manaCollected + " mana!", "MainPlayerScene");
    }
}
