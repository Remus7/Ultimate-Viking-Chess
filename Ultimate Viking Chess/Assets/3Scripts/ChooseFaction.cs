using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseFaction : MonoBehaviour
{
    public ManageSettings manager;
    public ChooserOption[] selectedUI;
    int selected;

    public void Start(){
        selectedUI[0].deselectHighlight();
        selectedUI[1].deselectHighlight();
        selectedUI[2].deselectHighlight();
        selectFaction(manager.gameSettings.ComputerFaction);
    }

    public void selectFaction(int x){
        selectedUI[selected].deselectHighlight();

        selected = x;
        manager.gameSettings.ComputerFaction = x;

        selectedUI[selected].selectHighlight();
    }
}
