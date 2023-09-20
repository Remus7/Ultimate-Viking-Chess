using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public PieceDie dieScript;
    public void DestroyPiece(){
        dieScript.destroyPiece();
    }
}
