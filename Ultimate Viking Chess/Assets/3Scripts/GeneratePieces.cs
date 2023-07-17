using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePieces : MonoBehaviour
{
    public GameObject piece;
    
    [HideInInspector]
    public int mapSize;
    [HideInInspector]
    public int[,] piecesMap;
    [HideInInspector]
    public int[,] fortsMap;
    [HideInInspector]
    public int[,] pieceRotationMap;

    GameObject[,] tiles;

    // Start is called before the first frame update
    void Start()
    {
        tiles = this.gameObject.GetComponent<GenerateMap>().tiles;

        for(int i = 0; i < mapSize; i ++){
            for(int j = 0; j < mapSize; j ++){
                if(piecesMap[i, j] != 0){
                    GameObject newPiece = Instantiate(piece);
                    newPiece.transform.parent = null;
                    newPiece.transform.position = new Vector3(tiles[i, j].transform.position.x, 0, tiles[i,j].transform.position.z);
                    newPiece.transform.Rotate(0, 90 * (pieceRotationMap[i, j] - 1), 0);

                    if(piecesMap[i, j] == 3) // King Model
                        newPiece.GetComponent<PieceManager>().modelId = 1;
                    else if(piecesMap[i, j] == 2) // Defender Model
                        newPiece.GetComponent<PieceManager>().modelId = 0;
                    else // Attacker Model
                        newPiece.GetComponent<PieceManager>().modelId = 0;
                    newPiece.GetComponent<PieceManager>().isAttacker = (piecesMap[i, j] == 1);

                    newPiece.GetComponent<PieceManager>().currentTile = tiles[i, j];
                    tiles[i, j].GetComponent<TileManager>().piece = newPiece;
                }
            }
        }        
    }
}
