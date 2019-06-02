using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

// used to display spell cast notification
public class SpellCastUI : MonoBehaviour
{
    [SerializeField] private Text titleText;
    [SerializeField] private Text infoText;
    [SerializeField] private Button singleButton;

    public bool panelActive = false;
    public string panelID = "spellCast";

    private void DisablePanel()
    {
        gameObject.SetActive(false);
    }
    public void EnablePanel()
    {
        gameObject.SetActive(true);
    }

    // used to notify players of when a spell is cast on them
    public void DisplayNotify(string spellName, string info, string buttonClick)
    {
        gameObject.GetComponent<UIDissolveImage>().Dissolve();

        titleText.text = spellName;
        infoText.text = spellName + " was cast on you!\n\n" + info;

        singleButton.onClick.RemoveAllListeners();

        if (buttonClick.Equals("OK"))
            singleButton.onClick.AddListener((OkClick));
        else
            singleButton.onClick.AddListener(() => SceneClick(buttonClick));

        gameObject.SetActive(true);

        // if next panel in queue is NOT a notify panel, disable this panel
        if (!PanelHolder.panelQueue.Peek().Equals(panelID))
        {
            DisablePanel();
        }
    }

    private void OkClick()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        gameObject.SetActive(false);

        if (PanelHolder.panelQueue.Count > 0)
            PanelHolder.panelQueue.Dequeue();
        PanelHolder.instance.CheckPanelQueue();
    }
    private void SceneClick(string scene)
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
        gameObject.SetActive(false);
        SceneManager.LoadScene(scene);
        UICanvasHandler.instance.ActivateSpellbookButtons(false);

        if (PanelHolder.panelQueue.Count > 0)
            PanelHolder.panelQueue.Dequeue();
        PanelHolder.instance.CheckPanelQueue();
    }
}