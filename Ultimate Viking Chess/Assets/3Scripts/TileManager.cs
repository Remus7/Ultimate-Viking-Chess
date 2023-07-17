using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public int lin;
    public int col;
    public GameObject piece;

    [HideInInspector]
    public GameObject board;

    ManageTileCanvas canvasScript;
    [HideInInspector]
    public SelectTiles boardScript;

    void Start(){
        canvasScript = this.gameObject.GetComponent<ManageTileCanvas>();
        boardScript = board.GetComponent<SelectTiles>();
    }

    void OnMouseDown(){
        if(canvasScript.image.gameObject.activeSelf && piece == null)
            boardScript.SelectTarget(this.gameObject);
    }
}
