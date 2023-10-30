using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageShop : MonoBehaviour
{
    Animator anim;

    void Start(){
        anim = gameObject.GetComponent<Animator>();
    }

    public void closeMenu(){
        anim.SetTrigger("CloseMenu");
    }
    public void disableMenu(){
        this.gameObject.SetActive(false);
    }
}
