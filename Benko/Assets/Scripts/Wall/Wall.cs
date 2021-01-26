using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    MeshFilter meshFilter;
    public Mesh corner;
    [HideInInspector]
    public GameObject adjacents;
    GameController gameController;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = corner;
    }

    void onCreate() 
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void LateUpdate() {
        // gameObject.transform.position = gameController.customGrid.snapToGrid(gameObject, target);
    }
}


