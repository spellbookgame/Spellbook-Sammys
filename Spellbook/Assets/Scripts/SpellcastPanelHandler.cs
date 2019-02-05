using UnityEngine;
using UnityEngine.UI;


/* to put this in the game:
 * drag the Spell panel prefab into the scene
 * create a button
 * attach the spell panel prefab to the button
 * call openPanel from button onclick listener
 */
public class SpellcastPanelHandler : MonoBehaviour
{
    [SerializeField] Button spellButton;
    private bool panelOpen;

    Player localPlayer;

    private void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        panelOpen = false;

        int yPos = 1300;
        // add buttons for each spell the player has collected
        for (int i = 0; i < localPlayer.Spellcaster.chapter.spellsCollected.Count; i++)
        {
            Button newSpellButton = Instantiate(spellButton);
            newSpellButton.transform.parent = gameObject.transform;
            newSpellButton.GetComponentInChildren<Text>().text = localPlayer.Spellcaster.chapter.spellsCollected[i].sSpellName;
            newSpellButton.transform.position = new Vector3(gameObject.transform.position.x, yPos, 0);

            // new int to pass into button onClick listener so loop will not throw index out of bounds error
            int i2 = i;
            // add listener to button
            newSpellButton.onClick.AddListener(() => localPlayer.Spellcaster.chapter.spellsCollected[i2].SpellCast(localPlayer.Spellcaster));

            // to position new button underneath prev button
            yPos -= 200;
        }
    }

    public void openPanel()
    {
        if (!panelOpen)
        {
            panelOpen = true;
            gameObject.SetActive(true);
        }
        else
        {
            panelOpen = false;
            gameObject.SetActive(false);
        }    
    }
}
