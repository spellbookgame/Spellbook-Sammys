using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool bIsLocal = true;

    //The player's chosen spellcaster class.
    private SpellCaster spellcaster;
    private bool bHasChosenSpellcaster = false;

    public SpellCaster Spellcaster {
        get => spellcaster;
        set => spellcaster = value;
    }

    private void Start()
    {
        // Prevents player object from being destroyed when switching scenes.
        DontDestroyOnLoad(this);

        //Example of setting player's chosen spellcaster:
        // (can later implement which one player chooses).
        this.spellcaster = new Alchemist();
        bHasChosenSpellcaster = true;
    }


    // TODO finish handling cases.  Input may change.
    public void chooseSpellcaster(int num)
    {
        switch (num)
        {
            case 0:
                spellcaster = new Alchemist();
                break;
            case 1:
                //spellcaster = new Elementalist();
                break;
        }
    }
}
