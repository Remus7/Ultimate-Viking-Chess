using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageShopMap : MonoBehaviour
{
    public ManageShop shopManager;
    public ChooseBoard boardManager;
    public GenerateMap2D mapImage;
    public int id;
    public float l;

    public GameObject lockedImage;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(1, 1, 1);

        mapImage.map = boardManager.maps[id];
        mapImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(l, l);
        mapImage.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

        lockedImage.GetComponent<RectTransform>().sizeDelta = new Vector2(l, l);
        lockedImage.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

        if(!boardManager.unlocked[id])
            lockedImage.SetActive(true);
        else
            lockedImage.SetActive(false);
    }

    public void unlockMap(){
        boardManager.unlocked[id] = true;
        lockedImage.GetComponent<Animator>().SetTrigger("UnlockImage");
    }

    public void expandThis(){
        shopManager.expandMap(id);
    }
    public void restrictThis(){
        shopManager.restrictMap(id);
    }
    public void toggleThis(){
        if(shopManager.expanded[id] == false)
            expandThis();
        else
            restrictThis();
    }
}
