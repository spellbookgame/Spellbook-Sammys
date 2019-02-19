using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInventory : MonoBehaviour
{
    bool isShowing = false;
    public GameObject inventory;
    public void onClickInventory()
    {
        if (isShowing)
        {
            isShowing = false;
            inventory.SetActive(false);
        }
        else
        {
            isShowing = true;
            inventory.SetActive(true);
        }
    }
}
