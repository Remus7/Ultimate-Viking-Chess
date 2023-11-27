using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public ManageSettings manager;
    Animator anim;
    public GameObject shopCanvas;
    public Animator cameraAnim;
    
    public GameObject singleplayerSettings;
    public GameObject multiplayerSettings;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();    
    }

    public void EnableShop(){
        shopCanvas.SetActive(true);
    }

    public void SlideOver(int players){
        if(players == 1)
            singleplayerSettings.SetActive(true);
            multiplayerSettings.SetActive(false);
        else{
            multiplayerSettings.SetActive(true);
            singleplayerSettings.SetActive(false);
        }

        anim.SetBool("GameRoom", !anim.GetBool("GameRoom"));
        cameraAnim.SetTrigger("ZoomCamera");
    }
    
    public void LoadGame(){
        // Debug.Log("Computer Faction: " + manager.gameSettings.ComputerFaction.ToString() + "\nComputer Difficulty: " + manager.gameSettings.ComputerDifficulty.ToString() + "\nLimited King Movement: " + manager.gameSettings.limitedKingMovement.ToString() + "\nDefender First: " + manager.gameSettings.defenderFirst.ToString());
        ManageSettings.SaveSettings(manager.gameSettings);

        SceneManager.LoadScene("GameBoard");
    }

    public void EditKingMovement(){
        manager.gameSettings.limitedKingMovement = !manager.gameSettings.limitedKingMovement;
    }
}
