using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageTileCanvas : MonoBehaviour
{
    public GameObject canvas;
    public Image image;
    public Image fortImage;

    public Color whiteImage;
    public Color redImage;

    public Sprite[] forts;
    [HideInInspector]
    public int currentFort;

    // Start is called before the first frame update
    void Start()
    {
        canvas.transform.position = new Vector3(transform.position.x, 0.02f, transform.position.z);
        image.gameObject.SetActive(false);

        if(currentFort != 0){
            fortImage.gameObject.SetActive(true);
            fortImage.sprite = forts[currentFort - 1];
        }
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
