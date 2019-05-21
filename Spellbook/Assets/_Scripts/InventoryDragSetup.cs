using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDragSetup : MonoBehaviour
{
    public ItemBox itemBox;
    void Start()
    {
        StartCoroutine(LateStart(1f));
        
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        foreach (Transform child in transform)
        {
            foreach(Transform grandchild in child.transform)
            {
                if(grandchild.name == "ItemButton" && grandchild.gameObject.GetComponent<Image>().IsActive())
                {
                    grandchild.gameObject.AddComponent<ItemDragHandler>();
                    grandchild.gameObject.AddComponent<CanvasGroup>();
                    grandchild.gameObject.GetComponent<ItemDragHandler>().itemBox = itemBox; 
                }
            }
        }
    }
    
}
