using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChooseBoard : MonoBehaviour
{
    public MapSO[] maps;
    public bool[] unlocked;
    public GenerateDemoBoard board;
    public TextMeshProUGUI titleText;

    public int id;

    void Start(){
        titleText.text = maps[id].boardName;
    }

    public void increaseId(){
        do{
            id ++;
            id %= maps.Length;
        } while(!unlocked[id]);

        LoadMap();
    }
    public void decreaseId(){
        do{
            id --;
            id += maps.Length;
            id %= maps.Length;
        } while(!unlocked[id]);

        LoadMap();
    }

    void LoadMap(){
        this.GetComponent<ManageSettings>().gameSettings.map = maps[id];
        titleText.text = maps[id].boardName;

        board.map = maps[id];
        board.GenerateBoardFull();
        board.gameObject.GetComponent<GenerateMap>().GenerateBoardMap();
        board.gameObject.GetComponent<GeneratePieces>().GenerateBoardPieces();
    }
}
