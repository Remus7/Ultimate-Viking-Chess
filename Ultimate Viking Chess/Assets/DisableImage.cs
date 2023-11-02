using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableImage : MonoBehaviour
{
    public GameObject questText;
    public void disableObject(){
        questText.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
