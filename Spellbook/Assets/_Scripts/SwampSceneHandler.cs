using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwampSceneHandler : MonoBehaviour
{
    [SerializeField] private Button buffLandButton;
    [SerializeField] private Button leaveButton;
    [SerializeField] private Text dialogueText;

    private bool requested;
    private bool requestCompleted;

    private Player localPlayer;

    private void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        buffLandButton.onClick.AddListener(BuffLand);
        leaveButton.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);
            SceneManager.LoadScene("MainPlayerScene");
        });
    }

    private void BuffLand()
    {
        SoundManager.instance.PlaySingle(SoundManager.buttonconfirm);

        if (!requested)
        {
            dialogueText.text = "I require an item...";
            buffLandButton.transform.GetChild(0).GetComponent<Text>().text = "Give an item!";
            requested = true;
        }
        else if(requested)
        {
            if(localPlayer.Spellcaster.inventory.Count <= 0)
            {
                dialogueText.text = "Fool! You have no items to give. Leave my swamp!";
            }
            else
            {
                if(!requestCompleted)
                {
                    int randIndex = Random.Range(0, localPlayer.Spellcaster.inventory.Count - 1);
                    string itemName = localPlayer.Spellcaster.inventory[randIndex].name;
                    localPlayer.Spellcaster.RemoveFromInventory(localPlayer.Spellcaster.inventory[randIndex]);
                    dialogueText.text = "I'll take your " + itemName + ". You will receive double mana from the land next round.";
                    requestCompleted = true;
                }
                else
                {
                    dialogueText.text = "I only accept one request per visit.";
                }
            }
        }
    }
}
