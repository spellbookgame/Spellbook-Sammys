using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/* namespace / HasChanged() written by Kiwasi Games
 * this script creates a builder that builds strings of item 
 * names as they are dropped into slots 
 */
public class SpellManager : MonoBehaviour, IHasChanged
{
    [SerializeField] Transform slots;
    [SerializeField] public Text inventoryText;
    [SerializeField] public GameObject panel;
    
    public Dictionary<string, int> slotPieces;
    private RectTransform panelRect;
    private float fWidth;
    private int iSlotCount;

    Player localPlayer;
    
    void Start()
    {
        fWidth = 610f;
        iSlotCount = 0;

        slotPieces = new Dictionary<string, int>();

        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();
        panelRect = panel.GetComponent<RectTransform>();

        HasChanged();

        GenerateSpellSlots();
    }

    void Update()
    {
        // checking to see if each slot is filled
        int i = 0;
        foreach(Transform slotTransform in slots)
        {
            GameObject item = slotTransform.GetComponent<SlotHandler>().item;
            if(item)
            {
                ++i;
            }
        }
        // if all slots are filled, call the CompareSpells() function
        if(i >= 4)
        {
            localPlayer.Spellcaster.chapter.CompareSpells(localPlayer.Spellcaster, slotPieces);
        }
    }

    // iterates through each slot and deletes child
    public void RemovePrefabs(Spell spell)
    {
        // remove slot children
        foreach(Transform slotTransform in slots)
        {
            Destroy(slotTransform.GetChild(0).gameObject);
        }
        slotPieces.Clear();
    }

    public void HasChanged()
    {
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        builder.Append(" - ");
        foreach(Transform slotTransform in slots)
        {
            // will return game object if there is one, or null if there isnt
            GameObject item = slotTransform.GetComponent<SlotHandler>().item;
            // if there is an item returned
            if(item)
            {
                // add item name to builder
                builder.Append(item.name);
                builder.Append(" - ");
            }
        }
        inventoryText.text = builder.ToString();
    }

    // if the scene is changed while spell pieces are still in slots, return them to player's inventory
    void OnDestroy()
    {
        foreach (Transform slotTransform in slots)
        {
            if (slotTransform.childCount > 0)
            {
                localPlayer.Spellcaster.spellPieces[slotTransform.GetChild(0).name] += 1;
            }
        }
    }

    private void GenerateSpellSlots()
    {
        if (localPlayer.Spellcaster.spellPieces["Alchemy A Spell Piece"] > 0)
        {
            GameObject alcASlot = Instantiate((GameObject)Resources.Load("Spell Slots/Alchemy A Spell Slot"), panel.transform);
            ++iSlotCount;
            alcASlot.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces["Alchemy A Spell Piece"].ToString();
            alcASlot.transform.GetChild(0).gameObject.AddComponent<DragHandler>();
        }
        if (localPlayer.Spellcaster.spellPieces["Alchemy B Spell Piece"] > 0)
        {
            GameObject alcBSlot = Instantiate((GameObject)Resources.Load("Spell Slots/Alchemy B Spell Slot"), panel.transform);
            ++iSlotCount;
            alcBSlot.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces["Alchemy B Spell Piece"].ToString();
            alcBSlot.transform.GetChild(0).gameObject.AddComponent<DragHandler>();
        }
        if (localPlayer.Spellcaster.spellPieces["Alchemy C Spell Piece"] > 0)
        {
            GameObject alcCSlot = Instantiate((GameObject)Resources.Load("Spell Slots/Alchemy C Spell Slot"), panel.transform);
            ++iSlotCount;
            alcCSlot.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces["Alchemy B Spell Piece"].ToString();
            alcCSlot.transform.GetChild(0).gameObject.AddComponent<DragHandler>();
        }
        if (localPlayer.Spellcaster.spellPieces["Alchemy D Spell Piece"] > 0)
        {
            GameObject alcDSlot = Instantiate((GameObject)Resources.Load("Spell Slots/Alchemy D Spell Slot"), panel.transform);
            ++iSlotCount;
            alcDSlot.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces["Alchemy D Spell Piece"].ToString();
            alcDSlot.transform.GetChild(0).gameObject.AddComponent<DragHandler>();
        }
        if (localPlayer.Spellcaster.spellPieces["Arcane A Spell Piece"] > 0)
        {
            GameObject arcASlot = Instantiate((GameObject)Resources.Load("Spell Slots/Arcane A Spell Slot"), panel.transform);
            ++iSlotCount;
            arcASlot.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces["Arcane A Spell Piece"].ToString();
            arcASlot.transform.GetChild(0).gameObject.AddComponent<DragHandler>();
        }
        if (localPlayer.Spellcaster.spellPieces["Arcane B Spell Piece"] > 0)
        {
            GameObject arcBSlot = Instantiate((GameObject)Resources.Load("Spell Slots/Arcane B Spell Slot"), panel.transform);
            ++iSlotCount;
            arcBSlot.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces["Arcane B Spell Piece"].ToString();
            arcBSlot.transform.GetChild(0).gameObject.AddComponent<DragHandler>();
        }
        if (localPlayer.Spellcaster.spellPieces["Arcane C Spell Piece"] > 0)
        {
            GameObject arcCSlot = Instantiate((GameObject)Resources.Load("Spell Slots/Arcane C Spell Slot"), panel.transform);
            ++iSlotCount;
            arcCSlot.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces["Arcane C Spell Piece"].ToString();
            arcCSlot.transform.GetChild(0).gameObject.AddComponent<DragHandler>();
        }
        if (localPlayer.Spellcaster.spellPieces["Arcane D Spell Piece"] > 0)
        {
            GameObject arcDSlot = Instantiate((GameObject)Resources.Load("Spell Slots/Arcane D Spell Slot"), panel.transform);
            ++iSlotCount;
            arcDSlot.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces["Arcane D Spell Piece"].ToString();
            arcDSlot.transform.GetChild(0).gameObject.AddComponent<DragHandler>();
        }
        if (localPlayer.Spellcaster.spellPieces["Elemental A Spell Piece"] > 0)
        {
            GameObject eleASlot = Instantiate((GameObject)Resources.Load("Spell Slots/Elemental A Spell Slot"), panel.transform);
            ++iSlotCount;
            eleASlot.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces["Elemental A Spell Piece"].ToString();
            eleASlot.transform.GetChild(0).gameObject.AddComponent<DragHandler>();
        }
        if (localPlayer.Spellcaster.spellPieces["Elemental B Spell Piece"] > 0)
        {
            GameObject eleBSlot = Instantiate((GameObject)Resources.Load("Spell Slots/Elemental B Spell Slot"), panel.transform);
            ++iSlotCount;
            eleBSlot.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces["Elemental B Spell Piece"].ToString();
            eleBSlot.transform.GetChild(0).gameObject.AddComponent<DragHandler>();
        }
        if (localPlayer.Spellcaster.spellPieces["Elemental C Spell Piece"] > 0)
        {
            GameObject eleCSlot = Instantiate((GameObject)Resources.Load("Spell Slots/Elemental C Spell Slot"), panel.transform);
            ++iSlotCount;
            eleCSlot.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces["Elemental C Spell Piece"].ToString();
            eleCSlot.transform.GetChild(0).gameObject.AddComponent<DragHandler>();
        }
        if (localPlayer.Spellcaster.spellPieces["Elemental D Spell Piece"] > 0)
        {
            GameObject eleDSlot = Instantiate((GameObject)Resources.Load("Spell Slots/Elemental D Spell Slot"), panel.transform);
            ++iSlotCount;
            eleDSlot.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces["Elemental D Spell Piece"].ToString();
            eleDSlot.transform.GetChild(0).gameObject.AddComponent<DragHandler>();
        }
        if (localPlayer.Spellcaster.spellPieces["Illusion A Spell Piece"] > 0)
        {
            GameObject illASlot = Instantiate((GameObject)Resources.Load("Spell Slots/Illusion A Spell Slot"), panel.transform);
            ++iSlotCount;
            illASlot.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces["Illusion A Spell Piece"].ToString();
            illASlot.transform.GetChild(0).gameObject.AddComponent<DragHandler>();
        }
        if (localPlayer.Spellcaster.spellPieces["Illusion B Spell Piece"] > 0)
        {
            GameObject illBSlot = Instantiate((GameObject)Resources.Load("Spell Slots/Illusion B Spell Slot"), panel.transform);
            ++iSlotCount;
            illBSlot.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces["Illusion B Spell Piece"].ToString();
            illBSlot.transform.GetChild(0).gameObject.AddComponent<DragHandler>();
        }
        if (localPlayer.Spellcaster.spellPieces["Illusion C Spell Piece"] > 0)
        {
            GameObject illCSlot = Instantiate((GameObject)Resources.Load("Spell Slots/Illusion C Spell Slot"), panel.transform);
            ++iSlotCount;
            illCSlot.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces["Illusion C Spell Piece"].ToString();
            illCSlot.transform.GetChild(0).gameObject.AddComponent<DragHandler>();
        }
        if (localPlayer.Spellcaster.spellPieces["Illusion D Spell Piece"] > 0)
        {
            GameObject illDSlot = Instantiate((GameObject)Resources.Load("Spell Slots/Illusion D Spell Slot"), panel.transform);
            ++iSlotCount;
            illDSlot.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces["Illusion D Spell Piece"].ToString();
            illDSlot.transform.GetChild(0).gameObject.AddComponent<DragHandler>();
        }
        if (localPlayer.Spellcaster.spellPieces["Summoning A Spell Piece"] > 0)
        {
            GameObject sumASlot = Instantiate((GameObject)Resources.Load("Spell Slots/Summoning A Spell Slot"), panel.transform);
            ++iSlotCount;
            sumASlot.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces["Summoning A Spell Piece"].ToString();
            sumASlot.transform.GetChild(0).gameObject.AddComponent<DragHandler>();
        }
        if (localPlayer.Spellcaster.spellPieces["Summoning B Spell Piece"] > 0)
        {
            GameObject sumBSlot = Instantiate((GameObject)Resources.Load("Spell Slots/Summoning B Spell Slot"), panel.transform);
            ++iSlotCount;
            sumBSlot.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces["Summoning B Spell Piece"].ToString();
            sumBSlot.transform.GetChild(0).gameObject.AddComponent<DragHandler>();
        }
        if (localPlayer.Spellcaster.spellPieces["Summoning C Spell Piece"] > 0)
        {
            GameObject sumCSlot = Instantiate((GameObject)Resources.Load("Spell Slots/Summoning C Spell Slot"), panel.transform);
            ++iSlotCount;
            sumCSlot.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces["Summoning C Spell Piece"].ToString();
            sumCSlot.transform.GetChild(0).gameObject.AddComponent<DragHandler>();
        }
        if (localPlayer.Spellcaster.spellPieces["Summoning D Spell Piece"] > 0)
        {
            GameObject sumDSlot = Instantiate((GameObject)Resources.Load("Spell Slots/Summoning D Spell Slot"), panel.transform);
            ++iSlotCount;
            sumDSlot.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces["Summoning D Spell Piece"].ToString();
            sumDSlot.transform.GetChild(0).gameObject.AddComponent<DragHandler>();
        }
        if (localPlayer.Spellcaster.spellPieces["Time A Spell Piece"] > 0)
        {
            GameObject timASlot = Instantiate((GameObject)Resources.Load("Spell Slots/Time A Spell Slot"), panel.transform);
            ++iSlotCount;
            timASlot.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces["Time A Spell Piece"].ToString();
            timASlot.transform.GetChild(0).gameObject.AddComponent<DragHandler>();
        }
        if (localPlayer.Spellcaster.spellPieces["Time B Spell Piece"] > 0)
        {
            GameObject timBSlot = Instantiate((GameObject)Resources.Load("Spell Slots/Time B Spell Slot"), panel.transform);
            ++iSlotCount;
            timBSlot.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces["Time B Spell Piece"].ToString();
            timBSlot.transform.GetChild(0).gameObject.AddComponent<DragHandler>();
        }
        if (localPlayer.Spellcaster.spellPieces["Time C Spell Piece"] > 0)
        {
            GameObject timCSlot = Instantiate((GameObject)Resources.Load("Spell Slots/Time C Spell Slot"), panel.transform);
            ++iSlotCount;
            timCSlot.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces["Time C Spell Piece"].ToString();
            timCSlot.transform.GetChild(0).gameObject.AddComponent<DragHandler>();
        }
        if (localPlayer.Spellcaster.spellPieces["Time D Spell Piece"] > 0)
        {
            GameObject timDSlot = Instantiate((GameObject)Resources.Load("Spell Slots/Time D Spell Slot"), panel.transform);
            ++iSlotCount;
            timDSlot.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = localPlayer.Spellcaster.spellPieces["Time D Spell Piece"].ToString();
            timDSlot.transform.GetChild(0).gameObject.AddComponent<DragHandler>();
        }
    }
}

namespace UnityEngine.EventSystems
{
    public interface IHasChanged: IEventSystemHandler
    {
        void HasChanged();
    }
}
