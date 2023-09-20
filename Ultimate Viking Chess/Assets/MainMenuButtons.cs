using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();    
    }

    public void SlideOver(){
        anim.SetTrigger("TriggerSlide");
    }
    
    public void LoadGame(){
        SceneManager.LoadScene("GameBoard");
    }
}
