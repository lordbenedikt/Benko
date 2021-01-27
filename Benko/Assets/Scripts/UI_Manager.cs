using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    //public Animator White_Fade;
    public TextMeshProUGUI GoldText;
    public int GoldAmount;
    public bool ActivateBuildMode;
    public GameObject archerprefab;
    public Vector3 spawn_pos;
    public GameObject Player_Spawn_PX;
    public TextMeshProUGUI BuildModeText;




    void Start(){
        //White_Fade = GameObject.Find("AlphaFade").GetComponent<Animator>();
        SetGold(300);
    }
    
    void Update()
    {
        ShowGold();
        


    }
    public void FadeToLevel(int levelIndex){
        //White_Fade.SetTrigger("FadeOut");
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

    public void ShowGold()
    {
        GoldText.SetText("Gold: " + GoldAmount);
    }

    public void BuildModeSet()
    {
        if(ActivateBuildMode)
        {
            ActivateBuildMode = false;
            BuildModeText.SetText("BuildMode: OFF");

            return;
        }
        ActivateBuildMode = true;
        BuildModeText.SetText("BuildMode: ON");
        
    }

    public void SetGold(int _amount)
    {
        GoldAmount = _amount;
    }

    public void AddGold(int _amount)
    {
        GoldAmount = GoldAmount + _amount;
    }

    public void BuildArcher()
    {
        if(GoldAmount >= 50)
        {
            Instantiate(archerprefab, spawn_pos, Quaternion.identity);
            GameObject go =Instantiate(Player_Spawn_PX, spawn_pos, Quaternion.identity);
            Destroy(go, 5f);
            AddGold(-50);
        }
       
    }


}
