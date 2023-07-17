using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTiles : MonoBehaviour
{
    [HideInInspector]
    public GenerateMap manager;
    GameObject[,] tiles;
    GameObject[] borders;
    int n;

    GameObject selectedTile;
    GameObject targetTile;

    // Start is called before the first frame update
    void Start()
    {
        tiles = manager.tiles;
        borders = manager.borders;
        n = manager.mapSize;
    }

    public void SelectPiece(GameObject tile){
        if(selectedTile != null)
            HighlightMoves(selectedTile);
        
        if(tile != selectedTile){
            HighlightMoves(tile);
            selectedTile = tile;
        
        } else{
            selectedTile = null;
        }
    }

    public void SelectTarget(GameObject tile){
        if(tile == targetTile){
            HighlightMoves(selectedTile);
            GameObject piece = selectedTile.GetComponent<TileManager>().piece;
            selectedTile.GetComponent<TileManager>().piece = null;
            piece.GetComponent<PieceManager>().currentTile = targetTile;
            targetTile.GetComponent<TileManager>().piece = piece;

            StartCoroutine(piece.GetComponent<PieceMovement>().MovePiece(selectedTile, targetTile));

            selectedTile = null;
            targetTile = null;
        
        } else{
            if(targetTile != null)
                targetTile.GetComponent<ManageTileCanvas>().setRed();

            targetTile = tile;
            tile.GetComponent<ManageTileCanvas>().setWhite();
        }
    }

    public void HighlightMoves(GameObject tile){
        int lin = tile.GetComponent<TileManager>().lin;
        int col = tile.GetComponent<TileManager>().col;
        int i, j;

        for(i = lin - 1, j = col; SelectableTile(i, j); i --)
            SelectTile(tiles[i, j]);
        for(i = lin, j = col - 1; SelectableTile(i, j); j --)
            SelectTile(tiles[i, j]);
        for(i = lin + 1, j = col; SelectableTile(i, j); i ++)
            SelectTile(tiles[i, j]);
        for(i = lin, j = col + 1; SelectableTile(i, j); j ++)
            SelectTile(tiles[i, j]);
    }

    void SelectTile(GameObject tile){
        ManageTileCanvas script = tile.GetComponent<ManageTileCanvas>();

        if(!script.image.gameObject.activeSelf)
            script.setRed();
        else
            script.disableImage();
    }

    bool SelectableTile(int i, int j){
        return i >= 0 && i < n && j >= 0 && j < n && tiles[i, j].GetComponent<TileManager>().piece == null;
    }
}
