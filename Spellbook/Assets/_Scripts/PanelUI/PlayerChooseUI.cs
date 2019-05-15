using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

// used to display player choose panel
// currently has no function
public class PlayerChooseUI : MonoBehaviour
{
    [SerializeField] private Button bAlchemist;
    [SerializeField] private Button bArcanist;
    [SerializeField] private Button bChronomancer;
    [SerializeField] private Button bElementalist;
    [SerializeField] private Button bSummoner;
    [SerializeField] private Button bIllusionist;
    private Button[] buttons;

    public bool panelActive = false;
    public string panelID = "playerchoose";

    SpellCaster player;

    private void Start()
    {
        //Ordered by ID num
        buttons = new Button[] { bAlchemist, bArcanist, bElementalist, bChronomancer, bIllusionist, bSummoner };
        Bolt.NetworkArray_Integer activeSpellcasters = NetworkGameState.instance.GetSpellcasterList();

        player = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>().Spellcaster;

        //Spawn only buttons corresponding to the spellcasters in this game.
        float yPos = 2975;
        float dy = 1360;
        for (int i = 0; i < activeSpellcasters.Length; i++)
        {
            if (activeSpellcasters[i] == 1)
            {
                Vector3 pos = buttons[i].gameObject.transform.localPosition;
                pos.y = yPos;
                buttons[i].gameObject.transform.localPosition = pos;
                buttons[i].gameObject.SetActive(true);
                yPos -= dy;
            }
        }
    }

    public void OnAlchemistClicked()
    {
        PanelHolder.instance.ChooseAlly(0, player);
        DisablePanel();
        PanelHolder.panelQueue.Dequeue();
        PanelHolder.instance.CheckPanelQueue();
    }
    public void OnArcanistClicked()
    {
        PanelHolder.instance.ChooseAlly(1, player);
        DisablePanel();
        PanelHolder.panelQueue.Dequeue();
        PanelHolder.instance.CheckPanelQueue();
    }
    public void OnElementalistClicked()
    {
        PanelHolder.instance.ChooseAlly(2, player);
        DisablePanel();
        PanelHolder.panelQueue.Dequeue();
        PanelHolder.instance.CheckPanelQueue();
    }
    public void OnChronomancerClicked()
    {
        PanelHolder.instance.ChooseAlly(3, player);
        DisablePanel();
        PanelHolder.panelQueue.Dequeue();
        PanelHolder.instance.CheckPanelQueue();
    }
    public void OnIllusionistClicked()
    {
        PanelHolder.instance.ChooseAlly(4, player);
        DisablePanel();
        PanelHolder.panelQueue.Dequeue();
        PanelHolder.instance.CheckPanelQueue();
    }
    public void OnSummonerClicked()
    {
        PanelHolder.instance.ChooseAlly(5, player);
        DisablePanel();
        PanelHolder.panelQueue.Dequeue();
        PanelHolder.instance.CheckPanelQueue();
    }

    private void DisablePanel()
    {
        gameObject.SetActive(false);
    }
    public void EnablePanel()
    {
        gameObject.SetActive(true);
    }

    // used to notify players of various events. input a buttonClick string to change the onClick listener
    public void DisplayPlayerChoose(/*string spellName*/)
    {
        // if current scene is Vuforia, change everything to image
        if (SceneManager.GetActiveScene().name.Equals("VuforiaScene"))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Image>().enabled = true;
        }

        gameObject.SetActive(true);

        if (!PanelHolder.panelQueue.Peek().Equals(panelID))
        {
            DisablePanel();
        }
    }

    private void OkClick()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        gameObject.SetActive(false);

        PanelHolder.panelQueue.Dequeue();
        PanelHolder.instance.CheckPanelQueue();
    }
}