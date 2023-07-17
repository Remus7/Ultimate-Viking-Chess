using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPiece : MonoBehaviour
{

    public GameObject[] pieces;
    GameObject piece;

    public Material attackerMat;
    public Material defenderMat;

    // Start is called before the first frame update
    public void setModel(int pieceId)
    {
        for(int i = 0; i < pieces.Length; i ++)
            if(i != pieceId)
                Destroy(pieces[i]);

        pieces[pieceId].SetActive(true);
        piece = pieces[pieceId];
    }

    public void setMaterial(bool isAttacker){
        GameObject child = piece.transform.GetChild(0).gameObject;

        if(isAttacker)
            child.GetComponent<Renderer>().material = attackerMat;
        else
            child.GetComponent<Renderer>().material = defenderMat;
    }

}