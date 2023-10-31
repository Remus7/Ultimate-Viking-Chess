using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateMap2D : MonoBehaviour
{
    public MapSO map;
    public RectTransform parentImage;

    public float borderTileRatio;

    public GameObject tile;
    public GameObject border;
    public Sprite line;

    public GameObject[] piecesCircle;

    public Sprite borderPiece;
    public Sprite borderCorner;

    [HideInInspector]
    public GameObject[,] tiles;
    [HideInInspector]
    public GameObject[] borders;

    int mapSize;
    float tileSize;
    float borderSize;
    float borderModelWidth;
    float lineWidth;

    int[] linpos = new int[4] {-1, -1, 1, 1};
    int[] colpos = new int[4] {-1, 1, 1, -1};
    int[] lindir = new int[4] {1, 0, -1, 0};
    int[] coldir = new int[4] {0, -1, 0, 1};

    void Start(){
        mapSize = map.mapSize;
        tileSize = parentImage.rect.width / (mapSize + 2 * borderTileRatio);
        borderSize = borderTileRatio * tileSize;
        borderModelWidth = borderSize * 0.65f;
        lineWidth = tileSize * 0.1f;

        float offset = (tileSize * mapSize) / 2;

        tiles = new GameObject[mapSize, mapSize];
        borders = new GameObject[4];

        // Instantiate tiles
        for(int i = 0; i < mapSize; i ++){
            for(int j = 0; j < mapSize; j ++){
                GameObject newTile = Instantiate(tile);    
                newTile.SetActive(true);
                newTile.transform.parent = parentImage;
                newTile.transform.localPosition = new Vector2(i * tileSize + tileSize / 2 - offset, j * tileSize + tileSize / 2 - offset);
                newTile.transform.localScale = new Vector2(tileSize, tileSize);
                tiles[i,j] = newTile;
            }
        }

        // Instantiate borders
        for(int i = 0; i < 4; i ++){
            GameObject newBorder = Instantiate(border);
            newBorder.SetActive(true);
            newBorder.transform.parent = parentImage;
            newBorder.transform.Rotate(0, 0, (i % 2) * 90);
            newBorder.transform.localScale = new Vector2(mapSize * tileSize + 2 * borderSize,borderSize);
            borders[i] = newBorder;
        }

        borders[0].transform.localPosition = new Vector2(tileSize * mapSize / 2 - offset, -borderSize / 2 - offset); // N = 0
        borders[1].transform.localPosition = new Vector2(-borderSize / 2 - offset, tileSize * mapSize / 2 - offset); // W = 1
        borders[2].transform.localPosition = new Vector2(tileSize * mapSize / 2 - offset, tileSize * mapSize + borderSize / 2 - offset); // S = 2
        borders[3].transform.localPosition = new Vector2(tileSize * mapSize + borderSize / 2 - offset, tileSize * mapSize / 2 - offset); // E = 3

        // Instantiate border models
        float offsetCorner = mapSize * tileSize / 2 + borderSize / 2 - borderModelWidth / 2;

        float dist = 2 * offsetCorner - 2 * borderModelWidth;
        int nrSide = mapSize + 1;
        float pieceDistance = dist / nrSide;

        for(int dir = 0; dir < 4; dir ++){
            // Border corner
            GameObject newCorner = AddGUI(borderCorner, "Corner");
            newCorner.transform.localPosition = new Vector2(linpos[dir] * (offsetCorner + 0.01f), colpos[dir] * (offsetCorner + 0.01f)); 
            newCorner.transform.Rotate(0, 0, 90 + -90 * dir);
            newCorner.transform.localScale = new Vector2(borderModelWidth * 2, borderModelWidth * 2);

            // Border sides
            for(int i = 0; i < nrSide; i ++){
                GameObject newPiece = AddGUI(borderPiece, "Piece");
                newPiece.transform.localPosition = new Vector2(lindir[dir] * (pieceDistance * i + borderModelWidth + pieceDistance / 2), coldir[dir] * (pieceDistance * i + borderModelWidth + pieceDistance / 2)) + new Vector2(linpos[dir] * ((dir % 2) * borderModelWidth / 2 + offsetCorner), colpos[dir] * ((1 - dir % 2) * borderModelWidth / 2 + offsetCorner));
                newPiece.transform.localScale = new Vector2(pieceDistance + 0.03f, borderModelWidth);
                newPiece.transform.Rotate(0, 0, (dir % 2) * 90);
            }
        }

        // Instantiate divider lines
        for(int i = 0; i <= mapSize; i ++){
            AddLine(new Vector2(i * tileSize - mapSize * tileSize / 2, 0), true);
            AddLine(new Vector2(0, i * tileSize - mapSize * tileSize / 2), false);
        }

        int n = mapSize;
        int[,] piecesMap = new int[n, n];
        // int[,] fortsMap = new int[n, n];
        
        for(int i = 0; i < n; i ++){
            for(int j = 0; j < n; j ++){
                piecesMap[i, j] = map.piecesLayout.rows[i].row[j];
                // fortsMap[i, j] = map.fortsLayout.rows[i].row[j];
            }
        }

        for(int i = 0; i < n; i ++){
            for(int j = 0; j < n; j ++){
                if(piecesMap[i, j] != 0){
                    GameObject newCircle = Instantiate(piecesCircle[piecesMap[i, j] - 1]);
                    newCircle.transform.localScale = new Vector2(tileSize, tileSize);
                    newCircle.SetActive(true);
                    newCircle.transform.parent = parentImage;
                    newCircle.transform.localPosition = tiles[i, j].transform.localPosition;
                }
            }
        }
    }

    void AddLine(Vector2 position, bool rotate){
        GameObject newLine = AddGUI(line, "Line");
        newLine.transform.localScale = new Vector2(tileSize * mapSize + lineWidth, lineWidth);
        newLine.transform.localPosition = position;

        if(rotate)
            newLine.transform.Rotate(0, 0, 90);
        else
            newLine.transform.Rotate(0, 0, 0);
    }

    GameObject AddGUI(Sprite sprite, string name){
        GameObject newGUI = new GameObject(name);
        newGUI.transform.parent = parentImage;

        newGUI.AddComponent<Image>();
        newGUI.GetComponent<Image>().sprite = sprite;
        newGUI.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);

        return newGUI;
    }
}
