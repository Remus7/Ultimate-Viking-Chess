// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// 
// public class SlideOption : MonoBehaviour
// {
//     public string[] textVariants;
//     public Text textObject;
//     int id;
// 
//     // Start is called before the first frame update
//     void Start(){
//         id = 0;
//         leftArrow.SetActive(false);
//     }
// 
//     public void increaseId(){
//         leftArrow.SetActive(true);
//         if(id < textVariants.length() - 1)
//             id ++;
//         if(id == textVariants.lentgh() - 1)
//             rightArrow.SetActive(false);
// 
//         textObject.text = textVariants[id];
//     }
//     public void decreaseId(){
//         rightArrow.SetActive(true);
//         if(id > 0)
//             id --;
//         if(id == 0)
//             leftArrow.SetActive(false);
// 
//         textObject.text = textVariants[id];
//     }
// 
//     public void increaseDifficulty() {
//         GameSettings.ComputerDifficulty ++;
//     }
//     public void decreaseDifficulty() {
//         GameSettings.ComputerDifficulty --;
//     }
// 
//     public void defenderFirst() {
//         GameSettings.defenderFirst = true;
//     }
//     public void attackerFirst() {
//         GameSettings.defenderFirst = false;
//     }
// }