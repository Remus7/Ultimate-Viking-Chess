using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    Animator anim;
    public Animator cameraAnim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();    
    }

    public void SlideOver(){
        anim.SetBool("GameRoom", !anim.GetBool("GameRoom"));
        cameraAnim.SetTrigger("ZoomCamera");
    }
    
    public void LoadGame(){
        SceneManager.LoadScene("GameBoard");
    }

    public void EditKingMovement(){
        GameSettings.extendedKingMovement = !GameSettings.extendedKingMovement;
    }
}
