using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform tilePrefab;
    [SerializeField] private Transform gameHolder;
    [SerializeField] private GameObject playerObject;
    public bool safeCenter = true;
    public int flagLimit = 10;
    public int flagCount = 0;

    //public Text itemPopup;

    private SceneChanger sceneChanger;
    private ItemPoolManager itemManager;

    private List<Tile> tiles = new();

    private int width;
    private int height;
    private int numMines;

    private readonly float tileSize = 0.5f;

    void Start()
    {
        sceneChanger = GetComponent<SceneChanger>();
        itemManager = GetComponent<ItemPoolManager>();
        CreateGameBoard(9, 9, 10);
        //CreateGameBoard(16, 16, 40);
        //CreateGameBoard(30, 16, 90);
        ResetGameState();
    }

    public void CreateGameBoard(int width, int height, int numMines)
    {
        this.width = width;
        this.height = height;
        this.numMines = numMines;

        for (int row = 0; row < height; row++) {
            for (int col = 0; col < width; col++) {
                Transform tileTransform = Instantiate(tilePrefab);
                tileTransform.parent = gameHolder;
                float xIndex = col - ((width - 1) / 2.0f);
                float yIndex = row - ((height - 1) / 2.0f);
                tileTransform.localPosition = new Vector2(xIndex * tileSize, yIndex * tileSize);

                Tile tile = tileTransform.GetComponent<Tile>();
                tiles.Add(tile);
                tile.gameManager = this;
            }
        }
    }

    private void ResetGameState() {
        int[] minePositions = Enumerable.Range(0, tiles.Count).OrderBy(x => Random.Range(0.0f, 1.0f)).ToArray();
        int[] itemPositions = Enumerable.Range(0, tiles.Count).OrderBy(x => Random.Range(0.0f, 1.0f)).ToArray();

        //List<ShopItem> pool = new List<ShopItem>();

        for (int i = 0; i < numMines; i++)
        {
            int pos = minePositions[i];
            if(pos != (height * width)/2)
            tiles[pos].isMine = true;
        }
        for(int i = 0; i < tiles.Count; i++)
        {
            tiles[i].mineCount = HowManyMines(i);
        }

        if (ItemPoolManager.ItemPool.Count > 0)
        {
            for (int i = 0; i < ItemPoolManager.ItemPool.Count; i++)
            {
                int pos = itemPositions[i];
                if (tiles[pos].isMine == false && pos != (height * width) / 2) 
                {
                    tiles[pos].hasItem = true;
                    tiles[pos].item = ItemPoolManager.ItemPool[i];
                }
            }
        }
    }

    private int HowManyMines(int location)
    {
        int count = 0;
        foreach(int pos in GetNeighbours(location))
        {
            if (tiles[pos].isMine)
            {
                count++;
            }
        }
        return count;
    }

    private List<int> GetNeighbours(int pos)
    {
        List<int> neighbours = new();
        int row = pos / width;
        int col = pos % width;
        //(0,0) is bottem left
        if (row < (height - 1))
        {
            neighbours.Add(pos + width); //North
            if (col > 0) {
                neighbours.Add(pos + width - 1); //North-West
            }
            if (col < (width - 1)) {
                neighbours.Add(pos + width + 1); //North East
            }
        }
        if (col > 0)
        {
            neighbours.Add(pos - 1); //West
        }
        if (col < (width - 1))
        {
            neighbours.Add(pos + 1); //East
        }
        if(row > 0)
        {
            neighbours.Add(pos - width);
            if(col > 0)
            {
                neighbours.Add(pos - width - 1); //South-West
            }
            if(col < (width - 1))
            {
                neighbours.Add(pos - width + 1); //South-East
            }
        }
        return neighbours;
    }

    public void ClickNeighbours(Tile tile)
    {
        int location = tiles.IndexOf(tile);
        foreach (int pos in GetNeighbours(location))
        {
            tiles[pos].ClickedTile();
        }
    }

    public void GameOver()
    { //disable clicks on all mines
        Destroy(playerObject);
           foreach (Tile tile in tiles)
        {
            tile.ShowGameOverState();
        }
        StartCoroutine(GoToGO());
    }
    
    IEnumerator GoToGO() 
    {
        yield return new WaitForSeconds(1f);
        sceneChanger.LoadSceneByName("GameOverScreen");
    }

    public void CheckGameOver()
    {
        //if there are numMines left active, then we're done
        int count = 0;
        foreach(Tile tile in tiles)
        {
            if (tile.active)
            {
                count++;
            }
        }

        if(count == numMines)
        {
            //Flag and disable everything
            Debug.Log("Winner!");
            foreach(Tile tile in tiles)
            {
                if (tile.flagged && tile.isMine) 
                {
                    ItemPoolManager.coins += 1;
                }

                tile.active = false;
                tile.SetFlaggedIfMine();
            }
        }
    }
 
}