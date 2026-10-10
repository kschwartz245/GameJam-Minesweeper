using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ItemPoolManager : MonoBehaviour
{
    public static int ItemPoolMax = 10;
    public static List<ShopItem> ItemPool = new List<ShopItem>();

    public static bool activeShopWarning = false;
}
