using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCheckBox : MonoBehaviour
{
    public ManageSettings manager;
    public GameObject checkBoxImage;
    public void enableCheckBox(){
        checkBoxImage.SetActive(!checkBoxImage.gameObject.activeSelf);
    }

    void Start(){
        checkBoxImage.SetActive(manager.gameSettings.limitedKingMovement);
    }
}
