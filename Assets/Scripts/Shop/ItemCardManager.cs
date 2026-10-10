using System;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

public class ItemCardManager : MonoBehaviour
{
    private int id;
    private bool bought = false;

    public Transform canvasTransform;

    public Text itemName;
    public Image icon;
    public Text cost;
    public Text description;

    //version for manually setting items
    public ShopItem item;

    //version for randomized items
    public ShopItem[] items;

    public GameObject[] warnings;

    void Start()
    {
        ItemPoolManager.activeShopWarning = false;
        canvasTransform = transform.parent.GetComponent<Transform>();

        int rand = UnityEngine.Random.Range(0, items.Length);
        item = items[rand];

        FillCard();
    }

    public void BuyItem() 
    {
        if (bought == false) 
        {
            if (ItemPoolManager.coins >= item.itemCost)
            {
                if (ItemPoolManager.ItemPool.Count < ItemPoolManager.ItemPoolMax)
                {
                    ItemPoolManager.coins -= item.itemCost;
                    ItemPoolManager.ItemPool.Add(item);
                    icon.color = new Color32(75, 75, 75, 255);
                    cost.text = "SOLD";
                    bought = true;
                }
                else 
                {
                    if (ItemPoolManager.activeShopWarning == false) 
                    {
                        ShowWarning(0);
                    }
                }
            }
            else
            {
                ShowWarning(1);
            }            
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

    private void ShowWarning(int index) 
    {
        Instantiate(warnings[index], canvasTransform);
    }
} 
