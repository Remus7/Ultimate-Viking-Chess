using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageTileCanvas : MonoBehaviour
{
    public GameObject canvas;
    public Image image;

    public Color whiteImage;
    public Color redImage;

    // Start is called before the first frame update
    void Start()
    {
        canvas.transform.position = new Vector3(transform.position.x, 0.02f, transform.position.z);
        image.gameObject.SetActive(false);
    }

    public void setRed(){
        image.gameObject.SetActive(true);
        image.color = redImage;
    }

    public void setWhite(){
        image.gameObject.SetActive(true);
        image.color = whiteImage;
    }

    public void disableImage(){
        image.gameObject.SetActive(false);
    }
}
