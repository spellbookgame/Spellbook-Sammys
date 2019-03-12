using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Used for testing only.
 * Does not have any networking related code.
 * This is what the original Player.cs had
 * before it implemented network stuff.
 * 
 * Can be used directly when testing any scene.
 */

public class DummyPlayer : MonoBehaviour
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


    #region button_class_clicks
    /*
     Class choosing.  This region may move to another file 
     in the future, possibly a singleton (GameManager).
         */

    public void onClickAclhemist()
    {
        spellcaster = new Alchemist();
        Debug.Log("Local Player chose " + spellcaster.classType);
    }

    public void onClickElementalist()
    {
        spellcaster = new Elementalist();
        Debug.Log("Local Player chose " + spellcaster.classType);
    }

    public void onClickArcanist()
    {
        spellcaster = new Arcanist();
        Debug.Log("Local Player chose " + spellcaster.classType);

    }

    public void onClickChronomancer()
    {
        spellcaster = new Chronomancer();
        Debug.Log("Local Player chose " + spellcaster.classType);

    }

    public void onClickTrickster()
    {
        spellcaster = new Trickster();
        Debug.Log("Local Player chose " + spellcaster.classType);

    }

    public void onClickSummoner()
    {
        spellcaster = new Summoner();
        Debug.Log("Local Player chose " + spellcaster.classType);

    }
    #endregion
}
