using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelClick : MonoBehaviour
{
    public PieceManager manager;

    void OnMouseDown(){
        manager.WasClicked();
    }
}
