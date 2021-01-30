using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [Header ("Setup")]
    public string BuildModeShortcut;
    public string BuildArcherShortcut;
    public string BuildEnemyShortcut;
    public int GoldAmount;
    public Vector3 spawn_pos;
    public GameObject Player_Spawn_PX;
    
    [Header ("Ignore")]
    public TextMeshProUGUI BuildModeText;
    public GameObject archerprefab;
    public GameObject wizard_prefab;
    [HideInInspector]
    public bool ActivateBuildMode;
    public TextMeshProUGUI GoldText;
    //public GameObject EnemySpawner;

    //public Archer_Controller Script;
   
    void Update()
    {
        ShowGold();
        if(Input.GetKeyDown(BuildModeShortcut)){
            BuildModeSet();
        }
        if(Input.GetKeyDown(BuildArcherShortcut)){
            BuildArcher();
        }
        if(Input.GetKeyDown(BuildEnemyShortcut)){
            //EnemySpawner.GetComponent<EnemySpawner>().SpawnEnemy();
            GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().SpawnEnemy();
        }
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
    public void ShowGold()
    {
        GoldText.SetText("Gold: " + GoldAmount);
    }

    public void BuildModeSet()
    {
        if(ActivateBuildMode)
        {
            ActivateBuildMode = false;
            BuildModeText.SetText("BuildMode: OFF (R)");

            return;
        }
        ActivateBuildMode = true;
        BuildModeText.SetText("BuildMode: ON (R)");
        
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

    public void BuildWizard()
    {
        if(GoldAmount >= 100)
        {
            Instantiate(wizard_prefab, spawn_pos, Quaternion.identity);
            GameObject go =Instantiate(Player_Spawn_PX, spawn_pos, Quaternion.identity);
            Destroy(go, 5f);
            AddGold(-100);
        }
    }

    // public void SetDamage(int _damage){
        
    //     //_damage = 
    //     Script.damage = _damage;
    // }

    public void CheatCode100Coins(){
        AddGold(100);
    }
}
