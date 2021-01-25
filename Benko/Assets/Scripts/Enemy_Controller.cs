using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_Controller : MonoBehaviour
{
    private GameObject player;
    public float speed = 1.0f;
    GameObject controller;

    // private Transform target;
    

    void Start() {
        controller = GameObject.FindGameObjectWithTag("GameController");
    }
    
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // Vector3 targetDirection = Player.transform.position;

        // float singleStep = speed * Time.deltaTime;

        // Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Debug.DrawRay(transform.position, newDirection, Color.red);

        // // Calculate a rotation a step closer to the target and applies rotation to this object
        // transform.rotation = Quaternion.LookRotation(newDirection);
    }
}

