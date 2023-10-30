using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDemoBoard : MonoBehaviour
{
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

    void Start(){
        map = manager.maps[manager.id];
        GenerateBoardFull();
    }

    public void GenerateBoardFull(){
        int n = map.mapSize;
        mapSize = n;

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
}
