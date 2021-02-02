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

    public TextMeshProUGUI MaxUnits;
    public int unit_amount;
    //public GameObject EnemySpawner;

    //public Archer_Controller Script;
   
    void Update()
    {
        SetInspector();
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
        GameObject[] Units = GameObject.FindGameObjectsWithTag("Unit");
        unit_amount = Units.Length;

        MaxUnits.SetText("Current Units  " + unit_amount);

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

    public void CheatCode100Coins(){
        AddGold(100);
    }






    public TextMeshProUGUI UpgradeDamageText;
    public void UpgradeDamage(){
        GetSelectedUnit();
        selectedUnit.GetComponent<UnitAttributes>().damage ++;
        UpgradeDamageText.SetText("Damage: " + selectedUnit.GetComponent<UnitAttributes>().damage);
    }
    public TextMeshProUGUI UpgradeSpeedText;
    public void UpgradeSpeed(){
        GetSelectedUnit();
        selectedUnit.GetComponent<UnitAttributes>().walkspeed ++;
        UpgradeSpeedText.SetText("Speed: " + selectedUnit.GetComponent<UnitAttributes>().walkspeed);
    }

    public void Heal(){
        GetSelectedUnit();
        selectedUnit.GetComponent<Health>().Currenthealth = selectedUnit.GetComponent<Health>().MaxHealth;
        //UpgradeSpeedText.SetText("Speed: " + selectedUnit.GetComponent<UnitAttributes>().walkspeed);
    }


    public GameObject selectedUnit;
    public void GetSelectedUnit(){
        GameObject[] Units = GameObject.FindGameObjectsWithTag("Unit");
        // unit_amount = Units.Length;
        // MaxUnits.SetText("Current Units  " + unit_amount);
        foreach(GameObject ply in Units) {
            if(ply.GetComponent<isSelected>().IsSelected == true){
                selectedUnit = ply;
                //selectedUnit.GetComponent<UnitAttributes>().walkspeed ++;
            }
        }
        if(selectedUnit==null) return;
    }

    public void SetInspector(){
        if(selectedUnit != null){
            GetSelectedUnit();
            UpgradeDamageText.SetText("Damage: " + selectedUnit.GetComponent<UnitAttributes>().damage);
            //UpgradeLifeText.SetText("maxdamage: " + selectedUnit.GetComponent<UnitAttributes>().damage);
            UpgradeSpeedText.SetText("Speed: " + selectedUnit.GetComponent<UnitAttributes>().walkspeed);

        }
        
        
    }
}
