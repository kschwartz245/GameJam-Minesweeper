using System;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

public class ItemCardManager : MonoBehaviour
{
    private int id;
    private bool bought = false;

    public Text itemName;
    public Image icon;
    public Text cost;
    public Text description;

    //version for manually setting items
    public ShopItem item;

    //version for randomized items
    public ShopItem[] items;

    void Start()
    {
        int rand = UnityEngine.Random.Range(0, items.Length);
        item = items[rand];

        FillCard();
    }

    public void BuyItem() 
    {
        if (!bought) 
        {
            //if (playerCoins >= cost)
            //{
            if (ItemPoolManager.ItemPool.Count < ItemPoolManager.ItemPoolMax)
            {
                //playerCoins -= cost;

                icon.color = new Color32(75, 75, 75, 255);
                cost.text = "SOLD";
                bought = true;
            }
            /*}
             * else
             * {
             * Tell the player they can't afford the item
             * }
            */
        }
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
