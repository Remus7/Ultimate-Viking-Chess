using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseFaction : MonoBehaviour
{
    public GameObject[] selectedUI;
    int selected;

    public void Start(){
        deselect(0);
        deselect(1);
        deselect(2);
    }

    public void selectFaction(int x){
        deselect(selected);
        selected = x;
        select(selected);
    }

    void deselect(int id){
        selectedUI[id].SetActive(false);
    }
    void select(int id){
        selectedUI[id].SetActive(true);
    }
}
