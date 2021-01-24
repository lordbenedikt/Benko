using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer_test_Controller : MonoBehaviour
{
    public Animator ShootAnim;


    private void Start()
    {
        //ShootAnim = GetComponent.Animator();
    }
    void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            ShootAnim.SetBool("Shoot", true);
        }

        if (!Input.GetKeyDown("k"))
        {
            ShootAnim.SetBool("Shoot", false);
        }
    }
}
