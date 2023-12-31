using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePieces : MonoBehaviour
{
    public bool demo;
    public GameObject piece;
    
    int mapSize;
    int[,] piecesMap;
    int[,] fortsMap;
    int[,] pieceRotationMap;

    [HideInInspector]
    public GameObject cameraObj;

    GameObject[,] tiles;

    public void GenerateBoardPieces(){
        if(!demo){
            ManageRules rulesScript = this.gameObject.GetComponent<ManageRules>();

            mapSize = rulesScript.mapSize;
            piecesMap = rulesScript.piecesMap;
            fortsMap = rulesScript.fortsMap;
            pieceRotationMap = rulesScript.pieceRotationMap;

        } else{
            GenerateDemoBoard rulesScript = this.gameObject.GetComponent<GenerateDemoBoard>();

            mapSize = rulesScript.mapSize;
            piecesMap = rulesScript.piecesMap;
            fortsMap = rulesScript.fortsMap;
            pieceRotationMap = rulesScript.pieceRotationMap;
        }
        tiles = this.gameObject.GetComponent<GenerateMap>().tiles;

        for(int i = 0; i < mapSize; i ++){
            for(int j = 0; j < mapSize; j ++){
                if(piecesMap[i, j] != 0){
                    GameObject newPiece = Instantiate(piece);
                    newPiece.transform.parent = null;
                    newPiece.transform.position = new Vector3(tiles[i, j].transform.position.x, 0, tiles[i,j].transform.position.z);
                    newPiece.transform.Rotate(0, 90 * (pieceRotationMap[i, j] - 1), 0);
                    if(demo){
                        newPiece.transform.parent = transform;
                        newPiece.transform.localScale = new Vector3(0.048f, 0.048f, 0.048f);
                        newPiece.transform.localPosition = new Vector3(newPiece.transform.localPosition.x, 0, newPiece.transform.localPosition.z);
                    }

                    if(piecesMap[i, j] == 3){ // King Model
                        newPiece.GetComponent<PieceManager>().modelId = 1;
                        newPiece.GetComponent<PieceManager>().isKing = true;
                    }
                    else if(piecesMap[i, j] == 2) // Defender Model
                        newPiece.GetComponent<PieceManager>().modelId = 0;
                    else // Attacker Model
                        newPiece.GetComponent<PieceManager>().modelId = 0;
                    newPiece.GetComponent<PieceManager>().isAttacker = (piecesMap[i, j] == 1);

                    if(!demo){
                        PieceManager managerScript = newPiece.GetComponent<PieceManager>();
                        managerScript.currentTile = tiles[i, j];
                        managerScript.manager= this.gameObject;

                        managerScript.dieScript = newPiece.GetComponent<PieceDie>();
                        managerScript.dieScript.cameraObj = cameraObj;
                        tiles[i, j].GetComponent<TileManager>().piece = newPiece;

                    } else{
                        PieceManager manager = newPiece.GetComponent<PieceManager>();
                        manager.UpdateGFX();
                        Destroy(manager.model.GetComponent<SetPiece>().pieces[manager.modelId].GetComponent<ModelClick>());
                        Destroy(newPiece.GetComponent<PieceManager>());
                        Destroy(newPiece.GetComponent<PieceMovement>());
                        Destroy(newPiece.GetComponent<PieceDie>());
                    }
                }
            }
        }        

        for(int i = 0; i < mapSize; i ++){
            for(int j = 0; j < mapSize; j ++){
                if(!demo)
                    tiles[i,j].GetComponent<TileManager>().fortType = fortsMap[i, j];
                tiles[i, j].GetComponent<ManageTileCanvas>().currentFort = fortsMap[i, j];
            }
        }
    }
}
