using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManageShopMap : MonoBehaviour
{
    public ManageShop shopManager;
    public ChooseBoard boardManager;
    public GenerateMap2D mapImage;
    [HideInInspector]
    public int id;
    [HideInInspector]
    public float l;
    [HideInInspector]
    public float padding;
    [HideInInspector]
    public float downExpand;

    float optionsW;
    float optionsH;

    public GameObject informationPanel;

    public GameObject lockedImage;
    public GameObject toggleButton;
    public GameObject nameText;
    public GameObject descriptionText;
    public GameObject questText;

    bool transition;
    [HideInInspector]
    public float time;
    float t;
    Vector2 desiredPos;
    Vector2 desiredScale;
    Vector2 startPos;
    Vector2 startScale;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(1, 1, 1);

        mapImage.map = boardManager.maps[id];
        mapImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(l, l);
        mapImage.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

        nameText.GetComponent<TextMeshProUGUI>().text = boardManager.maps[id].boardName;
        nameText.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(l, downExpand * 0.8f);
        nameText.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, - l / 2 - downExpand / 2  - padding / 2, 0);

        lockedImage.GetComponent<RectTransform>().sizeDelta = new Vector2(l, l);
        lockedImage.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

        informationPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(l, l);
        informationPanel.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

        toggleButton.GetComponent<RectTransform>().sizeDelta = new Vector2(l, l);
        toggleButton.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        optionsW = shopManager.optionsWidth + l + 2 * padding;
        optionsH = l + downExpand + 2 * padding;
        transition = false;
        informationPanel.SetActive(false);

        float descriptionWidth = shopManager.optionsWidth - 2f * padding;
        float descriptionHeigth = optionsH - padding * 2.5f - downExpand * 0.8f;
        float descriptPercentage = 0.65f;
        float questPercentage = 0.2f;

        descriptionText.GetComponent<TextMeshProUGUI>().text = boardManager.maps[id].description;
        descriptionText.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(descriptionWidth, descriptionHeigth * descriptPercentage);
        descriptionText.GetComponent<RectTransform>().anchoredPosition = new Vector2(-padding * 1.8f -descriptionWidth / 2, - descriptionHeigth * descriptPercentage / 2 - padding * 1.5f);

        questText.GetComponent<TextMeshProUGUI>().text = "LOCKED: Complete [this] to unlock (0/5)";
        questText.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(descriptionWidth, descriptionHeigth * questPercentage);
        questText.GetComponent<RectTransform>().anchoredPosition = new Vector2(-padding * 1.8f - descriptionWidth / 2, downExpand * 0.8f + descriptionHeigth * questPercentage / 2 + padding * 1.5f);

        if(!boardManager.unlocked[id]){
            lockedImage.SetActive(true);
            questText.SetActive(true);

        } else {
            lockedImage.SetActive(false);
            questText.SetActive(false);
        }
    }

    void Update(){
        if(transition){
            Vector2 currentPos, currentScale;

            if(t >= time){
                transition = false;
                currentPos = desiredPos;
                currentScale = desiredScale;

            } else{
                float x = t / time;
                currentPos = Lerp(startPos, desiredPos, x);
                currentScale = Lerp(startScale, desiredScale, x);
                t += Time.deltaTime;
            }

            informationPanel.GetComponent<RectTransform>().sizeDelta = currentScale;
            informationPanel.GetComponent<RectTransform>().localPosition = currentPos;
        }
    }

    Vector2 Lerp(Vector2 startPos, Vector2 endPos, float x){
        return startPos * (1 - x) + endPos * x;
    }

    public void unlockMap(){
        boardManager.unlocked[id] = true;
        lockedImage.GetComponent<Animator>().SetTrigger("UnlockImage");
    }

    public void expandThis(){
        // startScale = informationPanel.GetComponent<RectTransform>().sizeDelta;
        desiredScale = new Vector2(optionsW, optionsH);
        // startPos = informationPanel.GetComponent<RectTransform>().localPosition;
        desiredPos = new Vector2(optionsW / 2 - l / 2 - padding, -optionsH / 2 + l / 2 + padding);
        // transition = true;

        informationPanel.SetActive(true);
        informationPanel.GetComponent<RectTransform>().sizeDelta = desiredScale;
        informationPanel.GetComponent<RectTransform>().localPosition = desiredPos;

        t = 0;

        shopManager.expandMap(id);
    }
    public void restrictThis(){
        // startScale = informationPanel.GetComponent<RectTransform>().sizeDelta ;
        // desiredScale = new Vector2(l, l);
        // startPos = informationPanel.GetComponent<RectTransform>().localPosition;
        // desiredPos = new Vector2(0, 0);
        // transition = true;

        informationPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(l, l);
        informationPanel.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
        informationPanel.SetActive(false);
        t = 0;

        shopManager.restrictMap(id);
    }
    public void toggleThis(){
        if(shopManager.expanded[id] == false)
            expandThis();
        else
            restrictThis();
    }
}
