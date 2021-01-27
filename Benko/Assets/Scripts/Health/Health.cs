using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    public float MaxHealth;
    public float Currenthealth;

    public GameObject HealthBar;
    public GameObject _healthbar;
    public Image healthDisplay;
    public Image image;

    public float yOffset = 2;
    public float hpBarScale = 1;
    public GameObject DiePX;
    void Awake(){
        Currenthealth = MaxHealth;
    }
    GameObject GetChildWithName(GameObject obj, string name) {
         Transform trans = obj.transform;
         Transform childTrans = trans. Find(name);
         if (childTrans != null) {
             return childTrans.gameObject;
         } else {
             return null;
         }
     }
    void Start()
    {
        GameObject go = Instantiate(HealthBar, this.transform.position + new Vector3(0,yOffset,0), Quaternion.identity);
        Vector3 ls = go.transform.localScale;
        go.transform.localScale = new Vector3(ls.x*hpBarScale,ls.x*hpBarScale,ls.x*hpBarScale);
        go.transform.SetParent(this.transform);
        healthDisplay = go.GetComponent<HealthBar>().healthDisplay.GetComponent<Image>();

        if (_healthbar != null)
        {
            Debug.Log("Found");
        }
    }

    private void Update()
    {
        healthDisplay.fillAmount = Currenthealth/MaxHealth;
        if(Currenthealth <= 0)
        {
            this.gameObject.GetComponent<Archer_test_Controller>().Die();
            Die();
        }
    }
    

    public void Die() // Nur noch wegen dem Enemy
    {
        if (this.gameObject.tag == "Enemy")
        {
            GameObject.Find("Canvas").GetComponent<UI_Manager>().AddGold(10);
            Destroy(gameObject);
        }
        
    }
}
