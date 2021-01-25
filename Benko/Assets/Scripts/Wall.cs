using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    MeshFilter meshFilter;
    public Mesh corner;
    public GameObject adjacents;
    
    void Start()
    {
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

    // void LateUpdate() {
    //     snapToGrid();
    // }

    // void snapToGrid() {
        
    // }
}


