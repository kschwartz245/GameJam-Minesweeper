using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform tilePrefab;
    [SerializeField] private Transform gameHolder;

    private int width;
    private int height;
    private int numMines;

    private readonly float tileSize = 0.5f;

    void Start()
    {
        CreateGameBoard(9, 9, 10);
        //CreateGameBoard(16, 16, 40);
        //CreateGameBoard(30, 16, 90);
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
            }
        }
    }
}