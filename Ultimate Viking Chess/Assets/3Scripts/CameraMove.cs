using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject pivot;

    public float speedV;
    public float speedH;

    public float upperHeigth;
    public float lowerHeigth;

    float xid = 0.25f;
    float yid = 0.5f;
    float zoomDistance;
    Vector3 startRotation;

    public float dragOffset;

    bool clicked = false;

    Vector2 clickPosition;

    float Lerp(float t, float start, float end)
    {
        return start * t + end * (1f - t);
    }

    void Start(){
        zoomDistance = transform.localPosition.z;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            clicked = true;
            clickPosition = Input.mousePosition;
        }
        if(Input.GetMouseButtonUp(0))
            clicked = false;

        if(clicked == true){
            Vector2 currentMouse = Input.mousePosition;
            xid += (clickPosition.x - currentMouse.x) * speedV * Time.deltaTime * dragOffset;
            yid -= (clickPosition.y - currentMouse.y) * speedH * Time.deltaTime * dragOffset;
            clickPosition = currentMouse;

        } else{
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                xid -= speedV * Time.deltaTime;
            if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                xid += speedV * Time.deltaTime;

            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                yid -= speedH * Time.deltaTime;
            if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                yid += speedH * Time.deltaTime;
        }

        xid %= 1;
        yid = Mathf.Clamp01(yid);

        float posx = Lerp(xid, 0, 360);
        float posy = Lerp(yid, lowerHeigth, upperHeigth);

        pivot.transform.eulerAngles = new Vector3(posy, posx, 0);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, zoomDistance);
    }

}
