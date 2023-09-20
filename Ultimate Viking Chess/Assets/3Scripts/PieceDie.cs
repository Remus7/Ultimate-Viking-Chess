using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceDie : MonoBehaviour
{
    public GameObject canvasDeath;
    public GameObject pivot;
    PieceManager pieceManager;
    TileManager tileManager;

    [HideInInspector]
    public bool isDead = false;

    [HideInInspector]
    public GameObject cameraObj;

    void Update(){
        if(isDead)
            pivot.transform.LookAt(cameraObj.transform.position);
    }

    public void killPiece(){
        pieceManager = this.GetComponent<PieceManager>();
        tileManager = pieceManager.currentTile.GetComponent<TileManager>();

        pieceManager.manager.GetComponent<ManageRules>().piecesMap[tileManager.lin, tileManager.col] = 0;
        Debug.Log(tileManager.lin + " " + tileManager.col);
        tileManager.piece = null;

        canvasDeath.SetActive(true);
        isDead = true;
    }

    public void destroyPiece(){
        Destroy(this.gameObject);
    }
}
