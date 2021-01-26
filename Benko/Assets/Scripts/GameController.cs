using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public CustomGrid customGrid;

    void Start()
    {
        customGrid = gameObject.GetComponent<CustomGrid>();
    }

    void Update()
    {
        
    }

    public int gridIndexFromPos(float x, float z) {
        return customGrid.gridIndexFromPos(x,z);
    }
}
