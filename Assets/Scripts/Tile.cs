using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    [Header("Tile Sprites")]
    [SerializeField] private Sprite unclickedTile;
    [SerializeField] private Sprite flaggedTile;
    [SerializeField] private List<Sprite> clickedTile;
    [SerializeField] private Sprite mineTile;
    [SerializeField] private Sprite mineWrongTile;
    [SerializeField] private Sprite mineHitTile;

    [Header("GM set via code")]
    public GameManager gameManager;

    private SpriteRenderer spriteRenderer;
    public bool flagged = false;
    public bool active = true;
    public bool isMine = false;
    public int mineCount = 0;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ClickedTile();
        }
    }

    private void OnMouseOver()
    {
        if (active) {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
                if (gameManager.flagCount < gameManager.flagLimit && !flagged)
                {
                    flagged = !flagged;
                    gameManager.flagCount++;
                }
                else if (flagged)
                {
                    flagged = !flagged;
                    gameManager.flagCount--;
                }

                if (flagged)
                {
                    spriteRenderer.sprite = flaggedTile;
                }
                else
                {
                    spriteRenderer.sprite = unclickedTile;
                }
            }
        }
    }

    public void ClickedTile()
    {
        if (active & !flagged)
        {
            active = false;
            if(isMine)
            {
                spriteRenderer.sprite = mineHitTile;
                if (HasShield() == false) 
                {
                    gameManager.GameOver();
                }
            } 
            else
            {
                spriteRenderer.sprite = clickedTile[mineCount];
                if(mineCount == 0)
                {
                    gameManager.ClickNeighbours(this);
                }
                gameManager.CheckGameOver();           
            }
        }
    }

    public void ShowGameOverState()
    {
        if(active)
        {
            active = false;
            if(isMine & !flagged)
            {
                spriteRenderer.sprite = mineTile;
            } else if (flagged & isMine) {
                spriteRenderer.sprite = mineWrongTile;
            }
        }
    }

    public void SetFlaggedIfMine()
    {
        if(isMine)
        {
            flagged = true;
            spriteRenderer.sprite = flaggedTile;
        }
    }

    private bool HasShield() 
    {
        for (int i = 0; i < ItemPoolManager.inventory.Count; i++) 
        {
            if (ItemPoolManager.inventory[i].itemID == 0) 
            {
                ItemPoolManager.inventory.RemoveAt(i);

                return true;
            }
        }

        return false;
    }

}