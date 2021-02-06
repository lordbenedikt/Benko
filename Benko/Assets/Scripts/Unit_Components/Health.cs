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
    public GameObject HealthBar;
    [HideInInspector]
    public Image healthDisplay;
    public float yOffset = 2;
    public float hpBarScale = 1;
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
        GameObject go = Instantiate(HealthBar, this.transform.position + new Vector3(0,yOffset,0), Quaternion.identity);
        Vector3 ls = go.transform.localScale;
        go.transform.localScale = new Vector3(ls.x*hpBarScale,ls.x*hpBarScale,ls.x*hpBarScale);
        go.transform.SetParent(this.transform);
        healthDisplay = go.GetComponent<HealthBar>().healthDisplay.GetComponent<Image>();
    }
    private void Update()
    {
        healthDisplay.fillAmount = Currenthealth/MaxHealth;
    }
}
