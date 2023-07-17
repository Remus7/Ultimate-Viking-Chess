using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMovement : MonoBehaviour
{
    public float moveTime;
    Vector3 startPosition;
    Vector3 finalPosition;

    Vector3 Lerp(float t)
    {
        return finalPosition * t + startPosition * (1f - t);
    }

    public IEnumerator MovePiece(GameObject origin, GameObject target){
        int oi, oj, fi, fj;
        oi = origin.GetComponent<TileManager>().lin;
        oj = origin.GetComponent<TileManager>().col;
        fi = target.GetComponent<TileManager>().lin;
        fj = target.GetComponent<TileManager>().col;
        if(fi > oi)
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 90, transform.eulerAngles.z);
        else if(fi < oi)
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, -90, transform.eulerAngles.z);
        else if(fj > oj)
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
        else if (fj < oj)
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);

        float time = 0f;
        startPosition = origin.transform.position;
        finalPosition = target.transform.position;

        while(time <= moveTime){
            float t = time / moveTime;
            transform.position = Lerp(t);

            time += Time.deltaTime;
            yield return null;
        }

        transform.position = finalPosition;
    }

    private void DebugMovement(GameObject origin, GameObject target){
        int oi, oj, fi, fj;
        oi = origin.GetComponent<TileManager>().lin;
        oj = origin.GetComponent<TileManager>().col;
        fi = target.GetComponent<TileManager>().lin;
        fj = target.GetComponent<TileManager>().col;

        Debug.Log(string.Format("Moving from tile({0}, {1}) to tile ({2}, {3})",  oi, oj, fi, fj));
    }
}
