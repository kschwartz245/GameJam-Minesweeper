using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "Scriptable Objects/ShopItem")]
public class ShopItem : ScriptableObject
{
    public int itemID; //to track what each item does
    public string itemName;
    public Sprite itemIcon;
    public int itemCost;
    public string itemDesc;
}
