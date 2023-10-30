using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateMap : MonoBehaviour
{
    public bool demo;
    [HideInInspector]
    public int mapSize;

    public float tileSize;
    public float borderSize;
    public float tileWidth;
    public float borderWidth;
    public float lineWidth;

    public GameObject tile;
    public GameObject border;
    public Sprite line;

    public Sprite borderPiece;
    public Sprite borderCorner;
    public float borderModelWidth;

    public GameObject board;
    GameObject canvas;

    [HideInInspector]
    public GameObject[,] tiles;
    [HideInInspector]
    public GameObject[] borders;

    int[] linpos = new int[4] {-1, -1, 1, 1};
    int[] colpos = new int[4] {-1, 1, 1, -1};
    int[] lindir = new int[4] {1, 0, -1, 0};
    int[] coldir = new int[4] {0, -1, 0, 1};

    // Start is called before the first frame update
    void Start()
    {   
        GenerateBoardMap();
    }

    public void GenerateBoardMap(){
        if(demo)
            mapSize = this.gameObject.GetComponent<GenerateDemoBoard>().map.mapSize;

        tiles = new GameObject[mapSize, mapSize];
        borders = new GameObject[4];

        foreach (Transform child in board.transform) {
            GameObject.Destroy(child.gameObject);
        }
        if(!demo){
            board.transform.position = new Vector3(-mapSize * tileSize / 2, 0, -mapSize * tileSize / 2);
            board.GetComponent<SelectTiles>().manager = this;
        }

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
                newTile.transform.localPosition = new Vector3(i * tileSize + tileSize / 2, -tileWidth / 2, j * tileSize + tileSize / 2);
                newTile.transform.localScale = new Vector3(tileSize, tileWidth, tileSize);
                
                if(!demo){
                    newTile.GetComponent<TileManager>().lin = i;
                    newTile.GetComponent<TileManager>().col = j;
                    newTile.GetComponent<TileManager>().board = board;
                }
                tiles[i,j] = newTile;
            }
        }

        // Instantiate borders
        for(int i = 0; i < 4; i ++){
            GameObject newBorder = Instantiate(border);
            newBorder.transform.parent = board.gameObject.transform;
            newBorder.transform.Rotate(0, (i % 2) * 90, 0);
            newBorder.transform.localScale = new Vector3(mapSize * tileSize + 2 * borderSize, borderWidth, borderSize);

            if(!demo){
                Destroy(newBorder.GetComponent<TileManager>());
                Destroy(newBorder.GetComponent<ManageTileCanvas>());
            }
            borders[i] = newBorder;
        }

        borders[0].transform.localPosition = new Vector3(tileSize * mapSize / 2, -tileWidth / 2, -borderSize / 2); // N = 0
        borders[1].transform.localPosition = new Vector3(-borderSize / 2, -tileWidth / 2, tileSize * mapSize / 2); // W = 1
        borders[2].transform.localPosition = new Vector3(tileSize * mapSize / 2, -tileWidth / 2, tileSize * mapSize + borderSize / 2); // S = 2
        borders[3].transform.localPosition = new Vector3(tileSize * mapSize + borderSize / 2, -tileWidth / 2, tileSize * mapSize / 2); // E = 3

        // Instantiate border models
        float offsetCorner = mapSize * tileSize / 2 + borderSize / 2 - borderModelWidth / 2;

        float dist = 2 * offsetCorner - 2 * borderModelWidth;
        int nrSide = mapSize + 1;
        float pieceDistance = dist / nrSide;

        for(int dir = 0; dir < 4; dir ++){
            // Border corner
            GameObject newCorner = AddGUI(borderCorner, "Corner");
            newCorner.transform.localPosition = new Vector2(linpos[dir] * (offsetCorner + 0.01f), colpos[dir] * (offsetCorner + 0.01f)); 
            newCorner.transform.Rotate(90, -90 + 90 * dir, 0);
            newCorner.transform.localScale = new Vector2(borderModelWidth * 2, borderModelWidth * 2);

            // Border sides
            for(int i = 0; i < nrSide; i ++){
                GameObject newPiece = AddGUI(borderPiece, "Piece");
                newPiece.transform.localPosition = new Vector2(lindir[dir] * (pieceDistance * i + borderModelWidth + pieceDistance / 2), coldir[dir] * (pieceDistance * i + borderModelWidth + pieceDistance / 2)) + new Vector2(linpos[dir] * ((dir % 2) * borderModelWidth / 2 + offsetCorner), colpos[dir] * ((1 - dir % 2) * borderModelWidth / 2 + offsetCorner));
                newPiece.transform.localScale = new Vector2(pieceDistance + 0.03f, borderModelWidth);
                newPiece.transform.Rotate(90, (dir % 2) * 90, 0);
            }
        }

        // Instantiate divider lines
        for(int i = 0; i <= mapSize; i ++){
            AddLine(new Vector2(i * tileSize - mapSize * tileSize / 2, 0), true);
            AddLine(new Vector2(0, i * tileSize - mapSize * tileSize / 2), false);
        }
    }

    void AddLine(Vector2 position, bool rotate){
        GameObject newLine = AddGUI(line, "Line");
        newLine.transform.localScale = new Vector2(tileSize * mapSize + lineWidth, lineWidth);
        newLine.transform.localPosition = position;

        if(rotate)
            newLine.transform.Rotate(90, 90, 0);
        else
            newLine.transform.Rotate(90, 0, 0);
    }

    GameObject AddGUI(Sprite sprite, string name){
        GameObject newGUI = new GameObject(name);
        newGUI.transform.parent = canvas.gameObject.transform;

        newGUI.AddComponent<Image>();
        newGUI.GetComponent<Image>().sprite = sprite;
        newGUI.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);

        return newGUI;
    }
}
