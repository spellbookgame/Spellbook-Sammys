using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpellCollectionHandler : MonoBehaviour
{
    [SerializeField] private Button mainButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button spellButton;
    [SerializeField] private GameObject glyphContainer;
    [SerializeField] private GameObject spellInfoPanel;
    [SerializeField] private GameObject glyphPanel;

    Player localPlayer;

    // Start is called before the first frame update
    void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        // adding onClick listener for UI buttons
        mainButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("MainPlayerScene");
        });
        backButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("SpellbookScene");
        });

        int yPos = 780;
        // add buttons for each spell the player can collect
        for (int i = 0; i < localPlayer.Spellcaster.chapter.spellsAllowed.Count; i++)
        {
            Button newSpellButton = Instantiate(spellButton);
            newSpellButton.transform.parent = GameObject.Find("Canvas").transform;
            newSpellButton.GetComponentInChildren<Text>().text = localPlayer.Spellcaster.chapter.spellsAllowed[i].sSpellName;
            newSpellButton.transform.localPosition = new Vector3(0, yPos, 0);

            // new int to pass into button onClick listener so loop will not throw index out of bounds error
            int i2 = i;
            // add listener to button
            newSpellButton.onClick.AddListener(() => showSpellInfo(localPlayer.Spellcaster.chapter.spellsAllowed[i2]));

            // to position new button underneath prev button
            yPos -= 225;
        }
    }

    private void showSpellInfo(Spell spell)
    {
        // if the panel still has glyphs in it, return them to the glyph container
        while(glyphPanel.transform.childCount > 0)
        {
            Transform child = glyphPanel.transform.GetChild(0);
            child.SetParent(glyphContainer.transform);
            child.localPosition = Vector3.zero;
        }

        spellInfoPanel.transform.GetChild(0).GetComponent<Text>().text = spell.sSpellName;
        spellInfoPanel.transform.GetChild(1).GetComponent<Text>().text = "Tier: " + spell.iTier.ToString();
        spellInfoPanel.transform.GetChild(2).GetComponent<Text>().text = spell.sSpellInfo;
        
        // add glyph images to the panel to show player required glyphs
        foreach (KeyValuePair<string, int> kvp in spell.requiredGlyphs)
        {
            string glyphName = kvp.Key;
            GameObject glyphImage = glyphContainer.transform.Find(glyphName).gameObject;
            glyphImage.transform.SetParent(glyphPanel.transform);

            // removing text child from image
            if (glyphImage.transform.childCount > 0)
                Destroy(glyphImage.transform.GetChild(0).gameObject);
        }
    }
}
