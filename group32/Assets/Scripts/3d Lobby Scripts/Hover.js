import UnityEngine.SceneManagement;
#pragma strict

var levelToLoad : String;
var soundhover : AudioClip;
var soundSelection : AudioClip;
var QuitButton : boolean = false;
 
 function OnMouseEnter(){
     GetComponent.<AudioSource>().PlayOneShot(soundhover);
 }
 
 function OnMouseUp(){
     GetComponent.<AudioSource>().PlayOneShot(soundSelection);
     yield new WaitForSeconds(0.35);
     if(QuitButton){
         Application.Quit();
     }
     else{
         SceneManager.LoadScene (levelToLoad);
     }
 }
 
