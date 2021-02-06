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
    public int GoldAmount;
    //public Vector3 spawn_pos;
    public GameObject spawn_pos;
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
    public GameObject Build_Mode_Shade;

    void Update()
    {
        GoldText.SetText("Gold: " + GoldAmount); //ShowGold

        if(Input.GetKeyDown(BuildModeShortcut)){
            BuildModeSet();
        }
        if(Input.GetKeyDown(BuildArcherShortcut)){
            BuildArcher();
        }
        GameObject[] Units = GameObject.FindGameObjectsWithTag("Unit");
        unit_amount = Units.Length;

        MaxUnits.SetText("Current Units  " + unit_amount);

    }
    public void BuildModeSet()
    {
        if(ActivateBuildMode)
        {
            ActivateBuildMode = false;
            BuildModeText.SetText("BuildMode: OFF (R)");
            Build_Mode_Shade.SetActive(false);
            return;
        }
        ActivateBuildMode = true;
        BuildModeText.SetText("BuildMode: ON (R)");
        Build_Mode_Shade.SetActive(true);
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
            GameObject Archer = Instantiate(archerprefab, spawn_pos.transform.position, Quaternion.identity);
            Archer.transform.name = "Archer";
            GameObject go =Instantiate(Player_Spawn_PX, spawn_pos.transform.position, Quaternion.identity);
            Destroy(go, 5f);
            AddGold(-50);
        }
    }

    public void BuildWizard()
    {
        if(GoldAmount >= 100)
        {
            GameObject wizard = Instantiate(wizard_prefab, spawn_pos.transform.position, Quaternion.identity);
            wizard.transform.name = "Wizard";
            GameObject go =Instantiate(Player_Spawn_PX, spawn_pos.transform.position, Quaternion.identity);
            Destroy(go, 5f);
            AddGold(-100);
        }
    }

    public void CheatCode100Coins(){
        AddGold(100);
    }
}
