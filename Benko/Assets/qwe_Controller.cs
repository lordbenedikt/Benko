using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qwe_Controller : MonoBehaviour
{
    public GameObject Gold;
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropGold(int _amount, Vector3 _position){
        GameObject go = Instantiate(Gold, _position, Quaternion.identity);
        go.transform.parent = GameObject.Find("Gold").transform;

    }
}
