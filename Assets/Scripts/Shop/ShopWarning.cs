using UnityEngine;

public class ShopWarning : MonoBehaviour
{
    public float lifetime = .75f;

    private float time = 0f;

    void Start()
    {
        ItemPoolManager.activeShopWarning = true;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= lifetime) 
        {
            ItemPoolManager.activeShopWarning = false;
            Destroy(gameObject);
        }
    }
}
