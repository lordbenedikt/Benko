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
    private Animator Die_anim;

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
        // print("healthDisplay: " + healthDisplay);
        // print("fill: " + healthDisplay.fillAmount);

        //_healthbar = this.transform.Find("Health_Current").gameObject;
       // _healthbar = GameObject.Find("Health_Current");
        //image = _healthbar.GetComponent<Image>();
        

        if (_healthbar != null)
        {
            //if child was found
            //Debug.Log(_healthbar);
            Debug.Log("Found");
        }

        //TakeDamage(0.5f);

    }

    private void Update()
    {
        // if(MaxHealth==100) {
        //     print("healthDisplay: " + healthDisplay);
        // print("CurrentHealth: " + Currenthealth);
        // print("MaxHealth: " + MaxHealth);
        // print("Health: " + Currenthealth/MaxHealth);
        // }

        healthDisplay.fillAmount = Currenthealth/MaxHealth;
        
        //image.fillAmount = Currenthealth / MaxHealth;
        //Currenthealth = image.fillAmount;

        if(Currenthealth <= 0)
        {
            Die();
        }
    }
    public void TakeDamage(float amount)
    {
        //Currenthealth = Currenthealth - amount;
        //image.fillAmount = 0.81f;
    }

    public void Die()
    {
        
        GameObject go = Instantiate(DiePX, new Vector3(transform.position.x,transform.position.y+0.8f,transform.position.z), Quaternion.identity);
       
        if (this.gameObject.tag == "Enemy")
        {
            GameObject.Find("Canvas").GetComponent<UI_Manager>().AddGold(10);
        }

        if (this.gameObject.tag == "Player")//sollte eigentlich Archer Tag sein
        {
            print("Archer has died");
        }
        Destroy(go,1.0f);
        Destroy(gameObject);
    }
}
