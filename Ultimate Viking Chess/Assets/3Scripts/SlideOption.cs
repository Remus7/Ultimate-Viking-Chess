using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlideOption : MonoBehaviour
{
    public ManageSettings manager;
    public string[] textVariants;
    public TextMeshProUGUI textObject;
    public GameObject leftArrow;
    public GameObject rightArrow;
    int id;

    // Start is called before the first frame update
    void Start(){
        if(gameObject.name == "DifficultySlider")
            id = manager.gameSettings.ComputerDifficulty;
        else if (gameObject.name == "FirstMoveSlider")
            id = manager.gameSettings.defenderFirst ? 1 : 0;
        else
            id = 0;

        if(id == 0)
            leftArrow.SetActive(false);
        else if(id == textVariants.Length - 1)
            rightArrow.SetActive(false);
        textObject.text = textVariants[id];
    }

    public void increaseId(){
        leftArrow.SetActive(true);
        if(id < textVariants.Length - 1)
            id ++;
        if(id == textVariants.Length - 1)
            rightArrow.SetActive(false);

        textObject.text = textVariants[id];
    }
    public void decreaseId(){
        rightArrow.SetActive(true);
        if(id > 0)
            id --;
        if(id == 0)
            leftArrow.SetActive(false);

        textObject.text = textVariants[id];
    }

    public void increaseDifficulty() {
        manager.gameSettings.ComputerDifficulty ++;
    }
    public void decreaseDifficulty() {
        manager.gameSettings.ComputerDifficulty --;
    }

    public void defenderFirst() {
        manager.gameSettings.defenderFirst = true;
    }
    public void attackerFirst() {
        manager.gameSettings.defenderFirst = false;
    }
}