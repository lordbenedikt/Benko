using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public CustomGrid customGrid;
    public Canvas canvas;
    [HideInInspector]
    public UI_Manager UI;

    void Start()
    {
        UI = canvas.GetComponent<UI_Manager>();
        customGrid = gameObject.GetComponent<CustomGrid>();
    }

    void Update()
    {
        
    }

    public int gridIndexFromPos(float x, float z) {
        return customGrid.gridIndexFromPos(x,z);
    }
}
