using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTestScript : MonoBehaviour
{
    public Item item;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() 
    {
        Inventory.instance.Add(item);
        Debug.Log("Testing " + item.name);
        Destroy(this.gameObject);
    }
}
