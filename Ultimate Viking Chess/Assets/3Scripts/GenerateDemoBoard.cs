using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDemoBoard : MonoBehaviour
{
    public Transform boardCenter;
    public ChooseBoard manager;
    [HideInInspector]
    public MapSO map;
    [HideInInspector]
    public int mapSize;
    [HideInInspector]
    public int[,] piecesMap;
    [HideInInspector]
    public int[,] fortsMap;
    [HideInInspector]
    public int[,] pieceRotationMap;

    public void GenerateBoardFull(){
        int n = map.mapSize;
        mapSize = n;

        float l = this.GetComponent<GenerateMap>().tileSize * n;
        transform.localPosition = new Vector3(- l / 2 * transform.localScale.x, transform.localPosition.y, - l / 2 * transform.localScale.z);

        //float newSize = 0.03f * 9 / mapSize;
        //transform.localScale = new Vector3(newSize, newSize, newSize);

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

        gameObject.GetComponent<GenerateMap>().GenerateBoardMap();
        gameObject.GetComponent<GeneratePieces>().GenerateBoardPieces();
    }
}
