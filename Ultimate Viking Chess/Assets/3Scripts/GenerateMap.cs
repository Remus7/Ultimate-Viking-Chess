using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateMap : MonoBehaviour
{
    [HideInInspector]
    public int mapSize;

    public float tileSize;
    public float borderSize;
    public float mapWidth;
    public float lineWidth;

    public GameObject tile;
    public GameObject border;
    public Sprite line;

    public GameObject board;
    GameObject canvas;

    [HideInInspector]
    public GameObject[,] tiles;
    [HideInInspector]
    public GameObject[] borders;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new GameObject[mapSize, mapSize];
        borders = new GameObject[4];

        // board = new GameObject("Board");
        board.transform.position = new Vector3(-mapSize * tileSize / 2, 0, -mapSize * tileSize / 2);
        board.GetComponent<SelectTiles>().manager = this;

        canvas = new GameObject("Canvas");
        canvas.AddComponent<Canvas>();
        canvas.transform.parent = board.gameObject.transform;
        canvas.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        canvas.GetComponent<Canvas>().sortingOrder = 1;
        canvas.transform.localPosition = new Vector3(mapSize * tileSize / 2, 0.03f, mapSize * tileSize / 2);
        canvas.transform.Rotate(90, 0, 0);
        // canvas.transform.localScale = new Vector2(mapSize * tileSize + borderSize * 2, mapSize * tileSize + borderSize * 2);
        canvas.transform.localScale = new Vector2(1, 1);
        canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);

        // Instantiate tiles
        for(int i = 0; i < mapSize; i ++){
            for(int j = 0; j < mapSize; j ++){
                GameObject newTile = Instantiate(tile);
                newTile.transform.parent = board.gameObject.transform;
                newTile.transform.localPosition = new Vector3(i * tileSize + tileSize / 2, -mapWidth / 2, j * tileSize + tileSize / 2);
                newTile.transform.localScale = new Vector3(tileSize, mapWidth, tileSize);
                
                newTile.GetComponent<TileManager>().lin = i;
                newTile.GetComponent<TileManager>().col = j;
                newTile.GetComponent<TileManager>().board = board;
                tiles[i,j] = newTile;
            }
        }

        // Instantiate borders
        for(int i = 0; i < 4; i ++){
            GameObject newBorder = Instantiate(border);
            newBorder.transform.parent = board.gameObject.transform;
            newBorder.transform.Rotate(0, (i % 2) * 90, 0);
            newBorder.transform.localScale = new Vector3(mapSize * tileSize + 2 * borderSize, mapWidth, borderSize);

            Destroy(newBorder.GetComponent<TileManager>());
            Destroy(newBorder.GetComponent<ManageTileCanvas>());
            borders[i] = newBorder;
        }

        borders[0].transform.localPosition = new Vector3(tileSize * mapSize / 2, -mapWidth / 2, -borderSize / 2); // N = 0
        borders[1].transform.localPosition = new Vector3(-borderSize / 2, -mapWidth / 2, tileSize * mapSize / 2); // W = 1
        borders[2].transform.localPosition = new Vector3(tileSize * mapSize / 2, -mapWidth / 2, tileSize * mapSize + borderSize / 2); // S = 2
        borders[3].transform.localPosition = new Vector3(tileSize * mapSize + borderSize / 2, -mapWidth / 2, tileSize * mapSize / 2); // E = 3

        // Instantiate divider lines
        for(int i = 0; i <= mapSize; i ++){
            AddLine(new Vector2(i * tileSize - mapSize * tileSize / 2, 0), true);
            AddLine(new Vector2(0, i * tileSize - mapSize * tileSize / 2), false);
        }
    }

    void AddLine(Vector2 position, bool rotate){
        GameObject newLine = new GameObject("Line");
        newLine.transform.parent = canvas.gameObject.transform;
        newLine.transform.localScale = new Vector2(tileSize * mapSize + lineWidth, lineWidth);
        newLine.transform.localPosition = position;

        newLine.AddComponent<Image>();
        newLine.GetComponent<Image>().sprite = line;
        newLine.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);

        if(rotate)
            newLine.transform.Rotate(90, 90, 0);
        else
            newLine.transform.Rotate(90, 0, 0);
    }
}
