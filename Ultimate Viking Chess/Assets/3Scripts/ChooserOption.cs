using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooserOption : MonoBehaviour
{
    public GameObject highlightText;
    public GameObject highlight;
    public void selectHighlight(){
        highlightText.SetActive(true);
        highlight.SetActive(true);
    }
    public void deselectHighlight(){
        highlightText.SetActive(false);
        highlight.SetActive(false);
    }
}
