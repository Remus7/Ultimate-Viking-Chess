using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChooseBoard : MonoBehaviour
{
    public MapSO[] maps;
    public bool[] unlocked;
    public GameObject boardPivot;
    public GenerateDemoBoard board;
    public TextMeshProUGUI titleText;

    public int id;

    ManageSettings settingsScript;

    void Start(){
        settingsScript = this.GetComponent<ManageSettings>();
    
        id = settingsScript.gameSettings.mapID;
        LoadMap();
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
        settingsScript.gameSettings.map = maps[id];
        settingsScript.gameSettings.mapID = id;
        titleText.text = maps[id].boardName;

        board.map = maps[id];
        boardPivot.transform.eulerAngles = new Vector3(0, 0, 0);
        board.GenerateBoardFull();
        boardPivot.transform.eulerAngles = new Vector3(0, -90, 0);
    }
}
