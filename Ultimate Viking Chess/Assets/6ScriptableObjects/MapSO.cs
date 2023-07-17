using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Map", menuName = "Map")]
public class MapSO : ScriptableObject
{
    public string boardName;
    public int mapSize;
    public string description; 

    public MapStruct piecesLayout;
    public MapStruct fortsLayout;
    public MapStruct pieceRotationLayout;
    int currentSize = -1;

    void OnValidate(){
        if(currentSize != mapSize){
            piecesLayout = new MapStruct { rows = new rowData[mapSize] };
            fortsLayout = new MapStruct { rows = new rowData[mapSize] };
            pieceRotationLayout = new MapStruct { rows = new rowData[mapSize] };

            for(int i = 0; i < mapSize; i ++){
                piecesLayout.rows[i].row = new int[mapSize];
                fortsLayout.rows[i].row = new int[mapSize];
                pieceRotationLayout.rows[i].row = new int[mapSize];
            }

            currentSize = mapSize;
        }
    }
}
