using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    public float MaxHealth = 100;
    [HideInInspector]
    public float Currenthealth = 100;
    public GameObject HealthBarPrefab;
    [HideInInspector]
    public Image healthDisplay;
    public float yOffset = 2;
    public float hpBarScale = 1;

    private GameObject healthBar;

    GameObject GetChildWithName(GameObject obj, string name) {
         Transform trans = obj.transform;
         Transform childTrans = trans. Find(name);
         if (childTrans != null) {
             return childTrans.gameObject;
         } else {
             return null;
         }
     }
    void Awake() {
       Currenthealth = MaxHealth;
    }
    void Start()
    {
        healthBar = Instantiate(HealthBarPrefab, this.transform.position + new Vector3(0,yOffset,0), Quaternion.identity);
        Vector3 ls = healthBar.transform.localScale;
        healthBar.transform.localScale = new Vector3(ls.x*hpBarScale,ls.x*hpBarScale,ls.x*hpBarScale);
        healthBar.transform.SetParent(this.transform);
        healthDisplay = healthBar.GetComponent<HealthBar>().healthDisplay.GetComponent<Image>();
    }
    private void Update()
    {
        healthDisplay.fillAmount = Currenthealth/MaxHealth;
        healthBar.SetActive(Currenthealth != MaxHealth);
    }
    
}
