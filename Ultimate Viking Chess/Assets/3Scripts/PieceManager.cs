using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    public GameObject currentTile;
    public GameObject model;
    
    [HideInInspector]
    public bool isAttacker;
    [HideInInspector]
    public int modelId;

    // Start is called before the first frame update
    void Start()
    {
        SetPiece modelScript = model.GetComponent<SetPiece>();

        modelScript.setModel(modelId);
        modelScript.setMaterial(isAttacker);
    }

    public void WasClicked(){
        currentTile.GetComponent<TileManager>().boardScript.SelectPiece(currentTile.gameObject);
    }

}
