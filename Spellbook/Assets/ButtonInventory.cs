using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInventory : MonoBehaviour
{
    bool isShowing = false;
    public GameObject inventory;
    [SerializeField] GameObject itemInfoPanel;

    public void onClickInventory()
    {
        if (isShowing)
        {
            SoundManager.instance.PlaySingle(SoundManager.closeinventory);
            isShowing = false;
            inventory.SetActive(false);
            itemInfoPanel.SetActive(false);
        }
        else
        {
            SoundManager.instance.PlaySingle(SoundManager.openinventory);
            isShowing = true;
            inventory.SetActive(true);
        }
    }
}
