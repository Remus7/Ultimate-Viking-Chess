using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageRules : MonoBehaviour
{
    public MapSO map;
    public GameObject cameraObj;

    GenerateMap mapScript;
    GeneratePieces piecesScript;

    [HideInInspector]
    public int mapSize;
    [HideInInspector]
    public int[,] piecesMap;
    [HideInInspector]
    public int[,] fortsMap;
    [HideInInspector]
    public int[,] pieceRotationMap;

    public int player; // 1 is defender, 0 is attacker

    // Start is called before the first frame update
    void Start()
    {
        mapScript = this.gameObject.GetComponent<GenerateMap>();
        piecesScript = this.gameObject.GetComponent<GeneratePieces>();

        int n = map.mapSize;
        mapSize = n;

        mapScript.mapSize = n;
        piecesScript.cameraObj = cameraObj;

        piecesMap = new int[n, n];
        fortsMap = new int[n, n];
        pieceRotationMap = new int[n, n];
        for(int i = 0; i < n; i ++){
            for(int j = 0; j < n; j ++){
                piecesMap[i, j] = map.piecesLayout.rows[i].row[j];
                fortsMap[i, j] = map.fortsLayout.rows[i].row[j];
                pieceRotationMap[i, j] = map.pieceRotationLayout.rows[i].row[j];
            }
        }
    }

    public void makeMove(){
        player = 1 - player;
    }

    public void checkDeath(int lin, int col){
        Debug.Log("Checking Death");
        mapScript.tiles[lin, col].GetComponent<TileManager>().piece.GetComponent<PieceDie>().killPiece();
    }
}
