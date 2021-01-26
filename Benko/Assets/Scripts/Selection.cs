using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    public GameObject player = null;

    // Update is called once per frame
    void Update()
    {
        if(player!=null) {
            transform.position = player.transform.position;
        } else {
            gameObject.SetActive(false);
        }
    }
}
