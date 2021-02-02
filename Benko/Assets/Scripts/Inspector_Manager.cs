using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Inspector_Manager : MonoBehaviour
{
    public TextMeshProUGUI Unit_Name;
    public TextMeshProUGUI Unit_Level;
    public TextMeshProUGUI UpgradeDamageText;
    public TextMeshProUGUI UpgradeSpeedText;
    public TextMeshProUGUI UpgradeHealText;
    private GameObject selectedUnit;
    private void Update() {
        SetInspector();
    }
    public void UpgradeDamage(){
        GetSelectedUnit();
        selectedUnit.GetComponent<UnitAttributes>().damage ++;
        UpgradeDamageText.SetText("Damage: " + selectedUnit.GetComponent<UnitAttributes>().damage);
    }
    public void UpgradeSpeed(){
        GetSelectedUnit();
        selectedUnit.GetComponent<UnitAttributes>().walkspeed ++;
        UpgradeSpeedText.SetText("Speed: " + selectedUnit.GetComponent<UnitAttributes>().walkspeed);
    }
    public void Heal(){
        GetSelectedUnit();
        selectedUnit.GetComponent<Health>().Currenthealth = selectedUnit.GetComponent<Health>().MaxHealth;
    }
    public void GetSelectedUnit(){
        GameObject[] Units = GameObject.FindGameObjectsWithTag("Unit");
        foreach(GameObject ply in Units) {
            if(ply.GetComponent<isSelected>().IsSelected == true){
                selectedUnit = ply;
                return;
            }
            if(selectedUnit==null){
                selectedUnit = null;
            }
        }
    }

    public void SetInspector(){
        if(selectedUnit != null){
            GetSelectedUnit();
            UpgradeDamageText.SetText("Damage: " + selectedUnit.GetComponent<UnitAttributes>().damage);
            //UpgradeLifeText.SetText("maxdamage: " + selectedUnit.GetComponent<UnitAttributes>().damage);
            UpgradeSpeedText.SetText("Speed: " + selectedUnit.GetComponent<UnitAttributes>().walkspeed);
            Unit_Name.SetText(selectedUnit.name);
            Unit_Level.SetText("Level: " + selectedUnit.GetComponent<UnitAttributes>().Level);
        }
    }
}
