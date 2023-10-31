using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageShop : MonoBehaviour
{
    Animator anim;
    public GameObject scrollViewContent;
    public GameObject scrollView;
    public GameObject mapCase;
    public ChooseBoard boardsManager;
    public float padding;
    public float mapWidth;
    public float offsetH;

    public float optionsWidth;
    GameObject[] maps;
    int n;
    RectTransform contentPos;
    [HideInInspector]
    public bool[] expanded;

    void Start(){
        n = boardsManager.maps.Length;
        expanded = new bool[n];
        for(int i = 0; i < n; i ++)
            expanded[i] = false;

        contentPos = scrollViewContent.GetComponent<RectTransform>();
        anim = gameObject.GetComponent<Animator>();
        maps = new GameObject[n];
        spawnMaps();
    }

    public void closeMenu(){
        anim.SetTrigger("CloseMenu");
    }
    public void disableMenu(){
        // destroyMaps();
        this.gameObject.SetActive(false);
    }

    public void expandMap(int id){
        Debug.Log("expanding");
        if(expanded[id] == false){
            expanded[id] = true;
            for(int i = id + 1; i < n; i ++){
                maps[i].transform.Translate(new Vector2(optionsWidth, 0));
            }
            contentPos.sizeDelta += new Vector2(optionsWidth, 0);
        }
    }
    public void restrictMap(int id){
        Debug.Log("restricting");
        if(expanded[id] == true){
            expanded[id] = false;
            for(int i = id + 1; i < n; i ++){
                maps[i].transform.Translate(new Vector2(-optionsWidth, 0));
            }
            contentPos.sizeDelta += new Vector2(-optionsWidth, 0);
        }
    }

    public void spawnMaps(){
        float scrollViewH = scrollView.GetComponent<RectTransform>().sizeDelta.y;
        contentPos.sizeDelta = new Vector2(padding + n * (mapWidth + padding), scrollViewH);

        for(int i = 0; i < n; i ++){
            GameObject newCase = Instantiate(mapCase);
            newCase.transform.parent = contentPos;
            newCase.transform.localPosition = new Vector3(padding + i * (mapWidth + padding) + mapWidth / 2, -scrollViewH / 2 + offsetH, 0);

            ManageShopMap script = newCase.GetComponent<ManageShopMap>();
            script.shopManager = this;
            script.id = i;
            script.boardManager = boardsManager;
            script.l = mapWidth;

            maps[i] = newCase;
        }
    }
    public void destroyMaps(){
        foreach (Transform child in scrollViewContent.transform) {
            GameObject.Destroy(child.gameObject);
        }
    }
}
