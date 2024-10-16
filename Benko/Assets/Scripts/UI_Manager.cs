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
    public GameObject PlayerSpawnParticleEffect;
    
    [Header ("Ignore")]
    public TextMeshProUGUI BuildModeText;
    public GameObject archerprefab;
    public GameObject wizard_prefab;
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI MaxUnits;
    public int unit_amount;
    public GameObject BuildModeShade;
    public GameObject mouseprefab;
    public KeyCode EscapeKey = KeyCode.Escape;
    public GameObject Menu;
    [HideInInspector] public bool BuildModeIsActive;


    private bool iscurrentlyon;
    private GameController gameController;
    
    void Awake() 
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Start()
    {
        iscurrentlyon = false;
    }

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

        if (Input.GetKeyDown(EscapeKey))
        {

            if (iscurrentlyon == true)
            {
                Menu.gameObject.SetActive(false);
                iscurrentlyon = false;
                print("iscurrentlyon = false");
                return;
            }
            Menu.gameObject.SetActive(true);
            iscurrentlyon = true;
            print("iscurrentlyon = false");
            return;
        }
    }
    public void BuildModeSet()
    {
        if(BuildModeIsActive)
        {
            BuildModeIsActive = false;
            BuildModeText.SetText("BuildMode: OFF (R)");
            BuildModeShade.SetActive(false);
            MouseText("BuildMode: OFF");
            return;
        }
        BuildModeIsActive = true;
        BuildModeText.SetText("BuildMode: ON (R)");
        BuildModeShade.SetActive(true);
        MouseText("BuildMode: ON");
    }

    public void SetGold(int _amount)
    {
        GoldAmount = _amount;
    }

    public void AddGold(int _amount)
    {
        GoldAmount += _amount;
    }

    public void BuildArcher()
    {
        if(GoldAmount >= 50)
        {
            GameObject Archer = Instantiate(archerprefab, gameController.UnitsSpawnPoint.transform.position, Quaternion.identity);
            Archer.transform.name = "Archer";
            GameObject go = Instantiate(PlayerSpawnParticleEffect, gameController.UnitsSpawnPoint.transform.position, Quaternion.identity);
            Destroy(go, 5f);
            AddGold(-50);
            MouseText("Archer Builded");
        }

    }

    public void BuildWizard()
    {
        if(GoldAmount >= 100)
        {
            GameObject wizard = Instantiate(wizard_prefab, gameController.UnitsSpawnPoint.transform.position, Quaternion.identity);
            wizard.transform.name = "Wizard";
            GameObject go =Instantiate(PlayerSpawnParticleEffect, gameController.UnitsSpawnPoint.transform.position, Quaternion.identity);
            Destroy(go, 5f);
            AddGold(-100);
            MouseText("Wizard Builded");
        }
    }

    public void CheatCode100Coins(){
        AddGold(100);
        MouseText("You just cheated 100 coins");
    }

    public void MouseText(string _mousetext)
    {
        //print("mousetext has to be shown");
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        GameObject go = Instantiate(mouseprefab, mousePos, Quaternion.identity);
        go.transform.SetParent(GameObject.Find("Canvas").transform);
        //go.transform.parent = GameObject.Find("Canvas").transform;
        GameObject ChildGameObject1 = go.transform.GetChild(1).gameObject;
        ChildGameObject1.GetComponent<TextMeshProUGUI>().SetText(_mousetext);
        Destroy(go, 1.0f);
    }
}
