using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    public MapSO map;

    GenerateMap mapScript;
    GeneratePieces piecesScript;
    int mapSize;

    // Start is called before the first frame update
    void Start()
    {
        mapScript = this.gameObject.GetComponent<GenerateMap>();
        piecesScript = this.gameObject.GetComponent<GeneratePieces>();

        int n = map.mapSize;
        mapScript.mapSize = n;
        piecesScript.mapSize = n;

        piecesScript.piecesMap = new int[n, n];
        piecesScript.fortsMap = new int[n, n];
        piecesScript.pieceRotationMap = new int[n, n];
        for(int i = 0; i < n; i ++){
            for(int j = 0; j < n; j ++){
                piecesScript.piecesMap[i, j] = map.piecesLayout.rows[i].row[j];
                piecesScript.fortsMap[i, j] = map.fortsLayout.rows[i].row[j];
                piecesScript.pieceRotationMap[i, j] = map.pieceRotationLayout.rows[i].row[j];
            }
        }
    }
}
