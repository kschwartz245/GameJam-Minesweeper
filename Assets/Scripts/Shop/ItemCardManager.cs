using UnityEngine;
using UnityEngine.UI;

public class ItemCardManager : MonoBehaviour
{
    private int id;

    public Text itemName;
    public Image icon;
    public Text cost;
    public Text description;

    public ShopItem item;

    void Start()
    {
        FillCard();
    }

    private void FillCard() 
    {
        id = item.itemID;
        itemName.text = item.itemName;
        icon.sprite = item.itemIcon;
        cost.text = "Cost: " + item.itemCost.ToString() + " coins";
        description.text = item.itemDesc;
    }
}
