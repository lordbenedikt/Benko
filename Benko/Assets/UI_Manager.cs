using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public Animator White_Fade;

    

    void Start(){
        White_Fade = GameObject.Find("AlphaFade").GetComponent<Animator>();
    }
    
    void Update()
    {
        if(Input.GetKeyDown("q")){
            Escape();
            FadeToLevel(1);
        }
    }
    public void FadeToLevel(int levelIndex){
        White_Fade.SetTrigger("FadeOut");
    }

    public void StartGame(){
        SceneManager.LoadScene("Game_Scene");
    }

    public void Options(){
        SceneManager.LoadScene("Options");
    }
    public void Leave(){
        Debug.Log("Leave Game");
        Application.Quit();
    }

    public void Back(){
        SceneManager.LoadScene("TitleScreen");
    }

    public void Escape(){

    }

    
}
