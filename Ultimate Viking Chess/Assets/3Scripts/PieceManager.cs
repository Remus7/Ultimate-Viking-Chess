using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    public GameObject currentTile;
    public GameObject model;

    [HideInInspector]
    public PieceDie dieScript;
    
    [HideInInspector]
    public GameObject manager;
    [HideInInspector]
    public bool isAttacker;
    [HideInInspector]
    public int modelId;

    [HideInInspector]
    public bool isKing;

    // Start is called before the first frame update
    void Start()
    {
        SetPiece modelScript = model.GetComponent<SetPiece>();

        modelScript.setModel(modelId);
        modelScript.setMaterial(isAttacker);
    }

    public void WasClicked(){
        int player = manager.GetComponent<ManageRules>().player;
        if( !dieScript.isDead && ((player == 1 && !isAttacker) || (player == 0 && isAttacker)) )
            currentTile.GetComponent<TileManager>().boardScript.SelectPiece(currentTile.gameObject);
    }

}
