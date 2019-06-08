using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeGuideSpawner : MonoBehaviour
{
    public GameObject potionOfBlessing;
    public GameObject distilledPotion;
    public GameObject toxicPotion;
    public GameObject marcellasBlessing;
    public GameObject archive;
    public GameObject runicDarts;
    public GameObject manipulate;
    public GameObject reverseWounds;
    public GameObject chronoblast;
    public GameObject naturalDisaster;
    public GameObject eyeOfTheStorm;
    public GameObject fireball;
    public GameObject catastrophe;
    public GameObject catharsis;
    public GameObject tragedy;
    public GameObject ravensong;
    public GameObject bearsfury;
    public GameObject skeletons;

    public GameObject selectedSpell;

    //Note: this yPos variable doesn't affect the position, it is controlled by UIAutoHover.
    public float yPos = -2;

    public void SpawnGuidePrefab(string spellGuidePrefab)
    {
        switch (spellGuidePrefab)
        {
            case "Potion of Blessing":
                selectedSpell = Instantiate(potionOfBlessing, new Vector3(0, yPos, 0), Quaternion.identity, this.transform);
                break;
            case "Distilled Potion":
                selectedSpell = Instantiate(distilledPotion, new Vector3(0, yPos, 0), Quaternion.identity, this.transform);
                break;
            case "Toxic Potion":
                selectedSpell =Instantiate(toxicPotion, new Vector3(0, yPos, 0), Quaternion.identity, this.transform);
                break;
            case "Marcella's Blessing":
                selectedSpell =Instantiate(marcellasBlessing, new Vector3(0, yPos, 0), Quaternion.identity, this.transform);
                break;
            case "Archive":
                selectedSpell =Instantiate(archive, new Vector3(0, yPos, 0), Quaternion.identity, this.transform);
                break;
            case "Runic Darts":
                selectedSpell =Instantiate(runicDarts, new Vector3(0, yPos, 0), Quaternion.identity, this.transform);
                break;
            case "Manipulate":
                selectedSpell =Instantiate(manipulate, new Vector3(0, yPos, 0), Quaternion.identity, this.transform);
                break;
            case "Reverse Wounds":
                selectedSpell =Instantiate(reverseWounds, new Vector3(0, yPos, 0), Quaternion.identity, this.transform);
                break;
            case "Chronoblast":
                selectedSpell =Instantiate(chronoblast, new Vector3(0, yPos, 0), Quaternion.identity, this.transform);
                break;
            case "Natural Disaster":
                selectedSpell =Instantiate(naturalDisaster, new Vector3(0, yPos, 0), Quaternion.identity, this.transform);
                break;
            case "Eye of the Storm":
                selectedSpell =Instantiate(eyeOfTheStorm, new Vector3(0, yPos, 0), Quaternion.identity, this.transform);
                break;
            case "Fireball":
                selectedSpell =Instantiate(fireball, new Vector3(0, yPos, 0), Quaternion.identity, this.transform);
                break;
            case "Catastrophe":
                selectedSpell =Instantiate(catastrophe, new Vector3(0, yPos, 0), Quaternion.identity, this.transform);
                break;
            case "Catharsis":
                selectedSpell =Instantiate(catharsis, new Vector3(0, yPos, 0), Quaternion.identity, this.transform);
                break;
            case "Tragedy":
                selectedSpell =Instantiate(tragedy, new Vector3(0, yPos, 0), Quaternion.identity, this.transform);
                break;
            case "Raven's Song":
                selectedSpell =Instantiate(ravensong, new Vector3(0, yPos, 0), Quaternion.identity, this.transform);
                break;
            case "Bear's Fury":
                selectedSpell =Instantiate(bearsfury, new Vector3(0, yPos, 0), Quaternion.identity, this.transform);
                break;
            case "Skeletons":
                selectedSpell =Instantiate(skeletons, new Vector3(0, yPos, 0), Quaternion.identity, this.transform);
                break;
            
        }
        Debug.Log("Moving prefab cant fi " +selectedSpell == null);
        //selectedSpell.GetComponent<RectTransform>().position = new Vector3(0, yPos, 0);
        selectedSpell.GetComponent<UIAutoHover>()._startPosition = new Vector3(0, yPos, 0);
        //selectedSpell.transform.position = selectedSpell.GetComponent<UIAutoHover>()._startPosition;
    }

    public void PlayHitSound()
    {
        SoundManager.instance.PlaySingle(SoundManager.endTurn);
    }
}
