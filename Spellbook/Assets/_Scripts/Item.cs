using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon;
    public Image image;
    public int buyPrice;
    public int sellPrice;
    public string flavorDescription;
    public string mechanicsDescription;
    public bool isDefaultItem = false;

    public Item(ItemObject newItem) 
    {
        this.name = newItem.name;
        this.icon = newItem.sprite;
        this.buyPrice = newItem.buyPrice;
        this.sellPrice = newItem.sellPrice;
        this.flavorDescription = newItem.flavorDescription;
        this.mechanicsDescription = newItem.mechanicsDescription;
    }
}
