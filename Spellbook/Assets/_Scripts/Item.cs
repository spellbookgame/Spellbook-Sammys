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
}
