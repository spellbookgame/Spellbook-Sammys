﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SPClickHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject imageClone;

    [SerializeField] Text numText;

    Player localPlayer;

    void Start()
    {
        localPlayer = GameObject.FindGameObjectWithTag("LocalPlayer").GetComponent<Player>();

        numText.text = localPlayer.Spellcaster.dspellPieces[imageClone.name].ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // only create a clone if the player has enough spell pieces, and only create 1 and a time
        if (localPlayer.Spellcaster.dspellPieces[imageClone.name] > 0 && transform.parent.childCount <= 1)
        {
            // instantiating clone of whatever was clicked, and ommitting (clone) from its name
            GameObject clone = Instantiate(imageClone, transform.parent);
            clone.name = imageClone.name;

            // subtract 1 from the player's inventory whenever the spell piece is used
            localPlayer.Spellcaster.dspellPieces[imageClone.name] -= 1;
        }
    }

    void Update()
    {
        numText.text = localPlayer.Spellcaster.dspellPieces[imageClone.name].ToString();
    }
}
