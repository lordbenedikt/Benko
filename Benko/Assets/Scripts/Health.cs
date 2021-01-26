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
    public Image image;

    void Awake(){
        Currenthealth = MaxHealth;
    }

    void Start()
    {
        GameObject go = Instantiate(HealthBar, this.transform.position + new Vector3(0,1,0), Quaternion.identity);
        go.transform.SetParent(this.transform);

        //_healthbar = this.transform.Find("Health_Current").gameObject;
       // _healthbar = GameObject.Find("Health_Current");
        //image = _healthbar.GetComponent<Image>();
        

        if (_healthbar != null)
        {
            //if child was found
            //Debug.Log(_healthbar);
            Debug.Log("Found");
        }

        Debug.Log("Error");

        //TakeDamage(0.5f);

    }

    private void Update()
    {
        GameObject.Find("Health_Current").GetComponent<Image>().fillAmount = Currenthealth / MaxHealth;
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
        Debug.Log("Heyy");
    }
}
